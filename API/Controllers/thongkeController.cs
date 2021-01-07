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
        public List<double> thongke_admin()
        {
            List<double> result = new List<double>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                double a = db.Tblnhanviens.Count();
                double b = db.Tbldetais.Count();
                double c = db.Tbldetais.Where(x=>x.Tinhtrang == 3).Count() / db.Tbldetais.Count();
                double d = db.Tblphanhois.Count();
                db.Add(a);
                db.Add(b);
                db.Add(c);
                db.Add(d);
            }
            return result;
        }
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
                    a.soluong = dstt.Where(x => x.Idloai == ltt.Id).Count();
                }
            }
            return result;
        } 
        public List<thongke_admin_luotxem_loaitt> thongke_detai_trongnam_hh()
        {
            using(sql_NCKHContext db = new sql_NCKHContext())
            {
                List<thongke_admin_luotxem_loaitt> result = new List<thongke_admin_luotxem_loaitt>();
                List<Tbldetai> dt = db.Tbldetais.Where(x => x.Tinhtrang <= 5 && x.Tinhtrang > 0).ToList();
                for (int i = 1; i <= 12; i++)
                    {
                        thongke_admin_luotxem_loaitt a = new thongke_admin_luotxem_loaitt();
                        a.id = i;
                        a.loaitt = "Tháng " + i;
                }
                return result;
            }
        }
        public List<Tbldetai> thongke_detai_giahan(int pageindex, int pagesize)
        {
            using(sql_NCKHContext db = new sql_NCKHContext())
            {
                int index = (pageindex - 1) * pagesize;
                return db.Tbldetais.Where(x => x.Tinhtrang == 4).Skip(index).Take(pagesize).ToList();
            }
        } 
    }
}
