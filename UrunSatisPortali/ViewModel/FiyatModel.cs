using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UrunSatisPortali.ViewModel
{
    public class FiyatModel
    {
        public int FiyatId { get; set; }
        public Nullable<decimal> AlisFiyati { get; set; }
        public Nullable<decimal> SatisFiyati { get; set; }
    }
}