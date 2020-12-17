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
    public class loaitintucController : ControllerBase
    {
        [Route("get_loaitintuc_all")]
        [HttpGet]
        public List<Tblloaitt> get_loaitintuc_all()
        {
            List<Tblloaitt> ds = new List<Tblloaitt>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = db.Tblloaitts.ToList();               
            }
            return ds;
        }
        [Route("get_loaitintuc_pagesize")]
        [HttpGet]
        public datatable<Tblloaitt> get_loaitintuc_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblloaitt> dv = new datatable<Tblloaitt>();
            List<Tblloaitt> ds = new List<Tblloaitt>();
            int index = (pageindex - 1) * pagesize;
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tblloaitts.OrderByDescending(x=>x.Id).Where(x => x.Tenloaitt.IndexOf(search) >= 0).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblloaitts.Where(x => x.Tenloaitt.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tblloaitts.OrderByDescending(x => x.Id).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblloaitts.Count();
                }
                dv.result = ds;
            }
            return dv;
        }
        [Route("get_loaitintuc_id/{id}")]
        [HttpGet]
        public Tblloaitt get_loaitintuc_id(int id)
        {
            Tblloaitt ltt = new Tblloaitt();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ltt = db.Tblloaitts.SingleOrDefault(x => x.Id == id);
            }
            return ltt;
        }
        [Route("create_loaitintuc")]
        [HttpPost]
        public bool create_loaitintuc([FromBody] Tblloaitt ltt)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblloaitts.Add(ltt);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_loaitintuc/{id}")]
        [HttpPut]
        public bool edit_loaitintuc(int id, [FromBody] Tblloaitt ltt)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblloaitt d = db.Tblloaitts.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tenloaitt = ltt.Tenloaitt;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_loaitintuc/{id}")]
        [HttpDelete]
        public bool delete_loaitintuc(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblloaitt d = db.Tblloaitts.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblloaitts.Remove(d);
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
