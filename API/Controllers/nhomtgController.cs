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
    public class nhomtgController : ControllerBase
    {
        [Route("get_nhomtg_all/{id}")]
        [HttpGet]
        public List<nhomtg> get_nhomtg_all(int id)
        {
            List<nhomtg> ds = new List<nhomtg>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = db.Tblnhomtgs.Join(db.Tblnhanviens, ntg => ntg.Idnv, nv => nv.Id, (ntg, nv) => new nhomtg
                {
                    Id = ntg.Id,
                    Iddetai = ntg.Iddetai,
                    Idnv = ntg.Idnv,
                    Chucvu = ntg.Chucvu,
                    Hoten = nv.Hoten
                }).Where(x=>x.Iddetai==id).ToList();
                foreach(Tblnhomtg tg in db.Tblnhomtgs)
                {
                    if(tg.Iddetai == id && tg.Idnv == null)
                    {
                        nhomtg a = new nhomtg();
                        a.Id = tg.Id;
                        a.Idnv = -1;
                        a.Iddetai = tg.Iddetai;
                        a.Chucvu = tg.Chucvu;
                        a.Hoten = tg.Hoten;
                        ds.Add(a);
                    }
                }
                Tbldetai dt = db.Tbldetais.SingleOrDefault(x => x.Id == id);
                Tblnhanvien dsnv = db.Tblnhanviens.SingleOrDefault(x => x.Id == dt.Idnv);
                nhomtg b = new nhomtg();
                b.Id = 0;
                b.Iddetai = id;
                b.Idnv = dsnv.Id;
                b.Chucvu = "Chủ đề tài";
                b.Hoten = dsnv.Hoten;
                ds.Add(b);
            }
            return ds;
        }
        [Route("get_nhomtg_id/{id}")]
        [HttpGet]
        public Tblnhomtg get_nhomtg_id(int id)
        {
            Tblnhomtg dv = new Tblnhomtg();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblnhomtgs.SingleOrDefault(x => x.Id == id);
                if(dv.Idnv == null)
                {
                    dv.Idnv = -1;
                }
            }
            return dv;
        }
        [Route("create_nhomtg")]
        [HttpPost]
        public alter create_nhomtg([FromBody] List<Tblnhomtg> ntg)
        {
            alter result = new alter();
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    int dem = 0;
                    foreach(var item in ntg)
                    {
                        Tblnhomtg tg = db.Tblnhomtgs.Where(x=>(x.Chucvu == item.Chucvu&&x.Iddetai == item.Iddetai&& x.Idnv == item.Idnv) || (x.Chucvu == item.Chucvu && x.Iddetai == item.Iddetai && x.Hoten == item.Hoten)).SingleOrDefault();
                        if (tg == null)
                        {
                            if(item.Idnv != null)
                            {
                                Tblnhanvien nv = db.Tblnhanviens.SingleOrDefault(x => x.Id == item.Idnv);
                                item.Hoten = nv.Hoten;
                            }
                            db.Tblnhomtgs.Add(item);
                            db.SaveChanges();
                        }
                        else
                        {
                            dem++;
                        }
                        result.ketqua = true;
                        result.thongbao = string.Format("Thêm thành công {0}, lỗi {1}", ntg.Count() - dem, dem);
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ketqua = false;
                result.thongbao = ex.Message;
                return result;
            }
        }
        [Route("edit_nhomtg/{id}")]
        [HttpPut]
        public alter edit_nhomtg(int id, [FromBody] Tblnhomtg ntg)
        {
            alter result = new alter();
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblnhomtg d = db.Tblnhomtgs.SingleOrDefault(x => x.Id == id);
                    if (d== null)
                    {
                        result.ketqua = false;
                        result.thongbao = "Không tồn tại tác giả";
                        return result;
                    }
                    if (ntg.Idnv != null)
                    {
                        Tblnhomtg nhomtg = db.Tblnhomtgs.SingleOrDefault(x => x.Iddetai == ntg.Iddetai && x.Idnv == ntg.Idnv && x.Chucvu == ntg.Chucvu);
                        if (nhomtg == null)
                        {
                            d.Idnv = ntg.Idnv;
                            d.Hoten = db.Tblnhanviens.SingleOrDefault(x=>x.Id == ntg.Idnv).Hoten;
                            d.Chucvu = ntg.Chucvu;
                            db.SaveChanges();
                            result.ketqua = true;
                            result.thongbao = "Sửa thành công";
                        }
                        else
                        {
                            result.ketqua = false;
                            result.thongbao = "Tác giả đã tồn tại trong đề tài!! Không nên để trống họ tên";
                        }
                    }
                    else
                    {
                        Tblnhomtg nhomtg = db.Tblnhomtgs.SingleOrDefault(x => x.Iddetai == ntg.Iddetai && x.Hoten == ntg.Hoten && x.Chucvu == ntg.Chucvu);
                        if (nhomtg == null)
                        {
                            d.Hoten = ntg.Hoten;
                            d.Chucvu = ntg.Chucvu;
                            db.SaveChanges();
                            result.ketqua = true;
                            result.thongbao = "Sửa thành công";
                        }
                        else
                        {
                            result.ketqua = false;
                            result.thongbao = "Tác giả đã tồn tại trong đề tài!! Không nên để trống họ tên";
                        }
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.ketqua = false;
                result.thongbao = ex.Message;
                return result;
            }
        }
        [Route("delete_nhomtg/{id}")]
        [HttpDelete]
        public bool delete_nhomtg(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblnhomtg d = db.Tblnhomtgs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblnhomtgs.Remove(d);
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
