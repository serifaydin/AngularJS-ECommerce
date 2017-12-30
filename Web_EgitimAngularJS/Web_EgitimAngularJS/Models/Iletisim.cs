using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_EgitimAngularJS.Models
{
    public class Iletisim
    {
        public int TabloID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Telefon { get; set; }
        public string Sirket { get; set; }
        public string Mesaj { get; set; }
        public DateTime Tarih { get; set; }
    }
}