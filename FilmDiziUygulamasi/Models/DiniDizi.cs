using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class DiniDizi
    {
        public int DiniDiziID { get; set; }

        public string DiniDiziBaslik { get; set; }

        public string DiniDiziKonu { get; set; }

        public byte[] DiniDiziKapakFoto { get; set; }

        public int DiniDiziBaslangicYili { get; set; }

        public string DiniDiziIMDB { get; set; }

        public float DiniDiziBegeniOrani { get; set; }

        public int DiniDiziIzlenmeSayisi { get; set; }

        public int DiniDiziEkleyenID { get; set; }

        public string DiniDiziOyuncular { get; set; }

        public string DiniDiziYonetmenler { get; set; }

        public string DiniDiziOyuncularID { get; set; }

        public string DiniDiziYonetmenlerID { get; set; }

        public string DiniDiziEkleyenAd { get; set; }
    }
}