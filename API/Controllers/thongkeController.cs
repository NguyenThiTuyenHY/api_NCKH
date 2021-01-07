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
    public class thongkeController : ControllerBase
    {
        [Route("thongke_admin")]
        [HttpGet]
        public total thongke_admin()
        {
            total result = new total();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                result.total_canbo = db.Tblnhanviens.Count();
                result.total_detai = db.Tbldetais.Count();
                result.phantram_detai = db.Tbldetais.Where(x=>x.Tinhtrang == 3).Count() / db.Tbldetais.Count();
                result.total_phanhoi = db.Tblphanhois.Count();
            }
            return result;
        }
        [Route("thongke_admin_luotxem_loaitt")]
        [HttpGet]
        public List<thongke_admin_luotxem_loaitt> thongke_admin_luotxem_loaitt()
        {
            List<thongke_admin_luotxem_loaitt> result = new List<thongke_admin_luotxem_loaitt>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                List<Tblloaitt> dsltt = db.Tblloaitts.ToList();
                List<Tbltintuc> dstt = db.Tbltintucs.ToList();
                foreach(Tblloaitt ltt in dsltt)
                {
                    thongke_admin_luotxem_loaitt a = new thongke_admin_luotxem_loaitt();
                    a.id = ltt.Id;
                    a.loaitt = ltt.Tenloaitt;
                    a.soluong = dstt.Where(x => x.Idloai == ltt.Id).Sum(x=>x.Luotem);
                    result.Add(a);
                }
            }
            return result;
        }
        [Route("thongke_detai_trongnam_hh")]
        [HttpGet]
        public List<thongke_admin_luotxem_loaitt> thongke_detai_trongnam_hh()
        {
            using(sql_NCKHContext db = new sql_NCKHContext())
            {
                List<thongke_admin_luotxem_loaitt> result = new List<thongke_admin_luotxem_loaitt>();
                List<Tbldetai> dt = db.Tbldetais.Where(x => x.Tinhtrang < 5 && x.Tinhtrang > 0).ToList();
                DateTime date = DateTime.Now;
                for (int i = 1; i <= 12; i++)
                {
                    thongke_admin_luotxem_loaitt a = new thongke_admin_luotxem_loaitt();
                    a.id = i;
                    a.loaitt = "Tháng " + i;
                    a.soluong = dt.Where(x => x.Thoigianbd.Year == date.Year && x.Thoigianbd.Month == i).Count();
                    result.Add(a);
                }
                return result;
            }
        }
        [Route("thongke_detai_giahan")]
        [HttpGet]
        public List<Tbldetai> thongke_detai_giahan(int pageindex, int pagesize)
        {
            List<Tbldetai> dt = new List<Tbldetai>();
            using(sql_NCKHContext db = new sql_NCKHContext())
            {
                int index = (pageindex - 1) * pagesize;
                dt = db.Tbldetais.Where(x => x.Tinhtrang == 4).ToList();
                if (dt.Count()==0)
                {
                    return null;
                }
                else
                {
                    return db.Tbldetais.Where(x => x.Tinhtrang == 4).Skip(index).Take(pagesize).ToList();
                }
            }
        } 
    }
}
