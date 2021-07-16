using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class Dizi
    {
        public int DiziID { get; set; }

        public string DiziBaslik { get; set; }

        public string DiziKonu { get; set; }

        public byte[] DiziKapakFoto { get; set; }

        public int DiziBaslangicYili { get; set; }

        public string DiziIMDB { get; set; }

        public float DiziBegeniOrani { get; set; }

        public int DiziIzlenmeSayisi { get; set; }

        public int DiziEkleyenID { get; set; }

        public string DiziOyuncular { get; set; }

        public string DiziYonetmenler { get; set; }

        public string DiziOyuncularID { get; set; }

        public string DiziYonetmenlerID { get; set; }

        public string DiziEkleyenAd { get; set; }
    }
}