using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblphongban
    {
        public Tblphongban()
        {
            Tblnhanviens = new HashSet<Tblnhanvien>();
        }

        public int Id { get; set; }
        public string Tenphongban { get; set; }
        public int? Iddv { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tbldonvi IddvNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblnhanvien> Tblnhanviens { get; set; }
    }
}
