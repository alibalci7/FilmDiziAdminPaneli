using System;
using System.Data;
using System.Data.SqlClient;

namespace FilmDiziUygulamasi.Controllers
{
    public class Sorgular
    {
        public static bool GuidGuncelleme(Guid g)
        {
            SqlCommand cmd = new SqlCommand(string.Format("GuidGuncelleme"), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@guid", g.ToString());
            cmd.Parameters.AddWithValue("@id", GirisController.YoneticiID);
            return Tools.Exec(cmd);
        }

        public static bool GuidKontrol(Guid g)
        {
            SqlDataAdapter adp = new SqlDataAdapter(string.Format("YoneticileriListele"), Tools.Baglanti);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                if (dr[3].ToString() == g.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DiziEkle(string str, string dizibaslik, string dizikonu, byte[] dizikapak, int dizibaslangicyili, string diziimdb, int diziekleyen)
        {
            SqlCommand cmd = new SqlCommand(string.Format(str), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dizibaslik", dizibaslik);
            cmd.Parameters.AddWithValue("@dizikonu", dizikonu);
            cmd.Parameters.AddWithValue("@dizikapak", dizikapak);
            cmd.Parameters.AddWithValue("@dizibaslangicyili", dizibaslangicyili);
            cmd.Parameters.AddWithValue("@diziimdb", diziimdb);
            cmd.Parameters.AddWithValue("@diziekleyen", diziekleyen);
            return Tools.Exec(cmd);
        }

        public static bool BolumEkle(string str, string bolumlink, string bolumbaslik, string bolumsezonno, int diziid, int bolumekleyen)
        {
            SqlCommand cmd = new SqlCommand(str, Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bolumlink", bolumlink);
            cmd.Parameters.AddWithValue("@bolumbaslik", bolumbaslik);
            cmd.Parameters.AddWithValue("@bolumsezonno", bolumsezonno);
            cmd.Parameters.AddWithValue("@diziid", diziid);
            cmd.Parameters.AddWithValue("@bolumekleyen", bolumekleyen);
            return Tools.Exec(cmd);
        }

        public static bool FilmEkle(string str, string filmlink, string filmbaslik, string filmkonu, byte[] filmkapak, bool filmaltyazi, int filmyapimyili, string filmdil, string filmimdb, string filmsure, int filmekleyen)
        {
            SqlCommand cmd = new SqlCommand(string.Format(str), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@filmlink", filmlink);
            cmd.Parameters.AddWithValue("@filmbaslik", filmbaslik);
            cmd.Parameters.AddWithValue("@filmkonu", filmkonu);
            cmd.Parameters.AddWithValue("@filmkapak", filmkapak);
            cmd.Parameters.AddWithValue("@filmaltyazi", filmaltyazi);
            cmd.Parameters.AddWithValue("@filmyapimyili", filmyapimyili);
            cmd.Parameters.AddWithValue("@filmdil", filmdil);
            cmd.Parameters.AddWithValue("@filmimdb", filmimdb);
            cmd.Parameters.AddWithValue("@filmsure", filmsure);
            cmd.Parameters.AddWithValue("@filmekleyen", filmekleyen);
            return Tools.Exec(cmd);
        }

        public static bool CizgiFilmEkle(string cizgifilmbaslik, byte[] cizgifilmkapak, int cizgifilmekleyen)
        {
            SqlCommand cmd = new SqlCommand(string.Format("CizgiFilmEkle"), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cizgifilmbaslik", cizgifilmbaslik);
            cmd.Parameters.AddWithValue("@cizgifilmkapak", cizgifilmkapak);
            cmd.Parameters.AddWithValue("@cizgifilmekleyen", cizgifilmekleyen);
            return Tools.Exec(cmd);
        }

        public static bool DiziGuncelle(string str, int diziid, string dizibaslik, byte[] dizikapak, string dizikonu, int dizibaslangicyili, string diziimdb)
        {
            SqlCommand cmd = new SqlCommand(string.Format(str), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@diziid", diziid);
            cmd.Parameters.AddWithValue("@dizibaslik", dizibaslik);
            cmd.Parameters.AddWithValue("@dizikonu", dizikonu);
            cmd.Parameters.AddWithValue("@dizikapak", dizikapak);
            cmd.Parameters.AddWithValue("@dizibaslangicyili", dizibaslangicyili);
            cmd.Parameters.AddWithValue("@diziimdb", diziimdb);
            return Tools.Exec(cmd);
        }

        public static bool BolumGuncelle(string str, int bolumid, string bolumlink, string bolumbaslik, string bolumsezonno, int diziid)
        {
            SqlCommand cmd = new SqlCommand(str, Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@bolumid", bolumid);
            cmd.Parameters.AddWithValue("@bolumlink", bolumlink);
            cmd.Parameters.AddWithValue("@bolumbaslik", bolumbaslik);
            cmd.Parameters.AddWithValue("@bolumsezonno", bolumsezonno);
            cmd.Parameters.AddWithValue("@diziid", diziid);
            return Tools.Exec(cmd);
        }

        public static bool FilmGuncelle(string str, int filmid, string filmlink, string filmbaslik, string filmkonu, byte[] filmkapak, bool filmaltyazi, int filmyapimyili, string filmdil, string filmimdb, string filmsure)
        {
            SqlCommand cmd = new SqlCommand(string.Format(str), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@filmid", filmid);
            cmd.Parameters.AddWithValue("@filmlink", filmlink);
            cmd.Parameters.AddWithValue("@filmbaslik", filmbaslik);
            cmd.Parameters.AddWithValue("@filmkonu", filmkonu);
            cmd.Parameters.AddWithValue("@filmkapak", filmkapak);
            cmd.Parameters.AddWithValue("@filmaltyazi", filmaltyazi);
            cmd.Parameters.AddWithValue("@filmyapimyili", filmyapimyili);
            cmd.Parameters.AddWithValue("@filmdil", filmdil);
            cmd.Parameters.AddWithValue("@filmimdb", filmimdb);
            cmd.Parameters.AddWithValue("@filmsure", filmsure);
            return Tools.Exec(cmd);
        }

        public static bool CizgiFilmGuncelle(int cizgifilmid, byte[] cizgifilmkapak, string cizgifilmbaslik)
        {
            SqlCommand cmd = new SqlCommand(string.Format("CizgiFilmGuncelle"), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cizgifilmid", cizgifilmid);
            cmd.Parameters.AddWithValue("@cizgifilmbaslik", cizgifilmbaslik);
            cmd.Parameters.AddWithValue("@cizgifilmkapak", cizgifilmkapak);
            return Tools.Exec(cmd);
        }

        public static bool FilmOYKEkle(string sr, int id)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            return Tools.Exec(cmd);
        }

        public static bool FilmOYKEkle(string sr, int id, int fid)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@fid", fid);
            return Tools.Exec(cmd);
        }

        public static DataTable SorguYap(string sr)
        {
            SqlDataAdapter adp = new SqlDataAdapter(string.Format(sr), Tools.Baglanti);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        public static bool YoneticiEkle(string sr, string yoneticiadi, string yoneticiparola, int yoneticitip)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@yoneticiadi", yoneticiadi);
            cmd.Parameters.AddWithValue("@yoneticiparola", yoneticiparola);
            cmd.Parameters.AddWithValue("@yoneticitip", yoneticitip);
            return Tools.Exec(cmd);
        }

        public static DataTable HesabimGoruntule(string sr)
        {
            SqlDataAdapter adp = new SqlDataAdapter(string.Format(sr), Tools.Baglanti);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@id", GirisController.YoneticiID);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
        }

        public static bool HesabimGuncelle(string sr, string yoneticiadi, string yoneticiparola)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ad", yoneticiadi);
            cmd.Parameters.AddWithValue("@parola", yoneticiparola);
            cmd.Parameters.AddWithValue("@id", GirisController.YoneticiID);
            return Tools.Exec(cmd);
        }

        public static bool KategoriEkle(string sr,int ustid, string ad)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ustid", ustid);
            cmd.Parameters.AddWithValue("@ad", ad);
            return Tools.Exec(cmd);
        }

        public static bool OYGuncelle(string sr, int gelenid, string adsoyad)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@gelenid", gelenid);
            cmd.Parameters.AddWithValue("@adsoyad", adsoyad);
            return Tools.Exec(cmd);
        }      

        public static bool OYEkle(string sr, string adsoyad)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@adsoyad", adsoyad);
            return Tools.Exec(cmd);
        }

        public static DataTable OYBul(string sr, int gelenid)
        {
            SqlDataAdapter adp = new SqlDataAdapter(string.Format(sr), Tools.Baglanti);
            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
            adp.SelectCommand.Parameters.AddWithValue("@gelenid", gelenid);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            return dt;
           
        }

        public static void OYSil(string sr, int gelenid)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@gelenid", gelenid);
            Tools.Exec(cmd);
        }

        public static void ResimEkle(string sr, int gelenid, byte[] resim)
        {
            SqlCommand cmd = new SqlCommand(string.Format(sr), Tools.Baglanti);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", gelenid);
            cmd.Parameters.AddWithValue("@resim", resim);
            Tools.Exec(cmd);
        }

    }
}