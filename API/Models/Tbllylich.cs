using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tbllylich
    {
        public int Id { get; set; }
        public int? Idnv { get; set; }
        public string Hocham { get; set; }
        public int? Namphong { get; set; }
        public string Hocvi { get; set; }
        public int? Namcap { get; set; }
        public string Hedaotao { get; set; }
        public string Noidaotao { get; set; }
        public string Nganhhoc { get; set; }
        public string Nuocdaotao { get; set; }
        public int? Namtotnghiep { get; set; }
        public int? Bangdaihoc { get; set; }
        public int? Namtotnghiep2 { get; set; }
        public string Bangthacsi { get; set; }
        public int? Namcapbang { get; set; }
        public string Noidaotaoa2 { get; set; }
        public int? Bangtiensi { get; set; }
        public int? Namcapbang2 { get; set; }
        public string Noidaotao2 { get; set; }
        public string Tenchuyende { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblnhanvien IdnvNavigation { get; set; }
    }
}
