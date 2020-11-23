using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tbltintuc
    {
        public int Id { get; set; }
        public string Tieude { get; set; }
        public string Hinhanh { get; set; }
        public int? Idloai { get; set; }
        public string Noidung { get; set; }
        public int? Luotem { get; set; }
        //[Newtonsoft.Json.JsonIgnore]
        //[System.Xml.Serialization.XmlIgnore]
        public Tblloaitt IdloaiNavigation { get; set; }
    }
}
