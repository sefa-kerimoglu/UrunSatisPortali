using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisPortali.ViewModel
{
    public class UrunModel
    {
        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public Nullable<int> KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public string MarkaAdi { get; set; }
        public Nullable<int> Stok { get; set; }
        public string Kargo { get; set; }
        public Nullable<int> FiyatId { get; set; }
        public Nullable<int> KullaniciId { get; set; }

        public List<ResimModel> resimbilgi { get; set; }
        public List<YorumModel> yorumbilgi { get; set; }
    }
}