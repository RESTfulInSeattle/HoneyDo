using System.Web.Mvc;

namespace HoneyDo.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
           return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            return View();
        }
    }
}