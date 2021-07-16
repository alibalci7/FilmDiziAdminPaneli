using FilmDiziUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class YoneticilerController : Controller
    {
        // GET: DiziFilmOyuncular
        public ActionResult Index()
        {
            List<Yoneticiler> yoneticiler = new List<Yoneticiler>();

            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }
            if (GirisController.YoneticiTip != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            DataTable dt = Sorgular.SorguYap("YoneticileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Yoneticiler y = new Yoneticiler();
                y.YoneticiID = Convert.ToInt32(dr[0].ToString());
                y.YoneticiAdi = dr[1].ToString();
                y.YoneticiParola = dr[2].ToString();
                y.YoneticiTip = Convert.ToInt32(dr[4].ToString());
                yoneticiler.Add(y);
            }
            ViewBag.yoneticiler = yoneticiler;
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

            ViewBag.islem = islem;

            return View();
        }

        [HttpPost]
        public ActionResult Kaydet(Yoneticiler y)
        {
            List<Yoneticiler> yoneticiler = new List<Yoneticiler>();
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }
            DataTable dt = Sorgular.SorguYap("YoneticileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Yoneticiler yon = new Yoneticiler();
                yon.YoneticiID = Convert.ToInt32(dr[0].ToString());
                yon.YoneticiAdi = dr[1].ToString();
                yoneticiler.Add(yon);
            }
            foreach (var item in yoneticiler)
            {
                if (item.YoneticiAdi == y.YoneticiAdi)
                {
                    return View("Ekle");
                }
            }
            bool kayitoldumu;

            kayitoldumu = Sorgular.YoneticiEkle("YoneticiEkle", y.YoneticiAdi, y.YoneticiParola, y.YoneticiTip);

            if (!kayitoldumu)
            {
                return RedirectToAction("Ekle", new { islem = false });
            }

            return RedirectToAction("Index", new { islem = true });
        }

    }
}