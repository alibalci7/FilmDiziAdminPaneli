using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FilmDiziUygulamasi.Models
{
    public class Yonetmenler
    {
        [Display(Name = "Yönetmen ID")]
        public int YonetmenID { get; set; }

        [Display(Name = "Yönetmen Adı Soyadı")]
        public string YonetmenAdSoyad { get; set; }

    }
}