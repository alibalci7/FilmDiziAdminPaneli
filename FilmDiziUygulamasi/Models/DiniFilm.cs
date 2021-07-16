using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class DiniFilm
    {
        public int DiniFilmID { get; set; }

        public string DiniFilmLink { get; set; }

        public string DiniFilmBaslik { get; set; }

        public string DiniFilmKonu { get; set; }

        public byte[] DiniFilmKapakFoto { get; set; }

        public bool DiniFilmAltYazi { get; set; }


        public int DiniFilmYapimYili { get; set; }

        public string DiniFilmDil { get; set; }

        public string DiniFilmIMDB { get; set; }

        public string DiniFilmSure { get; set; }

        public int DiniFilmIzlenmeSayisi { get; set; }

        public float DiniFilmBegeniOrani { get; set; }

        public int DiniFilmEkleyenID { get; set; }

        public string DiniFilmOyuncular { get; set; }

        public string DiniFilmYonetmenler { get; set; }

        public string DiniFilmOyuncularID { get; set; }

        public string DiniFilmYonetmenlerID { get; set; }

        public string DiniFilmEkleyenAd { get; set; }
    }
}