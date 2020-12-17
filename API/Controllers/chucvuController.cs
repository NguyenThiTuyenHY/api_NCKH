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
    public class chucvuController : ControllerBase
    {
        [Route("get_chucvu_all")]
        [HttpGet]
        public List<Tblchucvu> get_chucvu_all()
        {
            List<Tblchucvu> ds = new List<Tblchucvu>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = db.Tblchucvus.ToList();
                if (ds == null)
                    return null;
            }
            return ds;
        }
        [Route("get_chucvu_pagesize")]
        [HttpGet]
        public datatable<Tblchucvu> get_chucvu_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblchucvu> dv = new datatable<Tblchucvu>();
            List<Tblchucvu> ds = new List<Tblchucvu>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                int index = (pageindex - 1) * pagesize;
                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tblchucvus.Where(x => x.Tenchucvu.IndexOf(search) >= 0).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblchucvus.Where(x => x.Tenchucvu.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tblchucvus.Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblchucvus.Count();
                }
                dv.result = ds;
               
            }
            return dv;
        }
        [Route("get_chucvu_id/{id}")]
        [HttpGet]
        public Tblchucvu get_chucvu_id(int id)
        {
            Tblchucvu dv = new Tblchucvu();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblchucvus.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_chucvu")]
        [HttpPost]
        public bool create_chucvu([FromBody] Tblchucvu cv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblchucvus.Add(cv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_chucvu/{id}")]
        [HttpPut]
        public bool edit_chucvu(int id, [FromBody] Tblchucvu cv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblchucvu d = db.Tblchucvus.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tenchucvu = cv.Tenchucvu;
                    d.Dinhmuc = cv.Dinhmuc;
                    d.Dieukien = cv.Dieukien;
                    d.Ghichu = cv.Ghichu;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_chucvu/{id}")]
        [HttpDelete]
        public bool delete_chucvu(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblchucvu d = db.Tblchucvus.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblchucvus.Remove(d);
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
