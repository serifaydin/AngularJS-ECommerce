using System;

namespace Web_EgitimAngularJS.Models
{
    public class Urunler
    {
        public int TabloID { get; set; }
        public int KategoriID { get; set; }
        public int TedarikciID { get; set; }
        public string Adi { get; set; }
        public string Icerik { get; set; }
        public int Kdv { get; set; }
        public double Fiyat { get; set; }
        public int Adet { get; set; }
        public string ResimUrl { get; set; }
        public DateTime OlusturmaTarih { get; set; }
    }
}