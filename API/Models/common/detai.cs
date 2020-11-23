using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class detai
    {
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
        public int? Tinhtrang { get; set; } // 1 - chua duyet, 2 - duyet, 3 - hoan thanh, 4 - xin thoi gian, 5 - ap dung thuc tien   
        public string Ghichu { get; set; } // Hình thức, quy mô, địa chỉ áp dụng
        public int? Idlinhvuc { get; set; }
        public int? Idloainv { get; set; }
        public int? Idhdnckh { get; set; }
        public int? Uytin { get; set; }
        public DateTime? Thoigianbd { get; set; }
        public DateTime? Thoigiankt { get; set; }
        public DateTime? Thoigiannt { get; set; } 
        public DateTime? Thoigiangiahan { get; set; } // Thời gian gia hạn
        public int? Kqbv { get; set; } // 1 - Xuất sắc, 2 - Giỏi, 3 - Khá, 4 - Trung bình, 5 - Kém
        public int? Capbv { get; set; } // 1 - Đề tài cấp bộ, 2 - Đề tài KHCNcấp trường, 3 - Đề tài cấp nhà nước
        public string Noidang { get; set; }
        public int? Namdang { get; set; }
        public string Tenhdnckh { get; set; }
        public string Tenlinhvuc { get; set; }
        public string Tenloainv { get; set; }
        
    }
}
