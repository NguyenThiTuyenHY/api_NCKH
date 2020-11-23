using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblloainhiemvu
    {
        public Tblloainhiemvu()
        {
            Tbldetais = new HashSet<Tbldetai>();
        }

        public int Id { get; set; }
        public string Tenloainv { get; set; }
        public string Ghichu { get; set; }
        public double? C { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tbldetai> Tbldetais { get; set; }
    }
}
