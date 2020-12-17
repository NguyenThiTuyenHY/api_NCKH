using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class detainhom
    {
        public int Id { get; set; }
        public string Tendetai { get; set; }
        public int? Idnv { get; set; }
        public string Sohieu { get; set; }
        public int? Kqbv { get; set; } // 1 - Xuất sắc, 2 - Giỏi, 3 - Khá, 4 - Trung bình, 5 - Kém
        public int? Capbv { get; set; } // 1 - Đề tài cấp bộ, 2 - Đề tài KHCNcấp trường, 3 - Đề tài cấp nhà nước
        public DateTime? Thoigianbd { get; set; }
        public DateTime? Thoigiankt { get; set; }
        public DateTime? Thoigiannt { get; set; }
        public DateTime? Thoigiangiahan { get; set; } // Thời gian gia hạn
        public int? Tinhtrang { get; set; } // 1 - chua duyet, 2 - duyet, 3 - hoan thanh, 4 - xin thoi gian, 5 - ap dung thuc tien   
    }
}
