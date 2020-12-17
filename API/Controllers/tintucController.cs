using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class tintucController : ControllerBase
    {
        public string _path;
        public tintucController(IConfiguration configuration)
        {
            _path = configuration["AppSettings:PATH"];
        }
        public string SaveFileFromBase64String(string RelativePathFileName, string dataFromBase64String)
        {
            if (dataFromBase64String.Contains("base64,"))
            {
                dataFromBase64String = dataFromBase64String.Substring(dataFromBase64String.IndexOf("base64,", 0) + 7);
            }
            return WriteFileToAuthAccessFolder(RelativePathFileName, dataFromBase64String);
        }
        public string WriteFileToAuthAccessFolder(string RelativePathFileName, string base64StringData)
        {
            try
            {
                string result = "";
                string serverRootPathFolder = _path;
                string fullPathFile = $@"{serverRootPathFolder}\{RelativePathFileName}";
                string fullPathFolder = System.IO.Path.GetDirectoryName(fullPathFile);
                if (!Directory.Exists(fullPathFolder))
                    Directory.CreateDirectory(fullPathFolder);
                System.IO.File.WriteAllBytes(fullPathFile, Convert.FromBase64String(base64StringData));
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [Route("get_tintuc_pagesize")]
        [HttpGet]
        public datatable<tintucloai> get_tintuc_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<tintucloai> dv = new datatable<tintucloai>();
            int index = (pageindex - 1) * pagesize;
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                if (!string.IsNullOrEmpty(search))
                {
                    dv.result = db.Tbltintucs.Join(db.Tblloaitts, tt => tt.Idloai, ltt => ltt.Id, (tt, ltt) => new tintucloai
                    {
                        Id = tt.Id,
                        Tieude = tt.Tieude,
                        Hinhanh = tt.Hinhanh,
                        Idloai = tt.Idloai,
                        Noidung = tt.Noidung,
                        Luotem = tt.Luotem,
                        Ngaydang = tt.Ngaydang,
                        Tenloaitt = ltt.Tenloaitt
                    }).Where(x => x.Tieude.IndexOf(search) >= 0).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tbltintucs.Where(x => x.Tieude.IndexOf(search) >= 0).Count();
                }
                else
                {
                    dv.result = db.Tbltintucs.Join(db.Tblloaitts, tt => tt.Idloai, ltt => ltt.Id, (tt, ltt) => new tintucloai
                    {
                        Id = tt.Id,
                        Tieude = tt.Tieude,
                        Hinhanh = tt.Hinhanh,
                        Idloai = tt.Idloai,
                        Noidung = tt.Noidung,
                        Luotem = tt.Luotem,
                        Ngaydang = tt.Ngaydang,
                        Tenloaitt = ltt.Tenloaitt
                    }).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tbltintucs.Count();
                }
            }
            return dv;
        }
        [Route("get_tintuc_idloai")]
        [HttpGet]
        public datatable<Tbltintuc> get_tintuc_idloai(int pagesize, int pageindex, int idloai)
        {
            datatable<Tbltintuc> dv = new datatable<Tbltintuc>();
            List<Tbltintuc> ds = new List<Tbltintuc>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ds = db.Tbltintucs.Where(x => x.Idloai == idloai).Skip(pageindex).Take(pagesize).ToList();
                foreach (Tbltintuc tt in ds)
                {
                    tt.IdloaiNavigation.Tenloaitt = db.Tblloaitts.SingleOrDefault(x => x.Id == tt.Idloai).Tenloaitt;
                }
                dv.total = db.Tbltintucs.Where(x => x.Idloai == idloai).Count();
            }
            return dv;
        }
        [Route("get_tintuc_id/{id}")]
        [HttpGet]
        public Tbltintuc get_tintuc_id(int id)
        {
            Tbltintuc ltt = new Tbltintuc();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                ltt = db.Tbltintucs.SingleOrDefault(x => x.Id == id);
            }
            return ltt;
        }
        [Route("create_tintuc")]
        [HttpPost]
        public bool create_tintuc([FromBody] Tbltintuc tt)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    if (tt.Hinhanh != null)
                    {
                        var arrData = tt.Hinhanh.Split(';');
                        if (arrData.Length == 3)
                        {
                            var savePath = $@"assets/images/news/{arrData[0]}";
                            tt.Hinhanh = $"{arrData[0]}";
                            SaveFileFromBase64String(savePath, arrData[2]);
                        }
                    }
                    tt.Luotem = 0;
                    db.Tbltintucs.Add(tt);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        [Route("edit_tintuc/{id}")]
        [HttpPut]
        public bool edit_tintuc(int id, [FromBody] Tbltintuc tt)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbltintuc d = db.Tbltintucs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    d.Tieude = tt.Tieude;
                    if (tt.Hinhanh != null)
                    {
                        var arrData = tt.Hinhanh.Split(';');
                        if (arrData.Length == 3)
                        {
                            var savePath = $@"assets/images/{arrData[0]}";
                            tt.Hinhanh = $"{arrData[0]}";
                            SaveFileFromBase64String(savePath, arrData[2]);
                        }
                    }
                    else
                    {
                        tt.Hinhanh = d.Hinhanh;
                    }
                    d.Hinhanh = tt.Hinhanh;
                    d.Idloai = tt.Idloai;
                    d.Noidung = tt.Noidung;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_tintuc/{id}")]
        [HttpDelete]
        public bool delete_tintuc(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tbltintuc d = db.Tbltintucs.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tbltintucs.Remove(d);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("get_tintuc_idloai/{id}")]
        [HttpGet]
        public List<Tbltintuc> get_tintuc_idloai(int id)
        {
            try
            {
                using(sql_NCKHContext db = new sql_NCKHContext())
                {
                    return db.Tbltintucs.Where(x => x.Idloai == id).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_tintuc_slider")]
        [HttpGet]
        public List<Tbltintuc> get_tintuc_slider()
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    return db.Tbltintucs.OrderByDescending(x=>x.Id).Skip(0).Take(3).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_tintuc_idloai_pagesize")]
        [HttpGet]
        public List<Tbltintuc> get_tintuc_idloai_pagesize(int id, int pagesize)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    return db.Tbltintucs.OrderByDescending(x => x.Id).Where(x=>x.Idloai == id).Skip(1).Take(pagesize).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_tintuc_idloai_singer")]
        [HttpGet]
        public Tbltintuc get_tintuc_idloai_singer(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    return db.Tbltintucs.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Idloai == id);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_tintuc_idloai_singer_new")]
        [HttpGet]
        public Tbltintuc get_tintuc_idloai_singer_new()
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    return db.Tbltintucs.OrderByDescending(x => x.Id).First();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_tintuc_idloai_new")]
        [HttpGet]
        public List<Tbltintuc> get_tintuc_idloai_new()
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    return db.Tbltintucs.OrderByDescending(x => x.Id).Skip(1).Take(3).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_tintuc_popular")]
        [HttpGet]
        public List<Tbltintuc> get_tintuc_popular()
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    return db.Tbltintucs.OrderByDescending(x => x.Luotem).Skip(0).Take(3).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
