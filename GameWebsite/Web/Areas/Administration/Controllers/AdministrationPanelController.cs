using Microsoft.AspNetCore.Mvc;

namespace GameWebsite.Web.Areas.Administration.Controllers
{
    [Route("/Administration/Home")]
    public class AdministrationPanelController : BaseAdministrationController
    { 
        public ActionResult Index()
        {
            return View();
        }

    }
}
