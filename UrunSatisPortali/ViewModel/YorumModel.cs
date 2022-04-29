using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisPortali.ViewModel
{
    public class YorumModel
    {
        public int YorumId { get; set; }
        public string YorumMetni { get; set; }
        public Nullable<double> Puani { get; set; }
        public Nullable<int> KullaniciId { get; set; }
        public Nullable<int> UrunId { get; set; }
    }
}