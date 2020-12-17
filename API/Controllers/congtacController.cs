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
    public class congtacController : ControllerBase
    {
        [Route("get_congtac_idnv/{idnv}")]
        [HttpGet]
        public List<Tblcongtac> get_congtac_idnv(int idnv)
        {
            try
            {
                List<Tblcongtac> ds = new List<Tblcongtac>();
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    ds = db.Tblcongtacs.Where(x => x.Idnv == idnv).ToList();
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        [Route("create_congtac")]
        [HttpPost]
        public bool create_congtac([FromBody] Tblcongtac ct)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblcongtacs.Add(ct);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_congtac")]
        [HttpPut]
        public bool edit_congtac(int id, [FromBody] Tblcongtac dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblcongtac d = db.Tblcongtacs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Noict = dv.Noict;
                    d.Ngaybad = dv.Ngaybad;
                    d.Ngaykt = dv.Ngaykt;
                    d.Chucvu = dv.Chucvu;
                    d.Chucdanh = dv.Chucdanh;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_congtac")]
        [HttpDelete]
        public bool delete_congtac(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblcongtac d = db.Tblcongtacs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblcongtacs.Remove(d);
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
