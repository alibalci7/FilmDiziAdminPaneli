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
    public class DiniDiziController : Controller
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

        public ActionResult Listeleme(bool islem = false)
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
                d.DiziKapakFoto = (byte[])dr[1];
                d.DiziBaslik = dr[2].ToString();
                d.DiziKonu = dr[3].ToString();
                d.DiziBaslangicYili = Convert.ToInt32(dr[4].ToString());
                d.DiziIMDB = dr[5].ToString();
                d.DiziIzlenmeSayisi = Convert.ToInt32(dr[6].ToString());
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
        public ActionResult DiziKaydet(DiniDizi d, HttpPostedFileBase uploadfile)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            byte[] kapakfoto = d.DiniDiziKapakFoto;

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

            var gelenid = d.DiniDiziID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.DiziGuncelle("DiniDiziGuncelle", d.DiniDiziID, d.DiniDiziBaslik, kapakfoto, d.DiniDiziKonu, d.DiniDiziBaslangicYili, d.DiniDiziIMDB);
            }
            else
            {
                kayitOldumu = Sorgular.DiziEkle("DiniDiziEkle", d.DiniDiziBaslik, d.DiniDiziKonu, kapakfoto, d.DiniDiziBaslangicYili, d.DiniDiziIMDB, GirisController.YoneticiID);
            }

            string[] oyuncular = d.DiniDiziOyuncularID.Split(',');
            foreach (var oyuncu in oyuncular)
            {
                if (!string.IsNullOrEmpty(oyuncu))
                {
                    int id = Convert.ToInt32(oyuncu);
                    if (d.DiniDiziID == 0)
                    {
                        Sorgular.FilmOYKEkle("DiniDiziOyuncuEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("DiniDiziOyuncuGuncelle", id, d.DiniDiziID);
                    }

                }
            }

            string[] yonetmenler = d.DiniDiziYonetmenlerID.Split(',');
            foreach (var yonetmen in yonetmenler)
            {
                if (!string.IsNullOrEmpty(yonetmen))
                {
                    int id = Convert.ToInt32(yonetmen);
                    if (d.DiniDiziID == 0)
                    {
                        Sorgular.FilmOYKEkle("DiniDiziYonetmenEkle", id);
                    }
                    else
                    {
                        Sorgular.FilmOYKEkle("DiniDiziYonetmenGuncelle", id, d.DiniDiziID);
                    }

                }
            }

            if (!kayitOldumu)
            {
                return RedirectToAction("DiziForm", "DiniDizi", new { islem = false });
            }

            return RedirectToAction("DiziForm", "DiniDizi", new { islem = true });
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
            List<DiniDizi> dinidiziler = new List<DiniDizi>();

            DiniDizi d = new DiniDizi();
            DataTable dt = Sorgular.OYBul("DiniDiziBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                d.DiniDiziID = Convert.ToInt32(dr[0].ToString());
                byte[] result = (byte[])dr[1];
                d.DiniDiziKapakFoto = result;
                d.DiniDiziBaslik = dr[2].ToString();
                d.DiniDiziKonu = dr[3].ToString();
                d.DiniDiziBaslangicYili = Convert.ToInt32(dr[4].ToString());
                d.DiniDiziIMDB = dr[5].ToString();
                d.DiniDiziOyuncular = dr[6].ToString();
                d.DiniDiziYonetmenler = dr[7].ToString();
                d.DiniDiziOyuncularID = dr[8].ToString();
                d.DiniDiziYonetmenlerID = dr[9].ToString();

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

            DataTable dt = Sorgular.OYBul("DiniDiziBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("DiniDiziSil", id);

            return RedirectToAction("Listeleme", "DiniDizi", new { islem = true });
        }

        public ActionResult BolumForm(bool islem = false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<DiniDizi> dinidiziler = new List<DiniDizi>();

            DataTable dt = Sorgular.SorguYap("DiniDizileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                DiniDizi d = new DiniDizi();
                d.DiniDiziID = Convert.ToInt32(dr[0].ToString());
                d.DiniDiziBaslik = dr[2].ToString();
                dinidiziler.Add(d);
            }

            ViewBag.dinidizilerim = dinidiziler;
            ViewBag.islem = islem;

            return View();
        }

        [HttpPost]
        public ActionResult BolumKaydet(DiniDiziBolum db)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            var gelenid = db.DiniDiziBolumID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.BolumGuncelle("DiniBolumGuncelle", gelenid, db.DiniDiziBolumLink, db.DiniDiziBolummBaslik, db.DiniDiziBolumSezonNo, db.DiniDiziID);
            }
            else
            {
                kayitOldumu = Sorgular.BolumEkle("DiniBolumEkle", db.DiniDiziBolumLink, db.DiniDiziBolummBaslik, db.DiniDiziBolumSezonNo, db.DiniDiziID, GirisController.YoneticiID);
            }

            if (!kayitOldumu)
            {
                return RedirectToAction("BolumForm", "DiniDizi", new { islem = false });
            }

            return RedirectToAction("BolumForm", "DiniDizi", new { islem = true });
        }

        public ActionResult BolumGuncelle(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<DiniDizi> dinidiziler = new List<DiniDizi>();
            List<DiniDiziBolum> bolumler = new List<DiniDiziBolum>();

            DiniDiziBolum db = new DiniDiziBolum();
            DataTable dt = Sorgular.OYBul("DiniBolumBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                db.DiniDiziBolumID = Convert.ToInt32(dr[0].ToString());
                db.DiniDiziBolumLink = dr[1].ToString();
                db.DiniDiziBolummBaslik = dr[2].ToString();
                db.DiniDiziBolumSezonNo = dr[3].ToString();
                db.DiniDiziID = Convert.ToInt32(dr[4].ToString());
            }
            if (db == null)
                return HttpNotFound();

            dt = Sorgular.SorguYap("DizileriListele");
            foreach (DataRow dr in dt.Rows)
            {
                DiniDizi d = new DiniDizi();
                d.DiniDiziID = Convert.ToInt32(dr[0].ToString());
                d.DiniDiziBaslik = dr[2].ToString();
                dinidiziler.Add(d);
            }

            ViewBag.dinidizilerim = dinidiziler;
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

            DataTable dt = Sorgular.OYBul("DiniBolumBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("DiniBolumSil", id);

            return RedirectToAction("Listeleme", "DiniDizi", new { islem = true });
        }

        public ActionResult Bolumler(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<DiniDiziBolum> dinibolumler = new List<DiniDiziBolum>();

            DataTable dt = Sorgular.SorguYap("DiniBolumlerimiGoster");
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr[5].ToString()) == id)
                {
                    DiniDiziBolum db = new DiniDiziBolum();
                    db.DiniDiziBolumID = Convert.ToInt32(dr[0].ToString());
                    db.DiniDiziBolummBaslik = dr[1].ToString();
                    db.DiniDiziBolumSezonNo = dr[2].ToString();
                    db.DiniDiziIzlenmeSayisi = Convert.ToInt32(dr[3].ToString());
                    dinibolumler.Add(db);
                }

            }
            ViewBag.dinibolumlerim = dinibolumler;
            return View();
        }
    
    }
}