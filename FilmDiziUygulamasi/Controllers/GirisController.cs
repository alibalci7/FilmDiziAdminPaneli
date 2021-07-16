using FilmDiziUygulamasi.Models;
using System;
using System.Data;
using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class GirisController : Controller
    {
        public static int YoneticiID;
        public static int YoneticiTip;
        public static string YoneticiAd;
        public static Guid g;

        public GirisController()
        {
            bool b = Sorgular.GuidKontrol(g);
            if (b==true)
            {
                OturumAcik();
            }
        }

        public ActionResult OturumAcik()
        {
            return RedirectToAction("Index", "Home");
        }

        // GET: Giris
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Kontrol(Yoneticiler y)
        {
            g = Guid.NewGuid();
            DataTable dt = Sorgular.SorguYap("GirisKontrol");
            foreach (DataRow dr in dt.Rows)
            {
                if(dr[1].ToString() ==y.YoneticiAdi && dr[2].ToString() == y.YoneticiParola)
                {
                    YoneticiID = Convert.ToInt32(dr[0].ToString());
                    YoneticiTip = Convert.ToInt32(dr[4].ToString());
                    YoneticiAd = dr[1].ToString();
                    Sorgular.GuidGuncelleme(g);
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Giris");
        }

        public ActionResult Cikis()
        {
            g = Guid.NewGuid();
            return RedirectToAction("Index", "Giris");
        }
    
    }
}