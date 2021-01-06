using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Taikhoan { get; set; }
        public string Matkhau { get; set; }
        public int? Idrole { get; set; }
        public string Token { get; set; }
        public string Hinhanh { get; set; }
        public int? Idnhanvien { get; set; }
        public int Trangthai { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblnhanvien IdnhanvienNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Role IdroleNavigation { get; set; }
    }
}
