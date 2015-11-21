using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using BJN.Domain;
using BJN.Services.Connectors.Web;
using BJN.Services.Local;
using Microsoft.AspNet.Identity.Owin;

namespace BJN.WebService.Controllers
{
    public class UserController : ApiController
    {
        private readonly BJNApiClient _apiClient;
        private readonly ApplicationUserManager _userManager;

        public UserController()
        {
            _apiClient = new BJNApiClient();
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }

        [Route("api/User/RoomSettings")]
        public async Task<HttpResponseMessage> GetUserRoomSettings(string email)
        {
            email = HttpUtility.UrlDecode(email);
            var user = _userManager.GetUsers().FirstOrDefault(x => x.Email == email);
            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new JsonContent(new
                    {
                        Message = "User does not exist"
                    })
                };
            }

            await _apiClient.GetToken(user);

            return await _apiClient.GetUserRoomSettings();
        }
    }
}
