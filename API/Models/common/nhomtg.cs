using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class nhomtg
    {
        public int Id { get; set; }
        public int? Iddetai { get; set; }
        public int? Idnv { get; set; }
        public string Chucvu { get; set; }
        public string Hoten { get; set; }
    }
}
