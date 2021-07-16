using FilmDiziUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class DiziFilmYonetmenlerController : Controller
    {
        // GET: DiziFilmOyuncular
        public ActionResult Index()
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }
            if (GirisController.YoneticiTip != 1 && GirisController.YoneticiTip != 2 && GirisController.YoneticiID != 4)
            {
                return RedirectToAction("Index", "Home");
            }

            List<Yonetmenler> yonetmenler = new List<Yonetmenler>();

            DataTable dt = Sorgular.SorguYap("YonetmenleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Yonetmenler y = new Yonetmenler();
                y.YonetmenID = Convert.ToInt32(dr[0].ToString());
                y.YonetmenAdSoyad = dr[1].ToString();
                yonetmenler.Add(y);
            }
            ViewBag.yonetmenler = yonetmenler;
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
            Yonetmenler y = new Yonetmenler();

            return View(y);
        }

        [HttpPost]
        public ActionResult Kaydet(Yonetmenler y)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            var gelenid = y.YonetmenID;
            bool kayitoldumu;

            if (gelenid != 0)
            {
                kayitoldumu = Sorgular.OYGuncelle("YonetmenGuncelle", y.YonetmenID, y.YonetmenAdSoyad);
            }
            else
            {
                kayitoldumu = Sorgular.OYEkle("YonetmenEkle", y.YonetmenAdSoyad);
            }

            if (!kayitoldumu)
            {
                return RedirectToAction("Ekle", new { islem = false });
            }

            return RedirectToAction("Ekle", new { islem = true });
        }

        public ActionResult Guncelle(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            Yonetmenler y = new Yonetmenler();
            DataTable dt = Sorgular.OYBul("YonetmenBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                y.YonetmenID = Convert.ToInt32(dr[0].ToString());
                y.YonetmenAdSoyad = dr[1].ToString();
            }
            if (y == null)
                return HttpNotFound();

            ViewBag.islem = false;

            return View("Ekle", y);
        }

        public ActionResult Sil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("YonetmenBul",id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("YonetmenSil",id);

            return RedirectToAction("Index", "DiziFilmYonetmenler");
        }
    }
}