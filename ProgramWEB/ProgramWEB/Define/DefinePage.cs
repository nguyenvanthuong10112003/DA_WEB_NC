﻿using ProgramWEB.Models;
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
        public static Page management { get; } = new Page("Quản lý", "/Management/Index");
        public static Page management_NhanSu { get; } = new Page("Nhân sự", "/Management/NhanSu");
        public static Page management_PhongBan { get; } = new Page("Phòng ban", "/Management/PhongBan");
        public static Page management_BoPhan { get; } = new Page("Bộ phận", "/Management/BoPhan");
        public static Page management_KhenThuongKyLuat { get; } = new Page("Khen thưởng kỷ luật", "/Management/KhenThuongKyLuat");
        public static Page management_TaiKhoan { get; } = new Page("Tài khoản", "/Management/TaiKhoan");
        public static Page management_BaoHiem { get; } = new Page("Bảo hiểm", "/Management/BaoHiem");
        public static Page management_ChamCong { get; } = new Page("Chấm công", "/Management/ChamCong");
        public static Page management_HopDong { get; } = new Page("Hợp đồng", "/Management/HopDong");
        public static Page management_LichSuLamViec { get; } = new Page("Lịch sử làm việc", "/Management/LichSuLamViec");
        public static Page management_CaLam { get; } = new Page("Ca làm", "/Management/CaLam");
        public static Page management_NgayNghi { get; } = new Page("Ngày nghỉ", "/Management/NgayNghi");
        public static Page management_DangKyNghiLam { get; } = new Page("Đăng ký nghỉ làm", "/Management/DangKyNghiLam");
        public static Page management_DangKyCaLam { get; } = new Page("Đăng ký ca làm", "/Management/DangKyCaLam");
        public static Page management_DuyetDangKy { get; } = new Page("Duyệt đăng ký", "/Management/DuyetDangKy");
        public static Page profile_NhanSu { get; } = new Page("Profile", "/NhanSu/Profile");
        public static Page caLam { get; } = new Page("CaLam", "/CaLam/Index");
        public static Page lichLamViec_CaLam { get; } = new Page("LichLamViec", "/CaLam/LichLamViec");
    }
}