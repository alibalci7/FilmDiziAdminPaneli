using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class CizgiFilmBolum
    {
        public int CizgiFilmBolumID { get; set; }

        public int CizgiFilmID { get; set; }

        public string CizgiFilmAdi { get; set; }

        public string CizgiFilmBolumLink { get; set; }

        public string CizgiFilmBolumBaslik { get; set; }

        public string CizgiFilmBolumSezonNo { get; set; }

        public int CizgiFilmBolumIzlenmeSayisi { get; set; }

        public int CizgiFilmBolumEkleyenID { get; set; }
    }
}