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
    public class nhanvienController : ControllerBase
    {
        string _path;
        public nhanvienController(IConfiguration configuration)
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
        [Route("get_nhanvien_pagesize")]
        [HttpGet]
        public datatable<nhanvien> get_nhanvien_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<nhanvien> dv = new datatable<nhanvien>();
            List<nhanvien> ds = new List<nhanvien>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                int index = (pageindex - 1) * pagesize;
                if (!string.IsNullOrEmpty(search))
                {
                    ds = db.Tblnhanviens.Join(db.Tblphongbans, nv => nv.Idpban, pb => pb.Id, (nv, pb) => new nhanvien
                    {
                        Id = nv.Id,
                        Hoten = nv.Hoten,
                        Gioitinh = nv.Gioitinh,
                        Dienthoai = nv.Dienthoai,
                        Email = nv.Email,
                        Idpban = nv.Idpban,
                        pban = pb.Tenphongban
                    }).Where(x => x.Hoten.IndexOf(search) >= 0 || x.Dienthoai.IndexOf(search) >= 0 || x.pban.IndexOf(search) >= 0).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblnhanviens.Join(db.Tblphongbans, nv => nv.Idpban, pb => pb.Id, (nv, pb) => new nhanvien
                    {
                        Id = nv.Id,
                        Hoten = nv.Hoten,
                        Gioitinh = nv.Gioitinh,
                        Dienthoai = nv.Dienthoai,
                        Email = nv.Email,
                        Idpban = nv.Idpban,
                        pban = pb.Tenphongban
                    }).Where(x => x.Hoten.IndexOf(search) >= 0 || x.Dienthoai.IndexOf(search) >= 0 || x.pban.IndexOf(search) >= 0).Count();
                }
                else
                {
                    ds = db.Tblnhanviens.Join(db.Tblphongbans, nv => nv.Idpban, pb => pb.Id, (nv, pb) => new nhanvien
                    {
                        Id = nv.Id,
                        Hoten = nv.Hoten,
                        Gioitinh = nv.Gioitinh,
                        Dienthoai = nv.Dienthoai,
                        Email = nv.Email,
                        Idpban = nv.Idpban,
                        pban = pb.Tenphongban
                    }).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Tblnhanviens.Count();
                }
                dv.result = ds;
            }
            return dv;
        }
        [Route("get_nhanvien_id/{id}")]
        [HttpGet]
        public detailnhanvien get_nhanvien_id(int id)
        {
            detailnhanvien dv = new detailnhanvien();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                //dv = db.Tblnhanviens.SingleOrDefault(x => x.Id == id);
                dv = (from nv in db.Tblnhanviens
                      join pb in db.Tblphongbans on nv.Idpban equals pb.Id
                      join cv in db.Tblchucvus on nv.Idchucvu equals cv.Id
                      select new detailnhanvien
                      {
                          Id = nv.Id,
                          Hoten = nv.Hoten,
                          Bidanh = nv.Bidanh,
                          Hinhanh = nv.Hinhanh,
                          Gioitinh = nv.Gioitinh,
                          Ngaysinh = nv.Ngaysinh,
                          Noisinh = nv.Noisinh,
                          Cmnd = nv.Cmnd,
                          Ncapcmnd = nv.Ncapcmnd,
                          Dantoc = nv.Dantoc,
                          Tongiao = nv.Tongiao,
                          Quoctich = nv.Quoctich,
                          Tthonnhan = nv.Tthonnhan,
                          Quequan = nv.Quequan,
                          Dcttru = nv.Dcttru,
                          Noiohnay = nv.Noiohnay,
                          Dienthoai = nv.Dienthoai,
                          Email = nv.Email,
                          Idpban = nv.Idpban,
                          Idchucvu = nv.Idchucvu,
                          Tdhocvan = nv.Tdhocvan,
                          Datotnghiep = nv.Datotnghiep,
                          Tdcaonhat = nv.Tdcaonhat,
                          Ngdaotao = nv.Ngdaotao,
                          Cngdaotao = nv.Ngdaotao,
                          Noidaotao = nv.Noidaotao,
                          Htdaotao = nv.Htdaotao,
                          Mantn = nv.Mantn,
                          Tinhtrang = nv.Tinhtrang,
                          Trinhdonn = nv.Trinhdonn,
                          Tinhoc = nv.Tinhoc,
                          pban = pb.Tenphongban,
                          chucvu = cv.Tenchucvu
                      }).SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_nhanvien")]
        [HttpPost]
        public bool create_nhanvien([FromBody] Tblnhanvien nv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    if (nv.Hinhanh != null)
                    {
                        var arrData = nv.Hinhanh.Split(';');
                        if (arrData.Length == 3)
                        {
                            var savePath = $@"assets/images/emloye/{arrData[0]}";
                            nv.Hinhanh = $"{arrData[0]}";
                            SaveFileFromBase64String(savePath, arrData[2]);
                        }
                    }
                    db.Tblnhanviens.Add(nv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_nhanvien/{id}")]
        [HttpPut]
        public bool edit_nhanvien(int id, [FromBody] Tblnhanvien nv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblnhanvien d = db.Tblnhanviens.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    if (nv.Hinhanh != null)
                    {
                        var arrData = nv.Hinhanh.Split(';');
                        if (arrData.Length == 3)
                        {
                            var savePath = $@"assets/images/emloye/{arrData[0]}";
                            nv.Hinhanh = $"{arrData[0]}";
                            SaveFileFromBase64String(savePath, arrData[2]);
                        }
                    }
                    else
                    {
                        nv.Hinhanh = d.Hinhanh;
                    }
                    d.Hoten = nv.Hoten;
                    d.Bidanh = nv.Bidanh;
                    d.Hinhanh = nv.Hinhanh;
                    d.Gioitinh = nv.Gioitinh;
                    d.Ngaysinh = nv.Ngaysinh;
                    d.Noisinh = nv.Noisinh;
                    d.Cmnd = nv.Cmnd;
                    d.Ncapcmnd = nv.Ncapcmnd;
                    d.Dantoc = nv.Dantoc;
                    d.Tongiao = nv.Tongiao;
                    d.Quoctich = nv.Quoctich;
                    d.Tthonnhan = nv.Tthonnhan;
                    d.Quequan = nv.Quequan;
                    d.Dcttru = nv.Dcttru;
                    d.Noiohnay = nv.Noiohnay;
                    d.Dienthoai = nv.Dienthoai;
                    d.Email = nv.Email;
                    d.Idpban = nv.Idpban;
                    d.Idchucvu = nv.Idchucvu;
                    d.Tdhocvan = nv.Tdhocvan;
                    d.Tdcaonhat = nv.Tdcaonhat;
                    d.Ngdaotao = nv.Ngdaotao;
                    d.Cngdaotao = nv.Cngdaotao;
                    d.Noidaotao = nv.Noidaotao;
                    d.Htdaotao = nv.Htdaotao;
                    d.Trinhdonn = nv.Trinhdonn;
                    d.Tinhoc = nv.Tinhoc;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_nhanvien/{id}")]
        [HttpDelete]
        public bool delete_nhanvien(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblnhanvien d = db.Tblnhanviens.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblnhanviens.Remove(d);
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
