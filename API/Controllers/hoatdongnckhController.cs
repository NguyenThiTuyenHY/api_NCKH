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
    public class hoatdongnckhController : ControllerBase
    {
        [Route("get_hoatdongnckh_pagesize")]
        [HttpGet]
        public datatable<Tblhoatdongnckh> get_hoatdongnckh_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblhoatdongnckh> dv = new datatable<Tblhoatdongnckh>();
            List<Tblhoatdongnckh> ds = new List<Tblhoatdongnckh>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tblhoatdongnckhs.Where(x => x.Tenhdnckh.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tblhoatdongnckhs.Where(x => x.Tenhdnckh.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tblhoatdongnckhs.Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tblhoatdongnckhs.Count();
                }
                dv.result = ds;

            }
            return dv;
        }
    }
}
