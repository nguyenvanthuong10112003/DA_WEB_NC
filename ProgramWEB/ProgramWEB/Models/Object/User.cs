using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class User
    {
        public string username { set; get; }
        public string maNhanSu { set; get; }
        public bool quyenAdmin { set; get; }
        public bool quyenQuanLy { set; get; }
        public string avatar { set; get; }
        protected QuanLyNhanSuContext context { get; set; }
        public User() { }
        public User(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar)
        {
            this.username = username;
            this.maNhanSu = maNhanSu;
            this.quyenAdmin = quyenAdmin;
            this.quyenQuanLy = quyenQuanLy;
            this.avatar = avatar;
        }
        public void init()
        {
            if (context == null)
                context = new QuanLyNhanSuContext();
        }
        public static string login(Data.TaiKhoan taiKhoan, string password)
        {
            if (taiKhoan == null)
                return "Tài khoản không tồn tại.";
            if (!BCrypt.Net.BCrypt.Verify(password, taiKhoan.TK_MatKhau))
                return "Mật khẩu đăng nhập không chính xác.";
            if (kiemTraBiKhoaVaMoKhoa(taiKhoan))
                return "Tài khoản đang bị khóa, hãy chờ đến " + taiKhoan.TK_ThoiGianMoKhoa.ToString() + ".";
            return string.Empty;
        }
        public static bool kiemTraBiKhoaVaMoKhoa(Data.TaiKhoan taiKhoan)
        {
            if (taiKhoan == null)
                return true;
            if (taiKhoan.TK_BiKhoa == false || taiKhoan.TK_BiKhoa == null)
                return false;
            if (taiKhoan.TK_ThoiGianMoKhoa == null || taiKhoan.TK_ThoiGianMoKhoa > DateTime.Now)
                return true;
            taiKhoan.TK_BiKhoa = !taiKhoan.TK_BiKhoa;
            int check = new QuanLyNhanSuContext().SaveChanges();
            if (check == 0)
                return true;
            return false;
        }
    }
}