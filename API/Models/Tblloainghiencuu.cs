using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblloainghiencuu
    {
        public Tblloainghiencuu()
        {
            Tbldetais = new HashSet<Tbldetai>();
        }

        public int Id { get; set; }
        public string Tenloai { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tbldetai> Tbldetais { get; set; }
    }
}
