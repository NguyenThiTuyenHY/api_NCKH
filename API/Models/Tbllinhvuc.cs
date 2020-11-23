using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tbllinhvuc
    {
        public Tbllinhvuc()
        {
            Tbldetais = new HashSet<Tbldetai>();
        }

        public int Id { get; set; }
        public string Tenlinhvuc { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tbldetai> Tbldetais { get; set; }
    }
}
