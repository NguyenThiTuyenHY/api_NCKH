using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Tbldetai
    {
        public Tbldetai()
        {
            Tblhosos = new HashSet<Tblhoso>();
            Tblnhomtgs = new HashSet<Tblnhomtg>();
            Tblphanhois = new HashSet<Tblphanhoi>();
            Tblsohuudetais = new HashSet<Tblsohuudetai>();
        }

        public int Id { get; set; }
        public string Tendetai { get; set; }
        public int? Idnv { get; set; }
        public int? Idlsp { get; set; }
        public string Tentc { get; set; }
        public string Sohieu { get; set; } // mã số
        public int? Namsx { get; set; }
        public int? Tap { get; set; }
        public int? So { get; set; }
        public int? Trang { get; set; }
        public string Soif { get; set; }
        public string Minhchung { get; set; } // lien ket
        public int? Tinhtrang { get; set; } // 1 - chua duyet, 2 - duyet, 3 - hoan thanh, 4 - xin thoi gian, 5 - ap dung thuc tien, 6- , -1 - huỷ   
        public string Ghichu { get; set; } // Hình thức, quy mô, địa chỉ áp dụng
        public int? Idlinhvuc { get; set; }
        public int? Idloainv { get; set; }
        public int? Idhdnckh { get; set; }
        public int? Uytin { get; set; }
        public DateTime Thoigianbd { get; set; }
        public DateTime Thoigiankt { get; set; }
        public DateTime? Thoigiannt { get; set; }
        public DateTime? Thoigiangiahan { get; set; }
        public int? Kqbv { get; set; } // 1 - Xuất sắc, 2 - Giỏi, 3 - Khá, 4 - Trung bình, 5 - Kém
        public int? Capbv { get; set; } // 1 - Đề tài cấp bộ 2- Đề tài KHCN cấp trường 3 - Đề tài cấp nhà nước
        public string Noidang { get; set; }
        public int? Namdang { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblhoatdongnckh IdhdnckhNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tbllinhvuc IdlinhvucNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblloainhiemvu IdloainvNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual Tblloainghiencuu IdlspNavigation { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblhoso> Tblhosos { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblnhomtg> Tblnhomtgs { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblphanhoi> Tblphanhois { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [System.Xml.Serialization.XmlIgnore]
        public virtual ICollection<Tblsohuudetai> Tblsohuudetais { get; set; }
    }
}
