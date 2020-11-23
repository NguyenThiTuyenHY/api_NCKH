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
    public class nhomtgController : ControllerBase
    {
        [Route("get_nhomtg_pagesize")]
        [HttpGet]
        public List<nhomtg> get_nhomtg_all(int id)
        {
            List<nhomtg> ds = new List<nhomtg>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = db.Tblnhomtgs.Join(db.Tblnhanviens, ntg => ntg.Idnv, nv => nv.Id, (ntg, nv) => new nhomtg
                {
                    Id = ntg.Id,
                    Iddetai = ntg.Iddetai,
                    Idnv = ntg.Idnv,
                    Chucvu = ntg.Chucvu,
                    Hoten = nv.Hoten
                }).Where(x=>x.Iddetai==id).ToList();
            }
            return ds;
        }
        [Route("get_nhomtg_id")]
        [HttpGet]
        public Tblnhomtg get_nhomtg_id(int id)
        {
            Tblnhomtg dv = new Tblnhomtg();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblnhomtgs.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_nhomtg")]
        [HttpPost]
        public bool create_nhomtg([FromBody] Tblnhomtg ntg)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblnhomtgs.Add(ntg);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_nhomtg")]
        [HttpPut]
        public bool edit_loainhiemvu(int id, [FromBody] Tblnhomtg ntg)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblnhomtg d = db.Tblnhomtgs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Chucvu = ntg.Chucvu;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_nhomtg")]
        [HttpDelete]
        public bool delete_nhomtg(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblnhomtg d = db.Tblnhomtgs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblnhomtgs.Remove(d);
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
