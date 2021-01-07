using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class hosoController : ControllerBase
    {
        public string _path;
        public hosoController(IConfiguration configuration)
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
        [Route("get_hoso_iddetai/{iddetai}")]
        [HttpGet]
        public List<Tblhoso> get_hoso_iddetai(int iddetai)
        {
            datatable<Tblhoso> dv = new datatable<Tblhoso>();
            List<Tblhoso> ds = new List<Tblhoso>();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {

                return db.Tblhosos.Where(x => x.Iddetai == iddetai).ToList();
            }
        }
        [Route("get_hoso_id/{id}")]
        [HttpGet]
        public Tblhoso get_hoso_id(int id)
        {
            Tblhoso dv = new Tblhoso();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Tblhosos.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_hoso")]
        [HttpPost]
        public bool create_hoso([FromBody] Tblhoso dv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    if (dv.Minhchung != null)
                    {
                        var arrData = dv.Minhchung.Split(';');
                        if (arrData.Length == 3)
                        {
                            var savePath = $@"assets/file/{arrData[0]}";
                            dv.Minhchung = $"{arrData[0]}";
                            SaveFileFromBase64String(savePath, arrData[2]);
                        }
                    }
                    db.Tblhosos.Add(dv);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_hoso/{id}")]
        [HttpPut]
        public bool edit_hoso(int id, [FromBody] Tblhoso hs)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblhoso d = db.Tblhosos.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return false;
                    if (hs.Minhchung != null)
                    {
                        var arrData = hs.Minhchung.Split(';');
                        if (arrData.Length == 3)
                        {
                            var savePath = $@"assets/file/{arrData[0]}";
                            hs.Minhchung = $"{arrData[0]}";
                            SaveFileFromBase64String(savePath, arrData[2]);
                        }
                    }
                    else
                    {
                        hs.Minhchung = d.Minhchung;
                    }
                    d.Ten = hs.Ten;
                    d.Ngay = hs.Ngay;
                    d.Minhchung = hs.Minhchung;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("delete_hoso/{id}")]
        [HttpDelete]
        public bool delete_hoso(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    Tblhoso d = db.Tblhosos.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Tblhosos.Remove(d);
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
