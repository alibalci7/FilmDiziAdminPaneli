using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class Film
    {
        //  FilmID, FilmLink, FilmBaslik, FilmKonu, FilmKapakFoto, FilmYapimYili, FilmDil, FilmIMDB, FilmSure, FilmIzlenmeSayisi, FilmBegeniOrani, FilmEkleyenID
        public int FilmID { get; set; }

        public string FilmLink { get; set; }

        public string FilmBaslik { get; set; }

        public string FilmKonu { get; set; }

        public byte[] FilmKapakFoto { get; set; }

        public bool FilmAltYazi { get; set; }

        public int FilmYapimYili { get; set; }

        public string FilmDil { get; set; }

        public string FilmIMDB { get; set; }

        public string FilmSure { get; set; }

        public int FilmIzlenmeSayisi { get; set; }

        public float FilmBegeniOrani { get; set; }

        public int FilmEkleyenID { get; set; }

        public string FilmKategoriler { get; set; }

        public string FilmOyuncular { get; set; }

        public string FilmYonetmenler { get; set; }

        public string FilmKategorilerID { get; set; }

        public string FilmOyuncularID { get; set; }

        public string FilmYonetmenlerID { get; set; }

        public string FilmEkleyenAd { get; set; }

    }
}