using ProgramWEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramWEB.Define
{
    public class DefinePage
    {
        public static Page home { get; } = new Page("Home", "/Home/Index");
        public static Page start { get; } = new Page("Start", "/Home/Start");
        public static Page user { get; } = new Page("User", "/User/Index");
        public static Page user_ChangePassword { get; } = new Page("Change Password", "/User/ChangePassword");
        public static Page user_ForgetPassword { get; } = new Page("Forget Password", "/User/ForgetPassword");
        public static Page user_Authentication { get; } = new Page("Authentication", "/User/Authentication");
        public static Page user_Logout { get; } = new Page("Logout", "/User/Logout");
        public static Page user_Login { get; } = new Page("Login", "/User/Login");
        public static Page user_Profile { get; } = new Page("Profile", "/User/Profile");
        public static Page user_Register { get; } = new Page("Register", "/User/Register");
        public static Page management { get; } = new Page("Management", "/Management/Index");
        public static Page management_NhanSu { get; } = new Page("NhanSu", "/Management/NhanSu");
        public static Page management_PhongBan { get; } = new Page("ThanhVien", "/Management/PhongBan");
        public static Page management_BoPhan { get; } = new Page("ThanhVien", "/Management/BoPhan");
        public static Page management_KhenThuongKyLuat { get; } = new Page("KhenThuongKyLuat", "/Management/KhenThuongKyLuat");
        public static Page management_TaiKhoan { get; } = new Page("TaiKhoan", "/Management/TaiKhoan");
        public static Page management_BaoHiem { get; } = new Page("BaoHiem", "/Management/BaoHiem");
        public static Page management_HopDong { get; } = new Page("HopDong", "/Management/HopDong");
        public static Page management_LichSuLamViec { get; } = new Page("LichSuLamViec", "Management/LichSuLamViec");
        public static Page management_CaLam { get; } = new Page("CaLam", "Management/CaLam");
        public static Page management_NgayNghi { get; } = new Page("NgayNghi", "Management/NgayNghi");
        public static Page thanhvien { get; } = new Page("ThanhVien", "/ThanhVien/Index");
        public static Page profile_ThanhVien { get; } = new Page("Profile", "/ThanhVien/Profile");
        public static Page caLam { get; } = new Page("CaLam", "/CaLam/Index");
        public static Page lichLamViec_CaLam { get; } = new Page("LichLamViec", "/CaLam/LichLamViec");
    }
}