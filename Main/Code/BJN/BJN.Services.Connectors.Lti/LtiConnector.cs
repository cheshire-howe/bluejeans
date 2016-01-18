using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BJN.Services.Connectors.Lti.Identity;
using BJN.Services.Connectors.Lti.Lti;
using BJN.Services.Connectors.Lti.Models;
using LtiLibrary.AspNet.Identity.Owin;
using LtiLibrary.Core.Common;
using LtiLibrary.Core.OAuth;
using LtiLibrary.Core.Outcomes.v1;
using LtiLibrary.Owin.Security.Lti;
using LtiLibrary.Owin.Security.Lti.Provider;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using Owin;

namespace BJN.Services.Connectors.Lti
{
    public class LtiConnector
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ProviderContext.Create);
            app.CreatePerOwinContext<LtiUserManager>(LtiUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            // The app also uses a RoleManager
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            app.UseLtiAuthentication(new LtiAuthenticationOptions
            {
                Provider = new LtiAuthenticationProvider
                {
                    // Look up the secret for the consumer
                    OnAuthenticate = async context =>
                    {
                        // Make sure the request is not being replayed
                        var timeout = TimeSpan.FromMinutes(5);
                        var oauthTimestampAbsolute = OAuthConstants.Epoch.AddSeconds(context.LtiRequest.Timestamp);
                        if (DateTime.UtcNow - oauthTimestampAbsolute > timeout)
                        {
                            throw new LtiException("Expired " + OAuthConstants.TimestampParameter);
                        }

                        var db = context.OwinContext.Get<ProviderContext>();
                        var consumer = await db.Consumers.SingleOrDefaultAsync(c => c.Key == context.LtiRequest.ConsumerKey);
                        if (consumer == null)
                        {
                            throw new LtiException("Invalid " + OAuthConstants.ConsumerKeyParameter);
                        }

                        var signature = context.LtiRequest.GenerateSignature(consumer.Secret);
                        if (!signature.Equals(context.LtiRequest.Signature))
                        {
                            throw new LtiException("Invalid " + OAuthConstants.SignatureParameter);
                        }

                        // If we made it this far the request is valid
                    },

                    // Sign in using application authentication. This handler will create a new application
                    // user if no matching application user is found.
                    OnAuthenticated = async context =>
                    {
                        var db = context.OwinContext.Get<ProviderContext>();
                        var consumer = await db.Consumers.SingleOrDefaultAsync(c => c.Key.Equals(context.LtiRequest.ConsumerKey));
                        if (consumer == null) return;

                        // Record the request for logging purposes and as reference for outcomes
                        var providerRequest = new ProviderRequest
                        {
                            Received = DateTime.UtcNow,
                            LtiRequest = JsonConvert.SerializeObject(context.LtiRequest, Formatting.None,
                            new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                        };
                        db.ProviderRequests.Add(providerRequest);
                        db.SaveChanges();

                        // Add the requst ID as a claim
                        var claims = new List<Claim>
                        {
                            new Claim("ProviderRequestId",
                                providerRequest.ProviderRequestId.ToString(CultureInfo.InvariantCulture))
                        };

                        // Outcomes can live a long time to give the teacher enough
                        // time to grade the assignment. So they are stored in a separate table.
                        var lisOutcomeServiceUrl = ((IOutcomesManagementRequest)context.LtiRequest).LisOutcomeServiceUrl;
                        var lisResultSourcedid = ((IOutcomesManagementRequest)context.LtiRequest).LisResultSourcedId;
                        if (!string.IsNullOrWhiteSpace(lisOutcomeServiceUrl)
                            && !string.IsNullOrWhiteSpace(lisResultSourcedid))
                        {
                            var outcome = await db.Outcomes.SingleOrDefaultAsync(o =>
                                o.ConsumerId == consumer.ConsumerId
                                && o.LisResultSourcedId == lisResultSourcedid);

                            if (outcome == null)
                            {
                                outcome = new Outcome
                                {
                                    ConsumerId = consumer.ConsumerId,
                                    LisResultSourcedId = lisResultSourcedid
                                };
                                db.Outcomes.Add(outcome);
                                await db.SaveChangesAsync(); // Assign OutcomeId;
                            }
                            outcome.ContextTitle = context.LtiRequest.ContextTitle;
                            outcome.ServiceUrl = lisOutcomeServiceUrl;
                            await db.SaveChangesAsync();

                            // Add the outcome ID as a claim
                            claims.Add(new Claim("OutcomeId",
                                outcome.OutcomeId.ToString(CultureInfo.InvariantCulture)));
                        }

                        // Sign in
                        await SecurityHandler.OnAuthenticated<LtiUserManager, LtiUser>(
                            context, claims);
                    },

                    // Generate a username using the LisPersonEmailPrimary from the LTI request
                    OnGenerateUserName = async context =>
                        await SecurityHandler.OnGenerateUserName(context)
                },
                //ChallengeResultUrl = new PathString("/Manage/ToolConsumerLogins"),
                SignInAsAuthenticationType = DefaultAuthenticationTypes.ApplicationCookie
            });

        }
    }
}
