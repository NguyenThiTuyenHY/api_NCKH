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
    public class roleController : ControllerBase
    {
        [Route("get_role_pagesize")]
        [HttpGet]
        public datatable<Role> get_role_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Role> dv = new datatable<Role>();
            List<Role> ds = new List<Role>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Roles.Where(x => x.Ten.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Roles.Where(x => x.Ten.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Roles.Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Roles.Count();
                }
                dv.result = ds;

            }
            return dv;
        }
        [Route("get_role_id/{id}")]
        [HttpGet]
        public Role get_role_id(int id)
        {
            Role dv = new Role();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Roles.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_role")]
        [HttpPost]
        public bool create_donvi([FromBody] Tbldonvi dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tbldonvis.Add(dv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_role/{id}")]
        [HttpPut]
        public bool edit_role(int id, [FromBody] Role rl)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Role d = db.Roles.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Ten = rl.Ten;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_role/{id}")]
        [HttpDelete]
        public bool delete_role(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Role d = db.Roles.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Roles.Remove(d);
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
