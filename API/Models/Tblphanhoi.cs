using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblphanhoi
    {
        public int Id { get; set; }
        public int? Iddetai { get; set; }
        public int? Idnv { get; set; }
        public string Noidung { get; set; }
        public DateTime? Ngay { get; set; }
        public string Ghichu { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tbldetai IddetaiNavigation { get; set; }
    }
}
