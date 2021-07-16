using FilmDiziUygulamasi.Models;
using FilmDiziUygulamasi.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace FilmDiziUygulamasi.Controllers
{
    public class DiniFilmController : Controller
    {
        // GET: DiziFilm
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

            return View();
        }

        public ActionResult Listeleme(bool islem=false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<DiniFilm> dinifilmler = new List<DiniFilm>();

            DataTable dt = Sorgular.SorguYap("DiniFilmleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                DiniFilm f = new DiniFilm();
                f.DiniFilmID = Convert.ToInt32(dr[0].ToString());
                f.DiniFilmLink = dr[1].ToString();
                f.DiniFilmKapakFoto = (byte[])dr[2];
                f.DiniFilmBaslik = dr[3].ToString();
                f.DiniFilmKonu = dr[4].ToString();
                f.DiniFilmYapimYili = Convert.ToInt32(dr[5].ToString());
                f.DiniFilmDil = dr[6].ToString();
                f.DiniFilmIMDB = dr[7].ToString();
                f.DiniFilmSure = dr[8].ToString();
                f.DiniFilmIzlenmeSayisi = Convert.ToInt32(dr[9].ToString());
                f.DiniFilmBegeniOrani = Convert.ToSingle(dr[10].ToString());
                f.DiniFilmOyuncular = dr[12].ToString();
                f.DiniFilmYonetmenler = dr[13].ToString();
                f.DiniFilmEkleyenAd = dr[14].ToString();
                dinifilmler.Add(f);
            }
            ViewBag.dinifilmlerim = dinifilmler;
            ViewBag.islem = islem;
            return View();
        }

        [HttpGet]
        public ActionResult KayitFormu(bool islem = false)
        {   
            DataTable dt;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<Oyuncular> oyuncular = new List<Oyuncular>();
            List<Yonetmenler> yonetmenler = new List<Yonetmenler>();

            dt = Sorgular.SorguYap("OyuncularıListele");
            foreach (DataRow dr in dt.Rows)
            {
                Oyuncular o = new Oyuncular();
                o.OyuncuID = Convert.ToInt32(dr[0].ToString());
                o.OyuncuAdSoyad = dr[1].ToString();
                oyuncular.Add(o);
            }
            ViewBag.oyuncular = oyuncular;

            dt = Sorgular.SorguYap("YonetmenleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Yonetmenler y = new Yonetmenler();
                y.YonetmenID = Convert.ToInt32(dr[0].ToString());
                y.YonetmenAdSoyad = dr[1].ToString();
                yonetmenler.Add(y);
            }
            ViewBag.yonetmenler = yonetmenler;

            ViewBag.islem = islem;

            return View();
        }

        [HttpPost]
        public ActionResult Kaydet(DiniFilm f, HttpPostedFileBase uploadfile)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            byte[] kapakfoto = f.DiniFilmKapakFoto;
            if (uploadfile != null)
            {
                if (uploadfile.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/Images"), Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName));
                    uploadfile.SaveAs(filePath);
                    Image a = Image.FromFile(filePath);
                    Bitmap foto = ResimAyarlama.FotografBoyutlandir(a, 150, 200);
                    kapakfoto = ResimAyarlama.ImageByteCevir(foto);
                }
            }

            var gelenid = f.DiniFilmID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.FilmGuncelle("DiniFilmGuncelle", f.DiniFilmID, f.DiniFilmLink, f.DiniFilmBaslik, f.DiniFilmKonu, kapakfoto, f.DiniFilmAltYazi, f.DiniFilmYapimYili, f.DiniFilmDil, f.DiniFilmIMDB, f.DiniFilmSure);
            }
            else
            {
                kayitOldumu = Sorgular.FilmEkle("DiniFilmEkle", f.DiniFilmLink, f.DiniFilmBaslik, f.DiniFilmKonu, kapakfoto,f.DiniFilmAltYazi, f.DiniFilmYapimYili, f.DiniFilmDil, f.DiniFilmIMDB, f.DiniFilmSure, GirisController.YoneticiID);
            }

            string[] oyuncular = f.DiniFilmOyuncularID.Split(',');
            foreach (var oyuncu in oyuncular)
            {
                if (!string.IsNullOrEmpty(oyuncu))
                {
                    int id = Convert.ToInt32(oyuncu);
                    if (f.DiniFilmID == 0)
                    {
                        Sorgular.FilmOYKEkle("DiniFilmOyuncuEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("DiniFilmOyuncuGuncelle", id, f.DiniFilmID);
                    }
                }
            }

            string[] yonetmenler = f.DiniFilmYonetmenlerID.Split(',');
            foreach (var yonetmen in yonetmenler)
            {
                if (!string.IsNullOrEmpty(yonetmen))
                {
                    int id = Convert.ToInt32(yonetmen);
                    if (f.DiniFilmID == 0)
                    {
                        Sorgular.FilmOYKEkle("DiniFilmYonetmenEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("DiniFilmYonetmenGuncelle", id, f.DiniFilmID);
                    }

                }
            }

            if (!kayitOldumu)
            {
                return RedirectToAction("KayitFormu", new { islem = false });
            }
            
            return RedirectToAction("KayitFormu", new { islem = true });
        }

        public ActionResult Guncelle(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<Oyuncular> oyuncular = new List<Oyuncular>();
            List<Yonetmenler> yonetmenler = new List<Yonetmenler>();
            List<DiniFilm> dinifilmler = new List<DiniFilm>();

            DiniFilm film = new DiniFilm();
            DataTable dt = Sorgular.OYBul("DiniFilmiBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                film.DiniFilmID = Convert.ToInt32(dr[0].ToString());
                film.DiniFilmLink = dr[1].ToString();
                byte[] result = (byte[])dr[2];
                film.DiniFilmKapakFoto = result;
                film.DiniFilmBaslik = dr[3].ToString();
                film.DiniFilmKonu = dr[4].ToString();
                film.DiniFilmYapimYili = Convert.ToInt32(dr[5].ToString());
                film.DiniFilmDil = dr[6].ToString();
                film.DiniFilmIMDB = dr[7].ToString();
                film.DiniFilmSure = dr[8].ToString();
                film.DiniFilmOyuncular = dr[12].ToString();
                film.DiniFilmYonetmenler = dr[13].ToString();
                film.DiniFilmOyuncularID = dr[15].ToString();
                film.DiniFilmYonetmenlerID = dr[16].ToString();

            }
            if (film == null)
                return HttpNotFound();

            dt = Sorgular.SorguYap("OyuncularıListele");
            foreach (DataRow dr in dt.Rows)
            {
                Oyuncular o = new Oyuncular();
                o.OyuncuID = Convert.ToInt32(dr[0].ToString());
                o.OyuncuAdSoyad = dr[1].ToString();
                oyuncular.Add(o);
            }
            ViewBag.oyuncular = oyuncular;

            dt = Sorgular.SorguYap("YonetmenleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Yonetmenler y = new Yonetmenler();
                y.YonetmenID = Convert.ToInt32(dr[0].ToString());
                y.YonetmenAdSoyad = dr[1].ToString();
                yonetmenler.Add(y);
            }
            ViewBag.yonetmenler = yonetmenler;
            ViewBag.islem = false;

            return View("KayitFormu", film);

        }

        public ActionResult Sil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("DiniFilmiBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("DiniFilmiSil", id);

            return RedirectToAction("Listeleme", "DiniFilm", new { islem = true });
        }
    
    }
}