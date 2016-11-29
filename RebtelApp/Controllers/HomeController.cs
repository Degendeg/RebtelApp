using System.Web.Mvc;

namespace RebtelApp.Controllers
{
    public class HomeController : Controller
    {
        public readonly IRebtelDataService _repo;
        public HomeController()
        {
            var entities = new rebtelEntities();
            var repo = new RebtelDataService(entities);
            _repo = repo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Api()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}