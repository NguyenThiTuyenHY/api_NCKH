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
    public class linhvucController : ControllerBase
    {
        [Route("get_linhvuc_pagesize")]
        [HttpGet]
        public datatable<Tbllinhvuc> get_linhvuc_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tbllinhvuc> dv = new datatable<Tbllinhvuc>();
            List<Tbllinhvuc> ds = new List<Tbllinhvuc>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tbllinhvucs.Where(x => x.Tenlinhvuc.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tbllinhvucs.Where(x => x.Tenlinhvuc.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tbllinhvucs.Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tbllinhvucs.Count();
                }
                dv.result = ds;

            }
            return dv;
        }
        [Route("get_linhvuc_id/{id}")]
        [HttpGet]
        public Tbllinhvuc get_linhvuc_id(int id)
        {
            Tbllinhvuc dv = new Tbllinhvuc();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbllinhvucs.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_linhvuc")]
        [HttpPost]
        public bool create_linhvuc([FromBody] Tbllinhvuc lv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tbllinhvucs.Add(lv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_linhvuc/{id}")]
        [HttpPut]
        public bool edit_linhvuc(int id, [FromBody] Tbllinhvuc lv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbllinhvuc d = db.Tbllinhvucs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tenlinhvuc = lv.Tenlinhvuc;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_linhvuc/{id}")]
        [HttpDelete]
        public bool delete_linhvuc(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbllinhvuc d = db.Tbllinhvucs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tbllinhvucs.Remove(d);
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
