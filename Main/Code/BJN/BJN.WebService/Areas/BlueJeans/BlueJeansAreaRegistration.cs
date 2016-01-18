using System.Web.Mvc;

namespace BJN.WebService.Areas.BlueJeans
{
    public class BlueJeansAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "BlueJeans";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "BlueJeans_default",
                "BlueJeans/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}