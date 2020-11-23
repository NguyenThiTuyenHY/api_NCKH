using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblhoatdongnckh
    {
        public Tblhoatdongnckh()
        {
            Tbldetais = new HashSet<Tbldetai>();
        }

        public int Id { get; set; }
        public string Tenhdnckh { get; set; }
        public int? Dinhmuc { get; set; }
        public int? Dmhtkinhphi { get; set; }
        public string Ghichu { get; set; }
        public int? Idloaihd { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblloaihoatdong IdloaihdNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tbldetai> Tbldetais { get; set; }
    }
}
