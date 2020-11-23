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
    public class hosoController : ControllerBase
    {
        [Route("get_hoso_pagesize")]
        [HttpGet]
        public List<Tblhoso> get_hoso_pagesize(int iddetai)
        {
            datatable<Tblhoso> dv = new datatable<Tblhoso>();
            List<Tblhoso> ds = new List<Tblhoso>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                return db.Tblhosos.Where(x => x.Iddetai == iddetai).ToList();
            }
        }
        [Route("get_hoso_id")]
        [HttpGet]
        public Tblhoso get_hoso_id(int id)
        {
            Tblhoso dv = new Tblhoso();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblhosos.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_hoso")]
        [HttpPost]
        public bool create_hoso([FromBody] Tblhoso dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblhosos.Add(dv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_hoso")]
        [HttpPut]
        public bool edit_hoso(int id, [FromBody] Tblhoso hs)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblhoso d = db.Tblhosos.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Ten = hs.Ten;
                    d.Ngay = hs.Ngay;
                    d.Minhchung = hs.Minhchung;
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
