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
    public class CizgiFilmController : Controller
    {
        // GET: CizgiFilm
        public ActionResult Index()
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }
            if (GirisController.YoneticiTip != 1 && GirisController.YoneticiTip != 3)
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

            List<CizgiFilm> cizgifilmler = new List<CizgiFilm>();

            DataTable dt = Sorgular.SorguYap("CizgiFilmleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                CizgiFilm c = new CizgiFilm();
                c.CizgiFilmID = Convert.ToInt32(dr[0].ToString());
                c.CizgiFilmKapakFoto = (byte[])dr[1];
                c.CizgiFilmBaslik = dr[2].ToString();
                c.CizgiFilmIzlenmeSayisi = Convert.ToInt32(dr[3].ToString());
                c.CizgiFilmEkleyenAd = dr[4].ToString();
                cizgifilmler.Add(c);
            }
            ViewBag.cizgifilmlerim = cizgifilmler;
            ViewBag.islem = islem;
            return View();
        }

        public ActionResult CizgiFilmForm(bool islem = false)
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
        public ActionResult CizgiFilmKaydet(CizgiFilm c, HttpPostedFileBase uploadfile)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            byte[] kapakfoto = c.CizgiFilmKapakFoto;

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

            var gelenid = c.CizgiFilmID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.CizgiFilmGuncelle(c.CizgiFilmID, kapakfoto, c.CizgiFilmBaslik);
            }
            else
            {
                kayitOldumu = Sorgular.CizgiFilmEkle(c.CizgiFilmBaslik, kapakfoto, GirisController.YoneticiID);
            }

            if (!kayitOldumu)
            {
                return RedirectToAction("CizgiFilmForm", new { islem = false });
            }

            return RedirectToAction("CizgiFilmForm", new { islem = true });
        }

        public ActionResult CizgiFilmGuncelle(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<CizgiFilm> cizgifilmler = new List<CizgiFilm>();

            CizgiFilm cf = new CizgiFilm();
            DataTable dt = Sorgular.OYBul("CizgiFilmBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                cf.CizgiFilmID = Convert.ToInt32(dr[0].ToString());
                byte[] result = (byte[])dr[1];
                cf.CizgiFilmKapakFoto = result;
                cf.CizgiFilmBaslik = dr[2].ToString();

            }
            if (cf == null)
                return HttpNotFound();

            //ViewBag.cizgifilm = c;
            ViewBag.islem = false;

            return View("CizgiFilmForm", cf);

        }

        public ActionResult CizgiFilmSil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("CizgiFilmBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("CizgiFilmSil", id);

            return RedirectToAction("Listeleme", "CizgiFilm", new { islem = true });
        }

        public ActionResult BolumForm(bool islem = false)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<CizgiFilm> cizgifilmler = new List<CizgiFilm>();

            DataTable dt = Sorgular.SorguYap("CizgiFilmleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                CizgiFilm c = new CizgiFilm();
                c.CizgiFilmID = Convert.ToInt32(dr[0].ToString());
                c.CizgiFilmBaslik = dr[2].ToString();
                cizgifilmler.Add(c);
            }

            ViewBag.islem = islem;
            ViewBag.cizgifilmlerim = cizgifilmler;

            return View();
        }

        [HttpPost]
        public ActionResult BolumKaydet(CizgiFilmBolum cb)
        {
            bool kayitOldumu;
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            var gelenid = cb.CizgiFilmBolumID;
            if (gelenid != 0)
            {
                kayitOldumu = Sorgular.BolumGuncelle("CFBolumGuncelle", gelenid, cb.CizgiFilmBolumLink, cb.CizgiFilmBolumBaslik, cb.CizgiFilmBolumSezonNo, cb.CizgiFilmID);
            }
            else
            {
                kayitOldumu = Sorgular.BolumEkle("CFBolumEkle", cb.CizgiFilmBolumLink, cb.CizgiFilmBolumBaslik, cb.CizgiFilmBolumSezonNo, cb.CizgiFilmID, GirisController.YoneticiID);
            }

            if (!kayitOldumu)
            {
                return RedirectToAction("BolumForm", new { islem = false });
            }

            if (!kayitOldumu)
            {
                return RedirectToAction("BolumForm", new { islem = false });
            }

            return RedirectToAction("BolumForm", new { islem = true });
        }

        public ActionResult BolumGuncelle(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<CizgiFilm> cizgifilmler = new List<CizgiFilm>();

            CizgiFilmBolum cfb = new CizgiFilmBolum();
            DataTable dt = Sorgular.OYBul("CFBolumBul", id);
            foreach (DataRow dr in dt.Rows)
            {
                cfb.CizgiFilmBolumID = Convert.ToInt32(dr[0].ToString());
                cfb.CizgiFilmBolumLink = dr[1].ToString();
                cfb.CizgiFilmBolumBaslik = dr[2].ToString();
                cfb.CizgiFilmBolumSezonNo = dr[3].ToString();
                cfb.CizgiFilmID = Convert.ToInt32(dr[4].ToString());
            }
            if (cfb == null)
                return HttpNotFound();

            dt = Sorgular.SorguYap("CizgiFilmleriListele");
            foreach (DataRow dr in dt.Rows)
            {
                CizgiFilm c = new CizgiFilm();
                c.CizgiFilmID = Convert.ToInt32(dr[0].ToString());
                c.CizgiFilmBaslik = dr[2].ToString();
                cizgifilmler.Add(c);
            }

            ViewBag.cizgifilmlerim = cizgifilmler;
            ViewBag.islem = false;

            return View("BolumForm", cfb);

        }

        public ActionResult BolumSil(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            DataTable dt = Sorgular.OYBul("CFBolumBul", id);

            if (dt.Rows.Count <= 0)
                return HttpNotFound();

            Sorgular.OYSil("CFBolumSil", id);

            return RedirectToAction("Listeleme", "CizgiFilm", new { islem = true });
        }

        public ActionResult Bolumler(int id)
        {
            bool b = Sorgular.GuidKontrol(GirisController.g);
            if (b == false)
            {
                return RedirectToAction("Index", "Giris");
            }

            List<CizgiFilmBolum> bolumler = new List<CizgiFilmBolum>();

            DataTable dt = Sorgular.SorguYap("CFBolumlerimiGoster");
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr[5].ToString()) == id)
                {
                    CizgiFilmBolum cb = new CizgiFilmBolum();
                    cb.CizgiFilmBolumID = Convert.ToInt32(dr[0].ToString());
                    cb.CizgiFilmBolumBaslik = dr[1].ToString();
                    cb.CizgiFilmBolumSezonNo = dr[2].ToString();
                    cb.CizgiFilmBolumIzlenmeSayisi = Convert.ToInt32(dr[3].ToString());
                    bolumler.Add(cb);
                }

            }
            ViewBag.bolumlerim = bolumler;
            return View();
        }

        //public ActionResult CizgiFilmResim()
        //{
        //    bool b = Sorgular.GuidKontrol(GirisController.g);
        //    if (b == false)
        //    {
        //        return RedirectToAction("Index", "Giris");
        //    }

        //    List<CizgiFilm> cizgifilmler = new List<CizgiFilm>();

        //    DataTable dt = Sorgular.SorguYap("CizgiFilmleriListele");
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        CizgiFilm c = new CizgiFilm();
        //        c.CizgiFilmID = Convert.ToInt32(dr[0].ToString());
        //        c.CizgiFilmBaslik = dr[2].ToString();
        //        cizgifilmler.Add(c);
        //    }
        //    ViewBag.cizgifilmler = cizgifilmler;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ResimEkle(HttpPostedFileBase uploadfile, int cizgifilmid)
        //{
        //    string filePath = "";
        //    if (uploadfile.ContentLength > 0)
        //    {
        //        filePath = Path.Combine(Server.MapPath("~/Images"), Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadfile.FileName));
        //        uploadfile.SaveAs(filePath);
        //    }

        //    Image a = Image.FromFile(filePath);
        //    Bitmap foto = ResimAyarlama.FotografBoyutlandir(a, 200, 150);
        //    byte[] kapakfoto = ResimAyarlama.ImageByteCevir(foto);

        //    Sorgular.ResimEkle("CizgiFilmResimGuncelle", cizgifilmid, kapakfoto);

        //    return RedirectToAction("Index", "Home", new { islem = true });
        //}

    }
}