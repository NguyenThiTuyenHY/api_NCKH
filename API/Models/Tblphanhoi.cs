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
        public int Trangthai { get; set; } // 1 Kích hoạt, 2 vô hiệu hoá
        public string Hinhanh { get; set; }
        public string Token_access { get; set; }
        public string Id_social { get; set; }
        public int? Loai { get; set; } // 
        public string Fistname { get; set; }
        public string Lastname { get; set; }
        public int? Number { get; set; }
        public DateTime?  Ngay_update { get; set; }
        public int Last_user { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tbldetai IddetaiNavigation { get; set; }
    }
}
