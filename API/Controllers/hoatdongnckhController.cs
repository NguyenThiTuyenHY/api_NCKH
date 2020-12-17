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
    public class hoatdongnckhController : ControllerBase
    {
        [Route("get_hoatdongnckh_pagesize")]
        [HttpGet]
        public datatable<hoadongnc> get_hoatdongnckh_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<hoadongnc> dv = new datatable<hoadongnc>();
            List<hoadongnc> ds = new List<hoadongnc>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                int index = (pageindex - 1) * pagesize;
                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tblhoatdongnckhs.Join(db.Tblloaihoatdongs, hd => hd.Idloaihd, lhd => lhd.Id, (hd, lhd) => new hoadongnc
                    {
                        Id = hd.Id,
                        Tenhdnckh = hd.Tenhdnckh,
                        Dinhmuc = hd.Dinhmuc,
                        Dmhtkinhphi = hd.Dmhtkinhphi,
                        Ghichu = hd.Ghichu,
                        Tenkhoahoc = lhd.Tenloaihd
                    }).Where(x => x.Tenhdnckh.IndexOf(search) >= 0).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblhoatdongnckhs.Where(x => x.Tenhdnckh.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tblhoatdongnckhs.Join(db.Tblloaihoatdongs, hd => hd.Idloaihd, lhd => lhd.Id, (hd, lhd) => new hoadongnc
                    {
                        Id = hd.Id,
                        Tenhdnckh = hd.Tenhdnckh,
                        Dinhmuc = hd.Dinhmuc,
                        Dmhtkinhphi = hd.Dmhtkinhphi,
                        Ghichu = hd.Ghichu,
                        Tenkhoahoc = lhd.Tenloaihd
                    }).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblhoatdongnckhs.Count();
                }
                dv.result = ds;

            }
            return dv;
        }
        [Route("get_hoatdongnckh_all")]
        [HttpGet]
        public List<Tblhoatdongnckh> get_hoatdongnckh_all()
        {
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                return db.Tblhoatdongnckhs.ToList();
            }
        }
        [Route("get_hoatdongnckh_id/{id}")]
        [HttpGet]
        public List<Tblhoatdongnckh> get_hoatdongnckh_id(int id)
        {
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                return db.Tblhoatdongnckhs.ToList();
            }
        }
        [Route("create_hoatdongnckh")]
        [HttpPost]
        public bool create_hoatdongnckh([FromBody] Tblhoatdongnckh nc)
        {
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                try
                {
                    db.Tblhoatdongnckhs.Add(nc);
                    db.SaveChanges();
                    return true;
                }
                catch(Exception)
                {
                    return false;
                }
            }
        }
        [Route("edit_hoatdongnckh/{id}")]
        [HttpPut]
        public bool edit_hoatdongnckh(int id,[FromBody] Tblhoatdongnckh nc)
        {
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                try
                {
                    Tblhoatdongnckh kh = db.Tblhoatdongnckhs.SingleOrDefault(x => x.Id==id);
                    if (kh == null)
                        return false;
                    kh.Tenhdnckh = nc.Tenhdnckh;
                    kh.Dinhmuc = nc.Dinhmuc;
                    kh.Dmhtkinhphi = nc.Dmhtkinhphi;
                    kh.Ghichu = nc.Ghichu;
                    kh.Idloaihd = nc.Idloaihd;
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        [Route("delete_hoatdongnckh/{id}")]
        [HttpDelete]
        public bool delete_hoatdongnckh(int id, [FromBody] Tblhoatdongnckh nc)
        {
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                try
                {
                    Tblhoatdongnckh kh = db.Tblhoatdongnckhs.SingleOrDefault(x => x.Id == id);
                    if (kh == null)
                        return false;
                    db.Tblhoatdongnckhs.Remove(kh);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
