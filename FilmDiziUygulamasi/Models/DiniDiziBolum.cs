using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class DiniDiziBolum
    {
        public int DiniDiziBolumID { get; set; }

        public int DiniDiziID { get; set; }

        public string DiniDiziAdi { get; set; }

        public string DiniDiziBolumLink { get; set; }

        public string DiniDiziBolummBaslik { get; set; }

        public string DiniDiziBolumSezonNo { get; set; }

        public int DiniDiziIzlenmeSayisi { get; set; }

        public int DiniDiziBolumEkleyenID { get; set; }

    }
}