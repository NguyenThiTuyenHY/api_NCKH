
using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tblcongtac
    {
        public int Id { get; set; }
        public int? Idnv { get; set; }
        public string Noict { get; set; }
        public DateTime Ngaybad { get; set; }
        public DateTime Ngaykt { get; set; }
        public string Chucvu { get; set; }
        public string Chucdanh { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblnhanvien IdnvNavigation { get; set; }
    }
}
