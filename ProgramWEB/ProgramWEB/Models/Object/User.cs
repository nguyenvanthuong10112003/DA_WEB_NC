using ProgramWEB.Define;
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
            if (taiKhoan.TK_BiKhoa == false)
                return false;
            if (taiKhoan.TK_ThoiGianMoKhoa == null || taiKhoan.TK_ThoiGianMoKhoa > DateTime.Now)
                return true;
            taiKhoan.TK_BiKhoa = !taiKhoan.TK_BiKhoa;
            int check = new QuanLyNhanSuContext().SaveChanges();
            if (check == 0)
                return true;
            return false;
        }
        public NhanSu getNhanSu()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                Data.NhanSu nhanSuData = context.NhanSus.Where(item => item.NS_Ma.Trim() == this.maNhanSu).FirstOrDefault();
                if (nhanSuData == null)
                    return null;
                NhanSu nhanSu = new NhanSu();
                Libary.Convert<NhanSu, Data.NhanSu>.ConvertObj(ref nhanSu, nhanSuData);
                return nhanSu;
            }
            catch { }
            return null;
        }
        public TaiKhoan getTaiKhoan()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                Data.TaiKhoan data = context.TaiKhoans.Find(this.username);
                if (data == null)
                    return null;
                TaiKhoan result = new TaiKhoan();
                Libary.Convert<TaiKhoan, Data.TaiKhoan>.ConvertObj(ref result, data);
                return result;
            }
            catch { }
            return null;
        }
        public IEnumerable<BaoHiem> getBaoHiems()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Data.BaoHiem> datas = context.BaoHiems.Where(item => item.NS_Ma.Trim() == this.maNhanSu);
                if (datas == null)
                    return null;
                return Libary.Convert<BaoHiem, Data.BaoHiem>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public IEnumerable<ChamCong> getChamCongs()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Data.ChamCong> datas = context.ChamCongs.Where(item => item.NS_Ma.Trim() == this.maNhanSu);
                if (datas == null)
                    return null;
                return Libary.Convert<ChamCong, Data.ChamCong>.ConvertObjs(datas);
            }
            catch { }
            return null;
        } 
        public IEnumerable<KhenThuongKyLuat> getKhenThuongKyLuats()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Data.KhenThuongKyLuat> datas = context.KhenThuongKyLuats.Where(item => item.NS_Ma.Trim() == this.maNhanSu);
                if (datas == null)
                    return null;
                return Libary.Convert<KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public IEnumerable<LichSuLamViec> getLichSuLamViecs()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Data.LichSuLamViec> datas = context.LichSuLamViecs.Where(item => item.NS_Ma.Trim() == this.maNhanSu);
                if (datas == null)
                    return null;
                return Libary.Convert<LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public IEnumerable<HopDong> getHopDongs()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Data.HopDong> datas = context.HopDongs.Where(item => item.NS_Ma.Trim() == this.maNhanSu);
                if (datas == null)
                    return null;
                return Libary.Convert<HopDong, Data.HopDong>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public IEnumerable<DangKyNghiLam> getDangKyNghiLams()
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Data.DangKyNghiLam> datas = context.DangKyNghiLams.Where(item => item.NS_Ma.Trim() == this.maNhanSu);
                if (datas == null)
                    return null;
                return Libary.Convert<DangKyNghiLam, Data.DangKyNghiLam>.ConvertObjs(datas);
            }
            catch { }
            return null;
        }
        public bool thayDoiAnhDaiDien(string newUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(newUrl))
                    return false;
                init();
                if (context == null)
                    return false;
                Data.TaiKhoan taiKhoan = context.TaiKhoans.Find(this.username);
                if (taiKhoan == null) return false;
                taiKhoan.TK_AnhDaiDien = newUrl;
                int check = context.SaveChanges();
                if (check == 0)
                    return false;
                return true;
            } catch { }
            return false;
        }
    }
}