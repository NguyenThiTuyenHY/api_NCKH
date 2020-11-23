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
    public class boiduongController : ControllerBase
    {
        [Route("get_boiduong_idnv/{idnv}")]
        [HttpGet]
        public List<Tblboiduong> get_boiduongc_idnv(int idnv)
        {
            List<Tblboiduong> ds = new List<Tblboiduong>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                ds = db.Tblboiduongs.Where(x => x.Idnv == idnv).ToList();

            }
            return ds;
        }
        [Route("create_boiduong")]
        [HttpPost]
        public bool create_boiduong([FromBody] Tblboiduong bd)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblboiduongs.Add(bd);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_boiduong/{id}")]
        [HttpPut]
        public bool edit_boiduong(int id, [FromBody] Tblboiduong bd)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblboiduong d = db.Tblboiduongs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Noibd = bd.Noibd;
                    d.Ngaybd = bd.Ngaybd;
                    d.Ngaykt = bd.Ngaykt;
                    d.Noidung = bd.Noidung;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_boiduong/{id}")]
        [HttpDelete]
        public bool delete_boiduong(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblboiduong d = db.Tblboiduongs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblboiduongs.Remove(d);
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
