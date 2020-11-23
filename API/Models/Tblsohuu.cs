using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblsohuu
    {
        public Tblsohuu()
        {
            Tblsohuudetais = new HashSet<Tblsohuudetai>();
        }

        public int Id { get; set; }
        public string Tensohuu { get; set; }
        public int? Dmhtkinhphi { get; set; }
        public int? Dm { get; set; }
        public string Ghichu { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblsohuudetai> Tblsohuudetais { get; set; }
    }
}
