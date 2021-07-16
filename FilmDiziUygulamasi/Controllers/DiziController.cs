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
    public class DiziController : Controller
    {
        // GET: Dizi
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

            List<Dizi> diziler = new List<Dizi>();

            DataTable dt = Sorgular.SorguYap("DizileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Dizi d = new Dizi();
                d.DiziID= Convert.ToInt32(dr[0].ToString()); 
                d.DiziKapakFoto = (byte[])dr[1];
                d.DiziBaslik = dr[2].ToString();
                d.DiziKonu = dr[3].ToString();
                d.DiziBaslangicYili = Convert.ToInt32(dr[4].ToString());
                d.DiziIMDB = dr[5].ToString();
                d.DiziIzlenmeSayisi= Convert.ToInt32(dr[6].ToString());
                d.DiziBegeniOrani = Convert.ToSingle(dr[7].ToString());
                d.DiziOyuncular = dr[8].ToString();
                d.DiziYonetmenler = dr[9].ToString();
                d.DiziEkleyenAd = dr[10].ToString();
                diziler.Add(d);
            }
            ViewBag.dizilerim = diziler;
            ViewBag.islem = islem;
            return View();
        }

        public ActionResult DiziForm(bool islem = false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<Oyuncular> oyuncular = new List<Oyuncular>();
            List<Yonetmenler> yonetmenler = new List<Yonetmenler>();

            DataTable dt;

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
        public ActionResult DiziKaydet(Dizi d, HttpPostedFileBase uploadfile)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            byte[] kapakfoto = d.DiziKapakFoto;

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
                DataTable dt = Sorgular.OYBul("DiziBul", d.DiziID);
                kapakfoto = (byte[])dt.Rows[0][1];
            }

            var gelenid = d.DiziID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.DiziGuncelle("DiziGuncelle", d.DiziID, d.DiziBaslik, kapakfoto, d.DiziKonu, d.DiziBaslangicYili, d.DiziIMDB);
            }
            else
            {
                kayitOldumu = Sorgular.DiziEkle("DiziEkle", d.DiziBaslik, d.DiziKonu, kapakfoto, d.DiziBaslangicYili, d.DiziIMDB, GirisController.YoneticiID);
            }

            string[] oyuncular = d.DiziOyuncularID.Split(',');
            foreach (var oyuncu in oyuncular)
            {
                if (!string.IsNullOrEmpty(oyuncu))
                {
                    int id = Convert.ToInt32(oyuncu);
                    if (d.DiziID == 0)
                    {
                        Sorgular.FilmOYKEkle("DiziOyuncuEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("DiziOyuncuGuncelle", id, d.DiziID);
                    }
                    
                }
            }

            string[] yonetmenler = d.DiziYonetmenlerID.Split(',');
            foreach (var yonetmen in yonetmenler)
            {
                if (!string.IsNullOrEmpty(yonetmen))
                {
                    int id = Convert.ToInt32(yonetmen);
                    if (d.DiziID == 0)
                    {
                        Sorgular.FilmOYKEkle("DiziYonetmenEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("DiziYonetmenGuncelle", id, d.DiziID);
                    }
                   
                }
            }

            return RedirectToAction("DiziForm", "Dizi", new { islem = true });
        }

        public ActionResult DiziGuncelle(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<Oyuncular> oyuncular = new List<Oyuncular>();
            List<Yonetmenler> yonetmenler = new List<Yonetmenler>();
            List<Dizi> diziler = new List<Dizi>();

            Dizi d = new Dizi();
            DataTable dt = Sorgular.OYBul("DiziBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                d.DiziID = Convert.ToInt32(dr[0].ToString());
                byte[] result = (byte[])dr[1];
                d.DiziKapakFoto = result;
                d.DiziBaslik = dr[2].ToString();
                d.DiziKonu = dr[3].ToString();
                d.DiziBaslangicYili = Convert.ToInt32(dr[4].ToString());
                d.DiziIMDB = dr[5].ToString();
                d.DiziOyuncular = dr[6].ToString();
                d.DiziYonetmenler = dr[7].ToString();
                d.DiziOyuncularID = dr[8].ToString();
                d.DiziYonetmenlerID = dr[9].ToString();

            }
            if (d == null)
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

            return View("DiziForm", d);
        }

        public ActionResult DiziSil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("DiziBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("DiziSil", id);

            return RedirectToAction("Listeleme", "Dizi", new { islem = true });
        }

        public ActionResult BolumForm(bool islem = false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<Dizi> diziler = new List<Dizi>();

            DataTable dt = Sorgular.SorguYap("DizileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Dizi d = new Dizi();
                d.DiziID = Convert.ToInt32(dr[0].ToString());
                d.DiziBaslik = dr[2].ToString();
                diziler.Add(d);
            }

            ViewBag.dizilerim = diziler;
            ViewBag.islem = islem;

            return View();
        }

        [HttpPost]
        public ActionResult BolumKaydet(DiziBolum db)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            var gelenid = db.DiziBolumID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.BolumGuncelle("BolumGuncelle", gelenid, db.DiziBolumLink, db.DiziBolummBaslik, db.DiziBolumSezonNo, db.DiziID);
            }
            else
            {
                kayitOldumu = Sorgular.BolumEkle("BolumEkle", db.DiziBolumLink, db.DiziBolummBaslik, db.DiziBolumSezonNo, db.DiziID, GirisController.YoneticiID);
            }

            if (!kayitOldumu)
            {
                return RedirectToAction("BolumForm", "Dizi", new { islem = false });
            }

            return RedirectToAction("BolumForm", "Dizi", new { islem = true });
        }

        public ActionResult BolumGuncelle(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<Dizi> diziler = new List<Dizi>();
            List<DiziBolum> bolumler = new List<DiziBolum>();

            DiziBolum db = new DiziBolum();
            DataTable dt = Sorgular.OYBul("BolumBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                db.DiziBolumID = Convert.ToInt32(dr[0].ToString());
                db.DiziBolumLink = dr[1].ToString();
                db.DiziBolummBaslik = dr[2].ToString();
                db.DiziBolumSezonNo = dr[3].ToString();
                db.DiziID = Convert.ToInt32(dr[4].ToString());
            }
            if (db == null)
                return HttpNotFound();

            dt = Sorgular.SorguYap("DizileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                Dizi d = new Dizi();
                d.DiziID = Convert.ToInt32(dr[0].ToString());
                d.DiziBaslik = dr[2].ToString();
                diziler.Add(d);
            }

            ViewBag.dizilerim = diziler;
            ViewBag.islem = false;

            return View("BolumForm", db);

        }

        public ActionResult BolumSil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("BolumBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("BolumSil", id);

            return RedirectToAction("Listeleme", "Dizi", new { islem = true });
        }

        public ActionResult Bolumler(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<DiziBolum> bolumler = new List<DiziBolum>();

            DataTable dt = Sorgular.SorguYap("BolumlerimiGoster");
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr[5].ToString())==id)
                {
                    DiziBolum db = new DiziBolum();
                    db.DiziBolumID = Convert.ToInt32(dr[0].ToString());
                    db.DiziBolummBaslik = dr[1].ToString();
                    db.DiziBolumSezonNo = dr[2].ToString();
                    db.DiziIzlenmeSayisi = Convert.ToInt32(dr[3].ToString());
                    bolumler.Add(db);
                }
               
            }
            ViewBag.bolumlerim = bolumler;
            return View();
        }

    }
}