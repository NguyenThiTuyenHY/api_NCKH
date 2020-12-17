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
    public class sohuuController : ControllerBase
    {
        [Route("get_sohuu_pagesize")]
        [HttpGet]
        public datatable<Tblsohuu> get_sohuu_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblsohuu> dv = new datatable<Tblsohuu>();
            List<Tblsohuu> ds = new List<Tblsohuu>();
            int index = (pageindex - 1) * pagesize;
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = db.Tblsohuus.Skip(pageindex).Take(pagesize).ToList();
                dv.total = db.Tbldonvis.Count();
                dv.result = ds;
            }
            return dv;
        }
        [Route("get_sohuu_id/{id}")]
        [HttpGet]
        public Tblsohuu get_sohuu_id(int id)
        {
            Tblsohuu sh = new Tblsohuu();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                sh = db.Tblsohuus.SingleOrDefault(x => x.Id == id);
            }
            return sh;
        }
        [Route("create_sohuu")]
        [HttpPost]
        public bool create_sohuu([FromBody] Tblsohuu sh)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblsohuus.Add(sh);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_sohuu/{id}")]
        [HttpPut]
        public bool edit_sohuu(int id, [FromBody] Tblsohuu hs)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblsohuu d = db.Tblsohuus.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tensohuu = hs.Tensohuu;
                    d.Dmhtkinhphi = hs.Dmhtkinhphi;
                    d.Dm = hs.Dm;
                    d.Ghichu = hs.Ghichu;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_sohuu/{id}")]
        [HttpDelete]
        public bool delete_sohuu(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblsohuu d = db.Tblsohuus.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblsohuus.Remove(d);
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
