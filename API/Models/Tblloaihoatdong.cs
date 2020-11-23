using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblloaihoatdong
    {
        public Tblloaihoatdong()
        {
            Tblhoatdongnckhs = new HashSet<Tblhoatdongnckh>();
        }

        public int Id { get; set; }
        public string Tenloaihd { get; set; }
        public string Ghichu { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblhoatdongnckh> Tblhoatdongnckhs { get; set; }
    }
}
