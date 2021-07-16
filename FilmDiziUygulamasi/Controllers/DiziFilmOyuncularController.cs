using FilmDiziUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class DiziFilmOyuncularController : Controller
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

            List<Oyuncular> oyuncular = new List<Oyuncular>();

            DataTable dt = Sorgular.SorguYap("OyuncularıListele");
            foreach (DataRow dr in dt.Rows)
            {
                Oyuncular o = new Oyuncular();
                o.OyuncuID = Convert.ToInt32(dr[0].ToString());
                o.OyuncuAdSoyad = dr[1].ToString();
                oyuncular.Add(o);
            }
            ViewBag.oyuncular = oyuncular;

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
            Oyuncular o = new Oyuncular();

            return View(o);
        }

        [HttpPost]
        public ActionResult Kaydet(Oyuncular o)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            var gelenid = o.OyuncuID;
            bool kayitoldumu;

            if (gelenid != 0)
            {
                kayitoldumu = Sorgular.OYGuncelle("OyuncuGuncelle", o.OyuncuID, o.OyuncuAdSoyad);
            }
            else
            {
                kayitoldumu = Sorgular.OYEkle("OyuncuEkle", o.OyuncuAdSoyad);
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

            List<Oyuncular> oyuncular = new List<Oyuncular>();

            Oyuncular o = new Oyuncular();
            DataTable dt = Sorgular.OYBul("OyuncuBul",id);
            foreach (DataRow dr in dt.Rows)
            {
                o.OyuncuID = Convert.ToInt32(dr[0].ToString());
                o.OyuncuAdSoyad = dr[1].ToString();
            }
            if (o == null)
                return HttpNotFound();

            ViewBag.islem = false;

            return View("Ekle", o);
        }

        public ActionResult Sil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("OyuncuBul",id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("OyuncuSil", id);

            return RedirectToAction("Index", "DiziFilmOyuncular");
        }

    }
}