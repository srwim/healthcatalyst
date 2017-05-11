using System.Web.Mvc;

namespace PeopleSearch.Areas.AreaPerson
{
    public class AreaPersonAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "AreaPerson";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "AreaPerson_default",
                "AreaPerson/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "PeopleSearch.Areas.AreaPerson.controllers" }
            );
        }
    }
}