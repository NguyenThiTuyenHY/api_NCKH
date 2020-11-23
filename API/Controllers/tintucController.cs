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
                        Tenloaitt = ltt.Tenloaitt
                    }).Where(x => x.Tieude.IndexOf(search) >= 0).Skip(pageindex).Take(pagesize).ToList();
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
                        Tenloaitt = ltt.Tenloaitt
                    }).Skip(pageindex).Take(pagesize).ToList();
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
        [Route("delete_loaitintuc/{id}")]
        [HttpDelete]
        public bool delete_loaitintuc(int id)
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
    }
}
