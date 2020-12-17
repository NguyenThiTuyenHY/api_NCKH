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
    public class loainghiencuuController : ControllerBase
    {
        [Route("get_loainghiencuu_pagesize")]
        [HttpGet]
        public datatable<Tblloainghiencuu> get_loainghiencuu_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tblloainghiencuu> dv = new datatable<Tblloainghiencuu>();
            int index = (pageindex - 1) * pagesize;
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    dv.result = db.Tblloainghiencuus.Where(x => x.Tenloai.IndexOf(search) >= 0).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblloainghiencuus.Where(x => x.Tenloai.IndexOf(search) >= 0).Count();
                }
                else
                {
                    dv.result = db.Tblloainghiencuus.Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblloainghiencuus.Count();
                }
            }
            return dv;
        }
    }
}
