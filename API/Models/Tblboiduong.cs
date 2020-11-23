using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblboiduong
    {
        public int Id { get; set; }
        public int? Idnv { get; set; }
        public string Noibd { get; set; }
        public DateTime? Ngaybd { get; set; }
        public DateTime? Ngaykt { get; set; }
        public string Noidung { get; set; }
        [JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblnhanvien IdnvNavigation { get; set; }
    }
}
