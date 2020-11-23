using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models.common
{
    public class phanhoi
    {
        public int Id { get; set; }
        public int? Iddetai { get; set; }
        public int? Idnv { get; set; }
        public string Noidung { get; set; }
        public DateTime? Ngay { get; set; }
        public string Ghichu { get; set; }
        public string Tendetai { get; set; }
        public string Hoten { get; set; }
        public string Hinhanh { get; set; }
    }
}
