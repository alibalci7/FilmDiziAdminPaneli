using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class Oyuncular
    {
        [Display(Name = "Oyuncu ID")]
        public int OyuncuID { get; set; }

        [Display(Name = "Oyuncu Adı Soyadı")]
        public string OyuncuAdSoyad { get; set; }

    }
}