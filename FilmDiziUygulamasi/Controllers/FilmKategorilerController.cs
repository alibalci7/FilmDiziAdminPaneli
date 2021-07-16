using FilmDiziUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class FilmKategorilerController : Controller
    {
        // GET: FilmKategoriler
        public ActionResult Index()
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }
            if (GirisController.YoneticiTip != 1 && GirisController.YoneticiTip != 2)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Kategoriler> kategoriler = new List<Kategoriler>();

            DataTable dt = Sorgular.SorguYap("FilmKategorileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Kategoriler k = new Kategoriler();
                k.KategoriID = Convert.ToInt32(dr[0].ToString());
                k.KategoriUstID = Convert.ToInt32(dr[1].ToString());
                k.KategoriAd = dr[2].ToString();
                if (k.KategoriUstID != 0)
                    kategoriler.Add(k);
            }
            ViewBag.kategoriler = kategoriler;
            return View();
        }

        [HttpGet]
        public ActionResult Ekle(bool islem = false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            Kategoriler k = new Kategoriler();
            ViewBag.islem = islem;

            return View(k);
        }

        [HttpPost]
        public ActionResult Kaydet(Kategoriler k)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            bool kayitoldumu;

            kayitoldumu = Sorgular.KategoriEkle("KategoriEkle", k.KategoriUstID, k.KategoriAd);

            if (!kayitoldumu)
            {
                return RedirectToAction("Ekle", new { islem = false });
            }

            return RedirectToAction("Index", new { islem = true });
        }

        public ActionResult Sil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("KategoriBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("KategoriSil", id);

            ViewBag.islem = true;
            return RedirectToAction("Index");
        }
    }
}