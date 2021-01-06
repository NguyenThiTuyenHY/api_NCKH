using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class nguoidung
    {
        public int Id { get; set; }
        public string Taikhoan { get; set; }
        public string Matkhau { get; set; }
        public string Hinhanh { get; set; }
        public string Token { get; set; }
        public int Role { get; set; }
        public int? Idnv { get; set; }
    }
}
