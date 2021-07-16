using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index(bool islem=false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            ViewBag.islem = islem;

            return View();
        }

        public ActionResult Navside()
        {
            ViewData["yoneticiad"] = GirisController.YoneticiAd;
            return View();
        }

    }
}