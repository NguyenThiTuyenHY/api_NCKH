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
    public class lylichController : ControllerBase
    {
        [Route("get_lylich_id/{id}")]
        [HttpGet]
        public Tbllylich get_lylich_id(int id)
        {
            Tbllylich ll = new Tbllylich();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ll = db.Tbllyliches.SingleOrDefault(x => x.Id == id);
            }
            return ll;
        }
        [Route("edit_lylich/{id}")]
        [HttpPut]
        public bool edit_lylich(int id, [FromBody] lylich ll)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbllylich d = db.Tbllyliches.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Hocham = ll.Hocham;
                    d.Namphong = ll.Namphong;
                    d.Hocvi = ll.Hocvi;
                    d.Namcap = ll.Namcap;
                    // Dai hoc
                    d.Hedaotao = ll.Hedaotao;
                    d.Noidaotao = ll.Noidaotao;
                    d.Nganhhoc = ll.Nganhhoc;
                    d.Nuocdaotao = ll.Nuocdaotao;
                    d.Namtotnghiep = ll.Namtotnghiep;
                    d.Bangdaihoc = ll.Bangdaihoc;
                    d.Namtotnghiep2 = ll.Namtotnghiep2;
                    // sau dai hoc
                    d.Bangthacsi = ll.Bangthacsi;
                    d.Namcapbang = ll.Namcapbang;
                    d.Noidaotaoa2 = ll.Noidaotao2;
                    d.Bangtiensi = ll.Bangtiensi;
                    d.Namcapbang2 = ll.Namcapbang2;
                    d.Noidaotao2 = ll.Noidaotao2;
                    d.Tenchuyende = ll.Tenchuyende;
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
