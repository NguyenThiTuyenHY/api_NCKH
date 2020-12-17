using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class donviController : ControllerBase
    {
        [Route("get_donvi_pagesize")]
        [HttpGet]
        public datatable<Tbldonvi> get_donvi_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tbldonvi> dv = new datatable<Tbldonvi>();
            List<Tbldonvi> ds = new List<Tbldonvi>();
            using (sql_NCKHContext db  =new sql_NCKHContext())
            {
                int index = (pageindex - 1) * pagesize;
                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tbldonvis.Where(x => x.Tendv.IndexOf(search) >= 0).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tbldonvis.Where(x => x.Tendv.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tbldonvis.Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tbldonvis.Count();
                }
                dv.result = ds;
                
            }
            return dv;
        }
        [Route("get_donvi_id/{id}")]
        [HttpGet]
        public Tbldonvi get_donvi_id(int id)
        {
            Tbldonvi dv = new Tbldonvi();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbldonvis.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_donvi")]
        [HttpPost]
        public bool create_donvi([FromBody] Tbldonvi dv)
        {
            try {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tbldonvis.Add(dv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_donvi/{id}")]
        [HttpPut]
        public bool edit_donvi(int id,[FromBody] Tbldonvi dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbldonvi d = db.Tbldonvis.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tendv = dv.Tendv;
                    d.Tyle = dv.Tyle;
                    d.Ghichu = dv.Ghichu;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_donvi/{id}")]
        [HttpDelete]
        public bool delete_donvi(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbldonvi d = db.Tbldonvis.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tbldonvis.Remove(d);
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
