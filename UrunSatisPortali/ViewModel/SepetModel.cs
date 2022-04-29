using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisPortali.ViewModel
{
    public class SepetModel
    {
        public int SepetId { get; set; }
        public Nullable<int> UrunId { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<int> KullaniciId { get; set; }
    }
}