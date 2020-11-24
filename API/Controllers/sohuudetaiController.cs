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
    public class sohuudetaiController : ControllerBase
    {
        [Route("get_sohuudetai_pagesize")]
        [HttpGet]
        public datatable<Tblsohuudetai> get_donvi_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblsohuudetai> dv = new datatable<Tblsohuudetai>();
            List<Tblsohuudetai> ds = new List<Tblsohuudetai>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = db.Tblsohuudetais.Skip(pageindex).Take(pagesize).ToList();
                dv.total = db.Tbldonvis.Count();
                dv.result = ds;
            }
            return dv;
        }
        [Route("get_sohuudetai_id")]
        [HttpGet]
        public Tblsohuudetai get_sohuudetai_id(int id)
        {
            Tblsohuudetai dv = new Tblsohuudetai();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblsohuudetais.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_sohuudetai")]
        [HttpPost]
        public bool create_sohuudetai([FromBody] Tblsohuudetai sh)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblsohuudetais.Add(sh);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_sohuudetai")]
        [HttpPut]
        public bool edit_sohuudetai(int id, [FromBody] Tblsohuudetai sh)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblsohuudetai d = db.Tblsohuudetais.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Idsohuu = sh.Idsohuu;
                    d.Ghichu = sh.Ghichu;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_sohuudetai")]
        [HttpDelete]
        public bool delete_sohuudetai(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblsohuudetai d = db.Tblsohuudetais.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblsohuudetais.Remove(d);
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
