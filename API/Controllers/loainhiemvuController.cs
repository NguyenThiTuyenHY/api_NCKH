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
    public class loainhiemvuController : ControllerBase
    {
        [Route("get_loainhiemvu_pagesize")]
        [HttpGet]
        public datatable<Tblloainhiemvu> get_loainhiemvu_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblloainhiemvu> dv = new datatable<Tblloainhiemvu>();
            List<Tblloainhiemvu> ds = new List<Tblloainhiemvu>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tblloainhiemvus.Where(x => x.Tenloainv.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tblloainhiemvus.Where(x => x.Tenloainv.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tblloainhiemvus.Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tblloainhiemvus.Count();
                }
                dv.result = ds;

            }
            return dv;
        }
        [Route("get_loainhiemvu_id/{id}")]
        [HttpGet]
        public Tblloainhiemvu get_loainhiemvu_id(int id)
        {
            Tblloainhiemvu dv = new Tblloainhiemvu();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblloainhiemvus.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_loainhiemvu")]
        [HttpPost]
        public bool create_loainhiemvu([FromBody] Tblloainhiemvu lnv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblloainhiemvus.Add(lnv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_loainhiemvu/{id}")]
        [HttpPut]
        public bool edit_loainhiemvu(int id, [FromBody] Tblloainhiemvu lnv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblloainhiemvu d = db.Tblloainhiemvus.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tenloainv = lnv.Tenloainv;
                    d.Ghichu = lnv.Ghichu;
                    d.C = lnv.C;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_loainhiemvu/{id}")]
        [HttpDelete]
        public bool delete_loainhiemvu(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblloainhiemvu d = db.Tblloainhiemvus.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblloainhiemvus.Remove(d);
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
