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
    
    public partial class Resim
    {
        public int ResimId { get; set; }
        public string ResimAdi { get; set; }
        public Nullable<int> UrunId { get; set; }
    
        public virtual Urun Urun { get; set; }
    }
}
