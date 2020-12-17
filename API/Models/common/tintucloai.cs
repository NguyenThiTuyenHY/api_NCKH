using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class tintucloai
    {
        public int Id { get; set; }
        public string Tieude { get; set; }
        public string Hinhanh { get; set; }
        public int? Idloai { get; set; }
        public string Noidung { get; set; }
        public int? Luotem { get; set; }
        public string Tenloaitt { get; set; }
        public DateTime? Ngaydang { get; set; }
    }
}
