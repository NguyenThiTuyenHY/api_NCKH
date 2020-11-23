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
    public class userController : ControllerBase
    {
        [Route("get_user_pagesize")]
        [HttpGet]
        public datatable<User> get_user_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<User> dv = new datatable<User>();
            List<User> ds = new List<User>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Users.Where(x => x.Taikhoan.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Users.Where(x => x.Taikhoan.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Users.Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Users.Count();
                }
                dv.result = ds;

            }
            return dv;
        }
        [Route("get_user_id/{id}")]
        [HttpGet]
        public User get_user_id(int id)
        {
            User dv = new User();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Users.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_user")]
        [HttpPost]
        public bool create_donvi([FromBody] User us)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Users.Add(us);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_user_matkhau")]
        [HttpPut]
        public bool edit_user_matkhau(int id, [FromBody] User us)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User d = db.Users.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Matkhau = us.Matkhau;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_user")]
        [HttpDelete]
        public bool delete_user(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User d = db.Users.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Users.Remove(d);
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
