//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UrunSatisPortali.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Yorum
    {
        public int YorumId { get; set; }
        public string YorumMetni { get; set; }
        public Nullable<double> Puani { get; set; }
        public Nullable<int> KullaniciId { get; set; }
        public Nullable<int> UrunId { get; set; }
    
        public virtual Kullanici Kullanici { get; set; }
        public virtual Urun Urun { get; set; }
    }
}
