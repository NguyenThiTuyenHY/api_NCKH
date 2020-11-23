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
    public class phongbanController : ControllerBase
    {
        [Route("get_phongban_pagesize")]
        [HttpGet]
        public datatable<Tblphongban> get_phongban_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblphongban> dv = new datatable<Tblphongban>();
            List<Tblphongban> ds = new List<Tblphongban>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tblphongbans.Where(x => x.Tenphongban.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tblphongbans.Where(x => x.Tenphongban.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tblphongbans.Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tblphongbans.Count();
                }
                dv.result = ds;
                
            }
            return dv;
        }
        [Route("get_phongban_id")]
        [HttpGet]
        public Tblphongban get_phongban_id(int id)
        {
            Tblphongban dv = new Tblphongban();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblphongbans.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_phongban")]
        [HttpPost]
        public bool create_phongban([FromBody] Tblphongban dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tblphongbans.Add(dv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_phongban")]
        [HttpPut]
        public bool edit_phongban(int id, [FromBody] Tblphongban pb)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblphongban d = db.Tblphongbans.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tenphongban = pb.Tenphongban;
                    d.Iddv = pb.Iddv;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_phongban")]
        [HttpDelete]
        public bool delete_phongban(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblphongban d = db.Tblphongbans.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblphongbans.Remove(d);
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
