using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace API.Models
{
    public partial class Tblchucvu
    {
        public Tblchucvu()
        {
            Tblnhanviens = new HashSet<Tblnhanvien>();
        }

        public int Id { get; set; }
        public string Tenchucvu { get; set; }
        public string Dieukien { get; set; }
        public int? Dinhmuc { get; set; }
        public string Ghichu { get; set; }
        [JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblnhanvien> Tblnhanviens { get; set; }
    }
}
