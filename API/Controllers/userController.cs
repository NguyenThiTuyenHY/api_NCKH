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
    public class userController : ControllerBase
    {
        public string _path;
        public userController(IConfiguration configuration)
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
        [Route("get_user_pagesize")]
        [HttpGet]
        public datatable<User> get_user_pagesize(int pagesize, int pageindex, string search)
        {
            datatable<User> dv = new datatable<User>();
            List<User> ds = new List<User>();
            int role_ = 0;
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                int index = (pageindex - 1) * pagesize;
                if (!string.IsNullOrEmpty(search))
                {
                    if (search.ToUpper() == "CÁN BỘ")
                    {
                        role_ = 3;
                    }
                    if (search.ToUpper() == "ADMIN")
                    {
                        role_ = 1;
                    }
                    if (search.ToUpper() == "NHÂN VIÊN")
                    {
                        role_ = 2;
                    }
                    ds = db.Users.Where(x => x.Taikhoan.IndexOf(search) >= 0 || x.Idrole == role_).Skip(index).Take(pagesize).ToList();
                    dv.total = db.Users.Where(x => x.Taikhoan.IndexOf(search) >= 0 || x.Idrole == role_).Count();
                }
                else
                {
                    ds = db.Users.Skip(index).Take(pagesize).ToList();
                    dv.total = db.Users.Count();
                }
                dv.result = ds;
            }
            return dv;
        }
        [Route("get_user_login")]
        [HttpGet]
        public nguoidung get_user_login(string username, string password)
        {
            try
            {
                nguoidung nd = new nguoidung();
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    nguoidung user = db.Users.Join(db.Roles, u => u.Idrole, r => r.Id, (u, r) => new nguoidung
                    {
                        Id = u.Id,
                        Taikhoan = u.Taikhoan,
                        Matkhau = u.Matkhau,
                        Token = u.Token,
                        Role = r.Id,
                        Hinhanh = u.Hinhanh,
                        Idnv = u.Idnhanvien
                    }).SingleOrDefault(x => x.Taikhoan.IndexOf(username) >= 0 && x.Matkhau.IndexOf(password) >= 0);
                    return user;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_user_login_bool")]
        [HttpGet]
        public alter get_user_login_bool(string username, string password)
        {
            try
            {
                alter nd = new alter();
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User u = db.Users.SingleOrDefault(x => x.Taikhoan == username);
                    if (u == null)
                    {
                        nd.ketqua = false;
                        nd.thongbao = "Tài khoản không tồn tại";
                    }
                    else
                    {
                        if(u.Matkhau == password)
                        {
                            if(u.Trangthai == 1)
                            {
                                nd.ketqua = true;
                                nd.thongbao = "Đăng nhập thành công";
                            }
                            else
                            {
                                nd.ketqua = false;
                                nd.thongbao = "Tài khoản của bạn đã bị xoá! Liên hệ với Admin để biết thêm chi tiết";
                            }
                        }
                        else
                        {
                            nd.ketqua = false;
                            nd.thongbao = "Mật khẩu không tồn tại";
                        }
                    }
                    return nd;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        [Route("get_user_id/{id}")]
        [HttpGet]
        public User get_user_id(int id)
        {
            User dv = new User();
            using (sql_NCKHContext db = new sql_NCKHContext())
            {
                dv = db.Users.SingleOrDefault(x => x.Id == id);
            }
            return dv;
        }
        [Route("create_user")]
        [HttpPost]
        public bool create_user([FromBody] User us)
        {
            try
            {
                if (us.Hinhanh != null)
                {
                    var arrData = us.Hinhanh.Split(';');
                    if (arrData.Length == 3)
                    {
                        var savePath = $@"assets/images/user/{arrData[0]}";
                        us.Hinhanh = $"{arrData[0]}";
                        SaveFileFromBase64String(savePath, arrData[2]);
                    }
                }
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    us.Trangthai = 1;
                    db.Users.Add(us);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("edit_user_matkhau")]
        [HttpPost]
        public alter edit_user_matkhau([FromBody] use us)
        {
            alter al = new alter();
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User d = db.Users.SingleOrDefault(x => x.Taikhoan.IndexOf(us.Taikhoan) >= 0 && x.Matkhau.IndexOf(us.Matkhaucu) >= 0);
                    if (d==null)
                    {
                        al.ketqua = false;
                        al.thongbao = "Khách hàng không tồn tại";

                    }
                    else
                    {
                        if (d.Matkhau.IndexOf(us.Matkhau) >= 0)
                        {
                            al.ketqua = false;
                            al.thongbao = "Mật khẩu cũ và mật khẩu mới trùng, xin đổi mật khẩu khác";
                        }
                        al.ketqua = true;
                        al.thongbao = "Đổi mật khẩu thành công";
                        d.Matkhau = us.Matkhau;
                        db.SaveChanges();
                    }
                }
                return al;
            }
            catch (Exception ex)
            {
                al.ketqua = true;
                al.thongbao = ex.Message;
                return al;
            }
        }
        [Route("delete_user/{id}")]
        [HttpDelete]
        public bool delete_user(int id)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User d = db.Users.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(d.ToString()))
                        return true;
                    db.Users.Remove(d);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("change_pass")]
        [HttpGet]
        public bool change_pass(int id, string pass)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User result = db.Users.SingleOrDefault(x => x.Id == id);
                    if (string.IsNullOrEmpty(result.ToString()))
                        return false;
                    result.Matkhau = pass;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("change_user/{id}/{idnv}")]
        [HttpGet]
        public bool change_user(int id, int idnv)
        {
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User us = db.Users.SingleOrDefault(x => x.Id == id);
                    if (us == null)
                    {
                        return false;
                    }
                    else
                    {
                        us.Idnhanvien = idnv;
                        db.SaveChanges();
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        [Route("change_status/{id}")]
        public alter change_status(int id)
        {
            alter result = new alter();
            try
            {
                using (sql_NCKHContext db = new sql_NCKHContext())
                {
                    User nguoidung = db.Users.SingleOrDefault(x => x.Id == id);
                    if (nguoidung == null)
                    {
                        result.ketqua = false;
                        result.thongbao = "Không tồn tại tài khoản này";
                    }
                    else
                    {
                        if (nguoidung.Trangthai == 1)
                        {
                            nguoidung.Trangthai = 2;
                            result.thongbao = "Tầi khoản này đã bị vô hiệu hoá";
                        }
                        else
                        {
                            nguoidung.Trangthai = 1;
                            result.thongbao = "Tầi khoản này hoạt động bình thường";
                        }
                        result.ketqua = true;
                        db.SaveChanges();
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
        public user_key get_report_user()
        {
            using(sql_NCKHContext db = new sql_NCKHContext())
            {
                user_key a = new user_key();
                a.tonguser = db.Users.Count();
                a.dsuser = db.Users.Where(x => x.Trangthai == 2).ToList();
                a.tongkey = a.dsuser.Count();
                return a;
            }
        }
    }
}
