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
    public class detaiController : ControllerBase
    {
        [Route("get_detai_pagesize")]
        [HttpGet]
        public datatable<Tbldetai> get_detai_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<Tbldetai> dv = new datatable<Tbldetai>();
            List<Tbldetai> ds = new List<Tbldetai>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tbldetais.Where(x => x.Tendetai.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tbldetais.Where(x => x.Tendetai.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tbldetais.Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tbldetais.Count();
                }
                dv.result = ds;

            }
            return dv;
        }
        public DateTime tinhthoigiannt(Tbldetai dt, DateTime tg)
        {
            double? c = 0;
            double? n = 0;
            double? s = 1;
            double? d = 0;
            DateTime a = new DateTime();
            using(sql_NCKHContext db = new sql_NCKHContext())
            {
                if (!string.IsNullOrEmpty(dt.Thoigiankt.ToString()))
                {
                    c = db.Tblloainhiemvus.SingleOrDefault(x => x.Id == dt.Idloainv).C;
                    n = Convert.ToDateTime(dt.Thoigiankt).Year - tg.Year;
                    s = 900 * c * n;
                    s = s + db.Tblhoatdongnckhs.SingleOrDefault(x => x.Id == dt.Idhdnckh).Dinhmuc;
                    List<Tblsohuudetai> ds = db.Tblsohuudetais.Where(x => x.Iddetai == dt.Id).ToList();
                    foreach (Tblsohuudetai ss in ds)
                    {
                        s = s + db.Tblsohuus.SingleOrDefault(x => x.Id == ss.Idsohuu).Dm;
                    }
                    if (float.TryParse(s.ToString(), out float x))
                    {
                        string[] b = s.ToString().Split('.');
                        int m = int.Parse(b[0]);
                        d = (s - m) * 60;
                        s = m;
                    }
                    TimeSpan span = new TimeSpan(Convert.ToInt32(s), Convert.ToInt32(d), 0);
                }
            }
            return a;
        }
        public DateTime giahan(string time)
        {
            string[] a = time.Split('/');
            return new DateTime(Convert.ToInt32(a[2]), Convert.ToInt32(a[1]), Convert.ToInt32(a[0]));
        }
        [Route("chuyen_trang_thai")]
        [HttpGet]
        public bool chuyen_trang_thai(int id, int trangthai,string time)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbldetai dt = db.Tbldetais.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(dt.ToString()))
                        return false;
                    switch (trangthai)
                    {
                        case 1:
                            dt.Tinhtrang = 1; //Xac nhan
                            dt.Thoigianbd = DateTime.Now;
                            dt.Thoigiannt = tinhthoigiannt(dt, Convert.ToDateTime(dt.Thoigianbd));
                            break;
                        case 2:
                            dt.Tinhtrang = 2; //Dang hoan thanh
                            break;
                        case 3:
                            dt.Tinhtrang = 3; //Hoan thanh
                            break;
                        case 4:
                            dt.Thoigiangiahan = giahan(time);
                            dt.Tinhtrang = 4; //Xin them thoi gian
                            break;
                        case 5:
                            dt.Tinhtrang = 5;
                            break;
                    }
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("get_detai_id")]
        [HttpGet]
        public Tbldetai get_detai_id(int id)
        {
            Tbldetai dv = new Tbldetai();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbldetais.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_detai")]
        [HttpPost]
        public bool create_detai([FromBody] Tbldetai dt)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    db.Tbldetais.Add(dt);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_detai")]
        [HttpPut]
        public bool edit_detai(int id, [FromBody] Tbldetai dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbldetai d = db.Tbldetais.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_detai")]
        [HttpDelete]
        public bool delete_detai(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbldetai d = db.Tbldetais.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tbldetais.Remove(d);
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
