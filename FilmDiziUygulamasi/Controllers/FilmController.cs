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
    public class FilmController : Controller
    {
        // GET: DiziFilm
        public ActionResult Index()
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            if(GirisController.YoneticiTip!=1 && GirisController.YoneticiTip != 2)
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

            List<Film> filmler = new List<Film>();

            DataTable dt = Sorgular.SorguYap("FilmleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Film f = new Film();
                f.FilmID = Convert.ToInt32(dr[0].ToString());
                f.FilmLink = dr[1].ToString();
                f.FilmKapakFoto = (byte[])dr[2];
                f.FilmAltYazi = Convert.ToBoolean(dr[3]);
                f.FilmBaslik = dr[4].ToString();
                f.FilmKonu = dr[5].ToString();
                f.FilmYapimYili = Convert.ToInt32(dr[6].ToString());
                f.FilmDil = dr[7].ToString();
                f.FilmIMDB = dr[8].ToString();
                f.FilmSure = dr[9].ToString();
                f.FilmIzlenmeSayisi = Convert.ToInt32(dr[10].ToString());
                f.FilmBegeniOrani = Convert.ToSingle(dr[11].ToString());
                f.FilmKategoriler = dr[12].ToString();
                f.FilmOyuncular = dr[13].ToString();
                f.FilmYonetmenler = dr[14].ToString();
                f.FilmEkleyenAd = dr[15].ToString();
                filmler.Add(f);
            }
            ViewBag.filmlerim = filmler;
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
            List<Kategoriler> kategoriler = new List<Kategoriler>();

            //ViewBag.film = f;

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

            dt = Sorgular.SorguYap("FilmKategorileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Kategoriler k = new Kategoriler();
                k.KategoriID = Convert.ToInt32(dr[0].ToString());
                k.KategoriUstID = Convert.ToInt32(dr[1].ToString());
                k.KategoriAd = dr[2].ToString();
                kategoriler.Add(k);
            }
            ViewBag.kategoriler = kategoriler;

            ViewBag.islem = islem;
            return View();
        }

        [HttpPost]
        public ActionResult Kaydet(Film f, HttpPostedFileBase uploadfile)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            byte[] kapakfoto = f.FilmKapakFoto;
            if (uploadfile != null)
            {
                if (uploadfile.ContentLength > 0)
                {
                    string filePath = Path.Combine(Server.MapPath("~/Images"), Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName));
                    uploadfile.SaveAs(filePath);
                    Image a = Image.FromFile(filePath);
                    Bitmap foto = ResimAyarlama.FotografBoyutlandir(a, 400, 500);
                    kapakfoto = ResimAyarlama.ImageByteCevir(foto);
                }
            }
            else
            {
                DataTable dt = Sorgular.OYBul("FilmiBul", f.FilmID);
                kapakfoto = (byte[])dt.Rows[0][2];
            }

            var gelenid = f.FilmID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.FilmGuncelle("FilmGuncelle", f.FilmID, f.FilmLink, f.FilmBaslik, f.FilmKonu, kapakfoto, f.FilmAltYazi, f.FilmYapimYili, f.FilmDil, f.FilmIMDB, f.FilmSure);
            }
            else
            {
                kayitOldumu = Sorgular.FilmEkle("FilmEkle", f.FilmLink, f.FilmBaslik, f.FilmKonu, kapakfoto, f.FilmAltYazi, f.FilmYapimYili, f.FilmDil, f.FilmIMDB, f.FilmSure, GirisController.YoneticiID);
            }

            string[] oyuncular = f.FilmOyuncularID.Split(',');
            foreach (var oyuncu in oyuncular)
            {
                if (!string.IsNullOrEmpty(oyuncu))
                {
                    int id = Convert.ToInt32(oyuncu);
                    if (f.FilmID == 0)
                    {
                        Sorgular.FilmOYKEkle("FilmOyuncuEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("FilmOyuncuGuncelle", id, f.FilmID);
                    }
                }
            }

            string[] yonetmenler = f.FilmYonetmenlerID.Split(',');
            foreach (var yonetmen in yonetmenler)
            {
                if (!string.IsNullOrEmpty(yonetmen))
                {
                    int id = Convert.ToInt32(yonetmen);
                    if (f.FilmID == 0)
                    {
                        Sorgular.FilmOYKEkle("FilmYonetmenEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("FilmYonetmenGuncelle", id, f.FilmID);
                    }
                        
                }
            }

            string[] kategoriler = f.FilmKategorilerID.Split(',');
            foreach (var kategori in kategoriler)
            {
                if (!string.IsNullOrEmpty(kategori))
                {
                    int id = Convert.ToInt32(kategori);
                    if (f.FilmID == 0)
                    {
                        Sorgular.FilmOYKEkle("FilmKategoriEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("FilmKategoriGuncelle", id, f.FilmID);
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
            List<Kategoriler> kategoriler = new List<Kategoriler>();
            List<Film> filmler = new List<Film>();

            Film film = new Film();
            DataTable dt = Sorgular.OYBul("FilmiBul",id);
            foreach (DataRow dr in dt.Rows)
            {
                film.FilmID = Convert.ToInt32(dr[0].ToString());
                film.FilmLink = dr[1].ToString();
                byte[] result = (byte[])dr[2];
                film.FilmKapakFoto = result;
                film.FilmAltYazi = Convert.ToBoolean(dr[3]);
                film.FilmBaslik = dr[4].ToString();
                film.FilmKonu = dr[5].ToString();
                film.FilmYapimYili = Convert.ToInt32(dr[6].ToString());
                film.FilmDil = dr[7].ToString();
                film.FilmIMDB = dr[8].ToString();
                film.FilmSure = dr[9].ToString();
                film.FilmIzlenmeSayisi= Convert.ToInt32(dr[10].ToString());
                film.FilmBegeniOrani = Convert.ToSingle(dr[11].ToString());
                film.FilmKategoriler = dr[12].ToString();
                film.FilmOyuncular = dr[13].ToString();
                film.FilmYonetmenler = dr[14].ToString();
                film.FilmKategorilerID = dr[15].ToString();
                film.FilmOyuncularID = dr[16].ToString();
                film.FilmYonetmenlerID = dr[17].ToString();

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

            dt = Sorgular.SorguYap("FilmKategorileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Kategoriler k = new Kategoriler();
                k.KategoriID = Convert.ToInt32(dr[0].ToString());
                k.KategoriUstID = Convert.ToInt32(dr[1].ToString());
                k.KategoriAd = dr[2].ToString();
                kategoriler.Add(k);
            }
            ViewBag.kategoriler = kategoriler;
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

            DataTable dt = Sorgular.OYBul("FilmiBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("FilmiSil", id);

            return RedirectToAction("Listeleme", "Film", new { islem = true });
        }

    }
}