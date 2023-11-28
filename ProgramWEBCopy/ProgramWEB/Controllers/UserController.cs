using ProgramWEB.Define;
using ProgramWEB.Models.DAO;
using ProgramWEB.Models.Data;
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
                if (username == null || password == null ||
                    string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                    return View();
                TaiKhoanDAO taiKhoanDAO = new TaiKhoanDAO();
                TaiKhoan taiKhoan = taiKhoanDAO.getTaiKhoanByUsername(username);
                if (taiKhoan == null || taiKhoan.TK_MatKhau != password || taiKhoanDAO.checkLocked(taiKhoan))
                {
                    //Xoa khoi cookie
                    removeFromCookie(new string[] { DefineCookie.cookieUsername, DefineCookie.cookiePassword });
                    return View();
                }
                //Them vao session
                UserLogin userSession = new UserLogin(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma.Trim(),
                    taiKhoan.TK_QuyenAdmin.GetValueOrDefault(), taiKhoan.TK_QuyenQuanLy.GetValueOrDefault(), taiKhoan.TK_AnhDaiDien);
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
            TaiKhoanDAO taiKhoanDAO = new TaiKhoanDAO();
            TaiKhoan taiKhoan = taiKhoanDAO.getTaiKhoanByUsername(username);
            string message = CheckLogin(taiKhoan, password);
            if (!string.IsNullOrEmpty(message))
                return JsonConvert.SerializeObject(new
                {
                    error = message
                });
            Session.Add(DefineSession.userSession, new UserLogin(taiKhoan.TK_TenDangNhap, taiKhoan.NS_Ma, taiKhoan.TK_QuyenAdmin.Value, taiKhoan.TK_QuyenQuanLy.Value, taiKhoan.TK_AnhDaiDien));
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
        [HttpPost]
        public string CheckLogin(TaiKhoan taiKhoan, string password)
        {
            if (taiKhoan == null)
                return "Tài khoản không tồn tại.";
            if (!BCrypt.Net.BCrypt.Verify(password, taiKhoan.TK_MatKhau))
                return "Mật khẩu đăng nhập không chính xác";
            if (new TaiKhoanDAO().checkLocked(taiKhoan))
                return "Tài khoản đang bị khóa, hãy chờ đến " + taiKhoan.TK_ThoiGianMoKhoa.ToString();
            return string.Empty;
        }
    }
}