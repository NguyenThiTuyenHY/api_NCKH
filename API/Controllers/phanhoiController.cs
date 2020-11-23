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
    public class phanhoiController : ControllerBase
    {
        [Route("get_phanhoi_pagesize")]
        [HttpGet]
        public List<Tblphanhoi> get_phanhoi_pagesize(int iddetai)
        {
            datatable<Tblphanhoi> dv = new datatable<Tblphanhoi>();
            List<Tblphanhoi> ds = new List<Tblphanhoi>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                return db.Tblphanhois.Where(x => x.Iddetai == iddetai).ToList();
            }
        }
        [Route("get_phanhoi_id")]
        [HttpGet]
        public Tblphanhoi get_phanhoi_id(int id)
        {
            Tblphanhoi dv = new Tblphanhoi();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblphanhois.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_phanhoi")]
        [HttpPost]
        public bool create_hoso([FromBody] Tblphanhoi dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblphanhois.Add(dv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_phanhoi")]
        [HttpPut]
        public bool edit_phanhoi(int id, [FromBody] Tblphanhoi ph)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblphanhoi d = db.Tblphanhois.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_hoso/{id}")]
        [HttpDelete]
        public bool delete_hoso(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblhoso d = db.Tblhosos.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblhosos.Remove(d);
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
