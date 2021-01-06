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
        public datatable<detai> get_detai_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<detai> dv = new datatable<detai>();
            List<detai> ds = new List<detai>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                //int index = (pageindex - 1) * pagesize;
                if (!string.IsNullOrEmpty(search))
                {
                    ds = (from dt in db.Tbldetais
                          join hd in db.Tblhoatdongnckhs on dt.Idhdnckh equals hd.Id
                          join lv in db.Tbllinhvucs on dt.Idlinhvuc equals lv.Id
                          join lnv in db.Tblloainhiemvus on dt.Idloainv equals lnv.Id
                          join nv in db.Tblloaihoatdongs on hd.Idloaihd equals nv.Id
                          select new detai
                          {
                              Id = dt.Id,
                              Tendetai = dt.Tendetai,
                              Idnv = dt.Idnv,
                              Idlsp = dt.Idlsp,
                              Tentc = dt.Tentc,
                              Sohieu = dt.Sohieu,
                              Namsx = dt.Namsx,
                              Tap = dt.Tap,
                              So = dt.So,
                              Trang = dt.Trang,
                              Soif = dt.Soif,
                              Minhchung = dt.Minhchung,
                              Tinhtrang = dt.Tinhtrang,
                              Ghichu = dt.Ghichu,
                              Idlinhvuc = dt.Idlinhvuc,
                              Idloainv = dt.Idloainv,
                              Idhdnckh = dt.Idhdnckh,
                              Uytin = dt.Uytin,
                              Thoigianbd = dt.Thoigianbd,
                              Thoigiankt = dt.Thoigiankt,
                              Thoigiannt = dt.Thoigiannt,
                              Thoigiangiahan = dt.Thoigiangiahan,
                              Kqbv = dt.Kqbv,
                              Capbv = dt.Capbv,
                              Noidang = dt.Noidang,
                              Namdang = dt.Namdang,
                              Tenhdnckh = nv.Tenloaihd,
                              Tenlinhvuc = lv.Tenlinhvuc,
                              Tenloainv = lnv.Tenloainv
                          }).Where(x => x.Tendetai.IndexOf(search) >= 0 &&( x.Tinhtrang != -1 && x.Tinhtrang != 6)).Skip(pageindex).Take(pagesize).ToList();
                    //ds = db.Tbldetais.Where(x => x.Tendetai.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tbldetais.Where(x => x.Tendetai.IndexOf(search) >= 0 &&( x.Tinhtrang != -1 && x.Tinhtrang != 6)).Count();
                }
                else
                {
                    ds = (from dt in db.Tbldetais
                          join hd in db.Tblhoatdongnckhs on dt.Idhdnckh equals hd.Id
                          join lv in db.Tbllinhvucs on dt.Idlinhvuc equals lv.Id
                          join lnv in db.Tblloainhiemvus on dt.Idloainv equals lnv.Id
                          join nv in db.Tblloaihoatdongs on hd.Idloaihd equals nv.Id
                          select new detai
                          {
                              Id = dt.Id,
                              Tendetai = dt.Tendetai,
                              Idnv = dt.Idnv,
                              Idlsp = dt.Idlsp,
                              Tentc = dt.Tentc,
                              Sohieu = dt.Sohieu,
                              Namsx = dt.Namsx,
                              Tap = dt.Tap,
                              So = dt.So,
                              Trang = dt.Trang,
                              Soif = dt.Soif,
                              Minhchung = dt.Minhchung,
                              Tinhtrang = dt.Tinhtrang,
                              Ghichu = dt.Ghichu,
                              Idlinhvuc = dt.Idlinhvuc,
                              Idloainv = dt.Idloainv,
                              Idhdnckh = dt.Idhdnckh,
                              Uytin = dt.Uytin,
                              Thoigianbd = dt.Thoigianbd,
                              Thoigiankt = dt.Thoigiankt,
                              Thoigiannt = dt.Thoigiannt,
                              Thoigiangiahan = dt.Thoigiangiahan,
                              Kqbv = dt.Kqbv,
                              Capbv = dt.Capbv,
                              Noidang = dt.Noidang,
                              Namdang = dt.Namdang,
                              Tenhdnckh = nv.Tenloaihd,
                              Tenlinhvuc = lv.Tenlinhvuc,
                              Tenloainv = lnv.Tenloainv
                          }).Where(x => x.Tinhtrang != -1 && x.Tinhtrang != 6).Skip(pageindex).Take(pagesize).ToList();
                    dv.total = db.Tbldetais.Where(x => x.Tinhtrang != -1 && x.Tinhtrang != 6).Count();

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
            using (sql_NCKHContext db = new sql_NCKHContext())
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
        public bool chuyen_trang_thai(int id, int trangthai, string time)
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
                        case 2:
                            dt.Tinhtrang = 2; //duyệt
                            dt.Thoigianbd = DateTime.Now;
                            dt.Thoigiannt = tinhthoigiannt(dt, Convert.ToDateTime(dt.Thoigianbd));
                            break;
                        case 3:
                            dt.Tinhtrang = 3; //Hoan thanh
                            break;
                        case 4:
                            dt.Thoigiangiahan = giahan(time);
                            dt.Tinhtrang = 4; //Xin them thoi gian
                            break;
                        case 5:
                            dt.Tinhtrang = 5; //áp dụng thực tế
                            break;
                        case -1:
                            dt.Tinhtrang = -1; //huỷ
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
        [Route("get_detai_id/{id}")]
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
        [Route("get_detai_idnv/{id}")]
        [HttpGet]
        public List<detai> get_detai_idnv(int id)
        {
            List<detai> ds = new List<detai>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = (from dt in db.Tbldetais
                      join hd in db.Tblhoatdongnckhs on dt.Idhdnckh equals hd.Id
                      join lv in db.Tbllinhvucs on dt.Idlinhvuc equals lv.Id
                      join lnv in db.Tblloainhiemvus on dt.Idloainv equals lnv.Id
                      join nv in db.Tblloaihoatdongs on hd.Idloaihd equals nv.Id
                      select new detai
                      {
                          Id = dt.Id,
                          Tendetai = dt.Tendetai,
                          Idnv = dt.Idnv,
                          Idlsp = dt.Idlsp,
                          Tentc = dt.Tentc,
                          Sohieu = dt.Sohieu,
                          Namsx = dt.Namsx,
                          Tap = dt.Tap,
                          So = dt.So,
                          Trang = dt.Trang,
                          Soif = dt.Soif,
                          Minhchung = dt.Minhchung,
                          Tinhtrang = dt.Tinhtrang,
                          Ghichu = dt.Ghichu,
                          Idlinhvuc = dt.Idlinhvuc,
                          Idloainv = dt.Idloainv,
                          Idhdnckh = dt.Idhdnckh,
                          Uytin = dt.Uytin,
                          Thoigianbd = dt.Thoigianbd,
                          Thoigiankt = dt.Thoigiankt,
                          Thoigiannt = dt.Thoigiannt,
                          Thoigiangiahan = dt.Thoigiangiahan,
                          Kqbv = dt.Kqbv,
                          Capbv = dt.Capbv,
                          Noidang = dt.Noidang,
                          Namdang = dt.Namdang,
                          Tenhdnckh = nv.Tenloaihd,
                          Tenlinhvuc = lv.Tenlinhvuc,
                          Tenloainv = lnv.Tenloainv
                      }).Where(x => x.Tinhtrang != -1 && x.Tinhtrang != 6 && x.Idnv == id).ToList();
            }
            return ds;
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
        [Route("edit_detai/{id}")]
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
        [Route("delete_detai/{id}")]
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
        [Route("get_detai_nckh/{id}")]
        [HttpGet]
        public List<detainhom> get_detai_nckh(int id)
        {
            List<detainhom> dv = new List<detainhom>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbldetais.Select(dt => new detainhom
                {
                    Id = dt.Id,
                    Tendetai = dt.Tendetai,
                    Idnv = dt.Idnv,
                    Sohieu = dt.Sohieu,
                    Kqbv = dt.Kqbv, // 1 - Xuất sắc, 2 - Giỏi, 3 - Khá, 4 - Trung bình, 5 - Kém
                    Capbv = dt.Capbv, // 1 - Đề tài cấp bộ, 2 - Đề tài KHCNcấp trường, 3 - Đề tài cấp nhà nước
                    Thoigianbd = dt.Thoigianbd,
                    Thoigiankt = dt.Thoigiankt,
                    Thoigiannt = dt.Thoigiannt,
                    Thoigiangiahan = dt.Thoigiangiahan, // Thời gian gia hạn
                    Tinhtrang = dt.Tinhtrang // 1 - chua duyet, 2 - duyet, 3 - hoan thanh, 4 - xin thoi gian, 5 - ap dung thuc tien, 6 - báo cáo
                }).Where(x => x.Idnv == id && x.Tinhtrang == 3).ToList();
            }
            return dv;
        }
        [Route("get_detai_nckh2/{id}")]
        [HttpGet]
        public List<detainoidang> get_detai_nckh2(int id)
        {
            List<detainoidang> dv = new List<detainoidang>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbldetais.Where(x => x.Idnv == id && x.Noidang != null).Select(dt => new detainoidang
                {
                    Id = dt.Id,
                    Tendetai = dt.Tendetai,
                    Idnv = dt.Idnv,
                    Noidang = dt.Noidang,
                    Namdang = dt.Namdang
                }).ToList();
            }
            return dv;
        }
        [Route("get_detai_nckh3/{id}")]
        [HttpGet]
        public List<detainoidang> get_detai_nckh3(int id)
        {
            List<detainoidang> dv = new List<detainoidang>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbldetais.Where(x => x.Idnv == id && x.Noidang != null && x.Tinhtrang != 6).Select(dt => new detainoidang
                {
                    Id = dt.Id,
                    Tendetai = dt.Tendetai,
                    Idnv = dt.Idnv,
                    Noidang = dt.Noidang,
                    Namdang = dt.Namdang
                }).ToList();
            }
            return dv;
        }
        [Route("get_detai_nckh6/{id}")]
        [HttpGet]
        public List<detainoidang> get_detai_nckh6(int id)
        {
            List<detainoidang> dv = new List<detainoidang>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbldetais.Where(x => x.Idnv == id && x.Tinhtrang == 6).Select(dt => new detainoidang
                {
                    Id = dt.Id,
                    Tendetai = dt.Tendetai,
                    Idnv = dt.Idnv,
                    Noidang = dt.Noidang,
                    Namdang = dt.Namdang
                }).ToList();
            }
            return dv;
        }
        [Route("get_detai_nckh5/{id}")]
        [HttpGet]
        public List<detaithucte> get_detai_nckh5(int id)
        {
            List<detaithucte> dv = new List<detaithucte>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tbldetais.Where(x => x.Idnv == id && x.Tinhtrang == 5).Select(dt => new detaithucte
                {
                    Id = dt.Id,
                    Tendetai = dt.Tendetai,
                    Idnv = dt.Idnv,
                    Ghichu = dt.Ghichu
                }).ToList();
            }
            return dv;
        }
    }
}
