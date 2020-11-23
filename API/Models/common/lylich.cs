using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class lylich
    {
        public int Id { get; set; }
        public int? Idnv { get; set; }
        public string Hocham { get; set; }
        public int? Namphong { get; set; }
        public string Hocvi { get; set; }
        public int? Namcap { get; set; }
        // Dai hoc
        public string Hedaotao { get; set; }
        public string Noidaotao { get; set; }
        public string Nganhhoc { get; set; }
        public string Nuocdaotao { get; set; }
        public int? Namtotnghiep { get; set; }
        public int? Bangdaihoc { get; set; }
        public int? Namtotnghiep2 { get; set; }
        // sau dai hoc
        public string Bangthacsi { get; set; }
        public int? Namcapbang { get; set; }
        public string Noidaotaoa2 { get; set; }
        public int? Bangtiensi { get; set; }
        public int? Namcapbang2 { get; set; }
        public string Noidaotao2 { get; set; }
        public string Tenchuyende { get; set; } // ten chuyen de luan an bac cao nhat
        // Ly lich so luoc
        public string Hinhanh { get; set; }
        public int? Gioitinh { get; set; }
        public string Hoten { get; set; }
        public DateTime? Ngaysinh { get; set; }
        public string Noisinh { get; set; }
        public string Quequan { get; set; }
        public string Dantoc { get; set; }
        public string Noiohnay { get; set; } //Dia chi lien lac
        public string Dienthoai { get; set; }
        public string Trinhdonn { get; set; }
        public string Tinhoc { get; set; }
        // chuc vu
        public int? Idpban { get; set; }
        public string Tenphongban { get; set; }
        public int? Idchucvu { get; set; }
        public string Tenchucvu { get; set; }

        // Trinh do cao nhat
        public string Tdcaonhat { get; set; }
        public List<Tblboiduong> dsboiduong { get; set; }
        public List<Tblcongtac> dscongtac { get; set; }
        public List<Tbldetai> dsdetai { get; set; }
    }
}
