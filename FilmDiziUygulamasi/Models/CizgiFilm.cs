using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class CizgiFilm
    {
        public int CizgiFilmID { get; set; }

        public string CizgiFilmBaslik { get; set; }

        public byte[] CizgiFilmKapakFoto { get; set; }

        public int CizgiFilmIzlenmeSayisi { get; set; }

        public int CizgiFilmEkleyenID { get; set; }

        public string CizgiFilmEkleyenAd { get; set; }

    }
}