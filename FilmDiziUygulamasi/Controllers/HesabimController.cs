using FilmDiziUygulamasi.Models;
using System;
using System.Data;
using System.Threading;
using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class HesabimController : Controller
    {
        // GET: Hesabim
        public ActionResult Index()
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            Yoneticiler y = new Yoneticiler();
            DataTable dt = Sorgular.HesabimGoruntule("HesabimGoruntule");
            foreach (DataRow dr in dt.Rows)
            {
                
                y.YoneticiID = Convert.ToInt32(dr[0].ToString());
                y.YoneticiAdi = dr[1].ToString();
                y.YoneticiParola = dr[2].ToString();
            }
            ViewBag.hesabım = y;
            return View();
        }

        public ActionResult Ekle(bool islem=false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            ViewBag.islem = islem;

            return View();
        }

        public ActionResult Guncelle()
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            Yoneticiler y = new Yoneticiler();
            DataTable dt = Sorgular.HesabimGoruntule("HesabimGoruntule");
            foreach (DataRow dr in dt.Rows)
            {
                y.YoneticiID = Convert.ToInt32(dr[0].ToString());
                y.YoneticiAdi = dr[1].ToString();
                y.YoneticiParola = dr[2].ToString();
            }
            if (y == null)
                return HttpNotFound();

            ViewBag.islem = false;

            return View("Ekle", y);
        }

        [HttpPost]
        public ActionResult Kaydet(Yoneticiler y)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            bool kayitoldumu;

            kayitoldumu = Sorgular.HesabimGuncelle("HesabimGuncelle", y.YoneticiAdi, y.YoneticiParola);

            Thread.Sleep(2000);

            if (!kayitoldumu)
            {
                return RedirectToAction("Ekle", new { islem = false });
            }

            return RedirectToAction("Ekle", new { islem = true });
        }
    
    }
}