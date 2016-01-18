using System;

namespace BJN.Services.Connectors.Lti.Lti
{
    public class ProviderRequest
    {
        public int ProviderRequestId { get; set; }

        public string LtiRequest { get; set; }
        public DateTime Received { get; set; }
    }
}