using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class Yoneticiler
    {
        public int YoneticiID { get; set; }

        [Display(Name = "Yönetici Adı")]
        public string YoneticiAdi { get; set; }

        [Display(Name = "Yönetici Parolası")]
        public string YoneticiParola { get; set; }

        public int YoneticiTip { get; set; }

    }
}