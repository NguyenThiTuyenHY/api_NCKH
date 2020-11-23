using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tbldonvi
    {
        public Tbldonvi()
        {
            Tblphongbans = new HashSet<Tblphongban>();
        }

        public int Id { get; set; }
        public string Tendv { get; set; }
        public int? Tyle { get; set; }
        public string Ghichu { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]

        public virtual ICollection<Tblphongban> Tblphongbans { get; set; }
    }
}
