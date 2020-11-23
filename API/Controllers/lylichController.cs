using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class lylichController : ControllerBase
    {
        [Route("get_lylich_id")]
        [HttpGet]
        public lylich get_lylich_id(int id)
        {
            lylich ll = new lylich();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                Tblnhanvien nv = db.Tblnhanviens.SingleOrDefault(x => x.Id == id);
                //db.Tblboiduongs.ToList();
                //ll.dsboiduong = db.Tblboiduongs.Where(x => x.Idnv == id).ToList();
                //ll.dscongtac = db.Tblcongtacs.Where(x => x.Idnv == id).ToList();
                //ll.dsdetai = db.Tbldetais.Where(x => x.Idnv == id).ToList();
                if (!string.IsNullOrEmpty(nv.Hinhanh))
                {
                    ll.Hinhanh = nv.Hinhanh;
                }               
                ll.Gioitinh = nv.Gioitinh;
                ll.Hoten = nv.Hoten;
                ll.Ngaysinh = nv.Ngaysinh;
                ll.Noisinh = nv.Noisinh;
                ll.Quequan = nv.Quequan;
                ll.Dantoc = nv.Dantoc;
                ll.Noiohnay = nv.Noiohnay; //Dia chi lien lac
                ll.Dienthoai = nv.Dienthoai;
                ll.Trinhdonn = nv.Trinhdonn;
                ll.Tinhoc = nv.Tinhoc;
                ll.Idpban = nv.Idpban;
                ll.Tenphongban = db.Tblphongbans.SingleOrDefault(x => x.Id == nv.Idpban).Tenphongban;
                ll.Idchucvu = nv.Idchucvu;
                ll.Tenchucvu = db.Tblchucvus.SingleOrDefault(x => x.Id == nv.Idchucvu).Tenchucvu;
            }
            return ll;
        }
        [Route("edit_lylich")]
        [HttpPut]
        public bool edit_lylich(int id, [FromBody] lylich ll)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbllylich d = db.Tbllyliches.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Hocham = ll.Hocham;
                    d.Namphong = ll.Namphong;
                    d.Hocvi = ll.Hocvi;
                    d.Namcap = ll.Namcap;
                    // Dai hoc
                    d.Hedaotao = ll.Hedaotao;
                    d.Noidaotao = ll.Noidaotao;
                    d.Nganhhoc = ll.Nganhhoc;
                    d.Nuocdaotao = ll.Nuocdaotao;
                    d.Namtotnghiep = ll.Namtotnghiep;
                    d.Bangdaihoc = ll.Bangdaihoc;
                    d.Namtotnghiep2 = ll.Namtotnghiep2;
                    // sau dai hoc
                    d.Bangthacsi = ll.Bangthacsi;
                    d.Namcapbang = ll.Namcapbang;
                    d.Noidaotaoa2 = ll.Noidaotao2;
                    d.Bangtiensi = ll.Bangtiensi;
                    d.Namcapbang2 = ll.Namcapbang2;
                    d.Noidaotao2 = ll.Noidaotao2;
                    d.Tenchuyende = ll.Tenchuyende;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
