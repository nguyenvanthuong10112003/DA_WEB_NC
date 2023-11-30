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

namespace ProgramWEB.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            if (ModelState.IsValid)
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
                TaiKhoan taiKhoan = context.TaiKhoans.Where(item => item.TK_TenDangNhap == username).FirstOrDefault();
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
                    taiKhoan.TK_QuyenAdmin.GetValueOrDefault(), taiKhoan.TK_QuyenQuanLy.GetValueOrDefault(), taiKhoan.TK_AnhDaiDien);
                } else if (taiKhoan.TK_QuyenQuanLy == true)
                {
                    userSession = new QuanLy(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                    taiKhoan.TK_QuyenAdmin.GetValueOrDefault(), taiKhoan.TK_QuyenQuanLy.GetValueOrDefault(), taiKhoan.TK_AnhDaiDien);
                } else
                {
                    userSession = new User(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                    taiKhoan.TK_QuyenAdmin.GetValueOrDefault(), taiKhoan.TK_QuyenQuanLy.GetValueOrDefault(), taiKhoan.TK_AnhDaiDien);
                }
                Session.Add(DefineSession.userSession, userSession);
                string[] beforeURL = (string[])Session[DefineSession.beforeUrlSession];
                if (beforeURL != null)
                {
                    Session.Remove(DefineSession.beforeUrlSession);
                    return RedirectToAction(beforeURL[0], beforeURL[1]);
                }
                return RedirectToAction("Index", "Home");
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
                    error = "Có lỗi xảy ra, vui lòng thử lại sau."
                });
            TaiKhoan taiKhoan = context.TaiKhoans.Find(username);
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
                taiKhoan.TK_QuyenAdmin.GetValueOrDefault(), taiKhoan.TK_QuyenQuanLy.GetValueOrDefault(), taiKhoan.TK_AnhDaiDien);
            }
            else if (taiKhoan.TK_QuyenQuanLy == true)
            {
                userSession = new QuanLy(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                taiKhoan.TK_QuyenAdmin.GetValueOrDefault(), taiKhoan.TK_QuyenQuanLy.GetValueOrDefault(), taiKhoan.TK_AnhDaiDien);
            }
            else
            {
                userSession = new User(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                taiKhoan.TK_QuyenAdmin.GetValueOrDefault(), taiKhoan.TK_QuyenQuanLy.GetValueOrDefault(), taiKhoan.TK_AnhDaiDien);
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
    }
}