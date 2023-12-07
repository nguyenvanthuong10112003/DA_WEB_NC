using ProgramWEB.Define;
using ProgramWEB.Models.Data;
using ProgramWEB.Models.Object;
using ProgramWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BCrypt.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web.UI;

namespace ProgramWEB.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Session[DefineSession.userSession] != null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    //Lay du lieu tu cookie
                    if (Request.Cookies[DefineCookie.cookieUsername] == null || Request.Cookies[DefineCookie.cookiePassword] == null)
                        return View();
                    var username = Request.Cookies[DefineCookie.cookieUsername].Value;
                    var password = Request.Cookies[DefineCookie.cookiePassword].Value;
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                        return View();
                    QuanLyNhanSuContext context = new QuanLyNhanSuContext();
                    if (context == null)
                        return View();
                    Models.Data.TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.TK_TenDangNhap == username).FirstOrDefault();
                    if (taiKhoan == null || taiKhoan.TK_MatKhau != password || Models.Object.User.kiemTraBiKhoaVaMoKhoa(taiKhoan))
                    {
                        //Xoa khoi cookie
                        removeFromCookie(new string[] { DefineCookie.cookieUsername, DefineCookie.cookiePassword });
                        return View();
                    }
                    //Them vao session
                    User userSession;
                    if (taiKhoan.TK_QuyenAdmin == true)
                    {
                        userSession = new Admin(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                        taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                    }
                    else if (taiKhoan.TK_QuyenQuanLy == true)
                    {
                        userSession = new QuanLy(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                        taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                    }
                    else
                    {
                        userSession = new User(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                        taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
                    }
                    Session.Add(DefineSession.userSession, userSession);
                    string[] beforeURL = (string[])Session[DefineSession.beforeUrlSession];
                    if (beforeURL != null)
                    {
                        Session.Remove(DefineSession.beforeUrlSession);
                        return RedirectToAction(beforeURL[0], beforeURL[1]);
                    }
                    return RedirectToAction("Index", "Home");
                } catch { }
            }
            return View();
        }
        [HttpPost]
        public string ToLogin(string username, string password, bool keepLogin = false)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || 
                username.Length > 30 || password.Length < 6)
                return JsonConvert.SerializeObject(new
                {
                    error = "Thông tin gửi lên không hợp lệ."
                });
            QuanLyNhanSuContext context = new QuanLyNhanSuContext();
            if (context == null)
                return JsonConvert.SerializeObject(new
                {
                    error = DefineError.loiHeThong
                });
            Models.Data.TaiKhoan taiKhoan = context.TaiKhoans.Find(username);
            string message = Models.Object.User.login(taiKhoan, password);
            if (!string.IsNullOrEmpty(message))
                return JsonConvert.SerializeObject(new
                {
                    error = message
                });
            User userSession;
            if (taiKhoan.TK_QuyenAdmin == true)
            {
                userSession = new Admin(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
            }
            else if (taiKhoan.TK_QuyenQuanLy == true)
            {
                userSession = new QuanLy(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
            }
            else
            {
                userSession = new User(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                taiKhoan.TK_QuyenAdmin, taiKhoan.TK_QuyenQuanLy, taiKhoan.TK_AnhDaiDien);
            }
            Session.Add(DefineSession.userSession, userSession);
            if (keepLogin)
            {
                DateTime timeSave = DateTime.Now.AddDays(30);
                Response.AppendCookie(new HttpCookie(DefineCookie.cookieUsername, taiKhoan.TK_TenDangNhap) { Expires = timeSave });
                Response.AppendCookie(new HttpCookie(DefineCookie.cookiePassword, taiKhoan.TK_MatKhau) { Expires = timeSave });
            }
            return JsonConvert.SerializeObject(new
            {
                success = "Đăng nhập thành công",
                url = "Home"
            });
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult Logout()
        {
            removeFromCookie(new string[] {DefineCookie.cookieUsername, DefineCookie.cookiePassword});
            Session.Remove(DefineSession.userSession);
            return RedirectToAction("Index","Home");
        }
        public void removeFromCookie(string[] cookiesName)
        {
            DateTime dateTime = DateTime.Now.AddDays(-1);
            for (int i = 0; i < cookiesName.Length; i++)
            {
                Response.AppendCookie(
                    new HttpCookie(cookiesName[i], null)
                    {
                        Expires = dateTime,
                    }
                );
            }
        }
        public ActionResult Profile()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = (User)Session[DefineSession.userSession];
                    if (user == null)
                        return Login();
                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("nhanSu", user.getNhanSu());
                    data.Add("baoHiem", user.getBaoHiems());
                    data.Add("nghiLam", user.getDangKyNghiLams());
                    data.Add("hopDong", user.getHopDongs());
                    data.Add("lichSu", user.getLichSuLamViecs());
                    data.Add("khenThuongKyLuat", user.getKhenThuongKyLuats());
                    data.Add("chamCong", user.getChamCongs());
                    ViewBag.data = data;
                } catch { }
            }
            return View();
        }
        [HttpPost]
        public ActionResult Profile(HttpPostedFileBase changeAvatar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = (User)Session[DefineSession.userSession];
                    if (user == null)
                        return Login();
                    if (changeAvatar == null || changeAvatar.ContentLength == 0)
                        return Profile();
                    var url = "/Images/avatar/";

                    var urlSV = Server.MapPath(url);
                    var date = DateTime.Now;
                    var fileNewName = user.maNhanSu.Trim() + "-" + date.Year + "-" + date.Month + "-" + date.Day + "-" +
                        date.Hour + "-" + date.Minute + "-" + date.Second + "-" + date.Millisecond + "-" + changeAvatar.FileName;
                    changeAvatar.SaveAs(urlSV + fileNewName);

                    string old = user.avatar;

                    if (!user.thayDoiAnhDaiDien(fileNewName))
                        return Profile();

                    user.avatar = fileNewName;

                    FileInfo file = new FileInfo(Server.MapPath(url + old));
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                }
                catch { }
            }
            return Profile();
        }
    }
}