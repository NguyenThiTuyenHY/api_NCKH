using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class nhanvien
    {
        public int Id { get; set; }
        public string Hoten { get; set; }
        public int? Gioitinh { get; set; }
        public string Dienthoai { get; set; }
        public string Email { get; set; }
        public int? Idpban { get; set; }
        public string pban { get; set; }
    }
}
