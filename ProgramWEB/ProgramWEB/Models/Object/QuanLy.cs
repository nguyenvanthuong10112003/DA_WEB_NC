using Newtonsoft.Json;
using PagedList;
using ProgramWEB.Libary;
using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class QuanLy : User
    {
        public QuanLy() : base() 
        { 
            
        }
        public QuanLy(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar) : base(username, maNhanSu, quyenAdmin, quyenQuanLy, avatar)
        {
        }
        public IEnumerable<NhanSu> layDanhSachNhanSu()
        {
            try
            {
                init();
                if (context != null) 
                    return context.NhanSus;      
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<NhanSu>> layDanhSachNhanSu(NhanSu findBy = null, int page = 1, int pageSize = 10, string sortBy = "NS_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<NhanSu> results =
                    (findBy != null ? timKiemNhieuNhanSu(findBy)
                    : (from NhanSu in context.NhanSus select NhanSu));
                results = ObjectHelper.OrderByDynamic<NhanSu>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page;
                return new List<IEnumerable<NhanSu>>()
                {
                    results,
                    results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public IEnumerable<NhanSu> timKiemNhieuNhanSu(NhanSu findNhanSu = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (findNhanSu != null)
                {
                    return (from NhanSu in context.NhanSus
                            where
                            (findNhanSu.NS_Ma != null ? NhanSu.NS_Ma.StartsWith(findNhanSu.NS_Ma) : NhanSu != null) &&
                            (findNhanSu.NS_HoVaTen != null ? NhanSu.NS_HoVaTen.StartsWith(findNhanSu.NS_HoVaTen) : NhanSu != null) &&
                            (findNhanSu.NS_GioiTinh != null ? NhanSu.NS_GioiTinh.Value == findNhanSu.NS_GioiTinh.Value : NhanSu != null) &&
                            (findNhanSu.NS_NgaySinh != null ? NhanSu.NS_NgaySinh == findNhanSu.NS_NgaySinh : NhanSu != null) &&
                            (findNhanSu.NS_SoDienThoai != null ? NhanSu.NS_SoDienThoai.StartsWith(findNhanSu.NS_SoDienThoai) : NhanSu != null) &&
                            (findNhanSu.NS_Email != null ? NhanSu.NS_Email.StartsWith(findNhanSu.NS_Email) : NhanSu != null) &&
                            (findNhanSu.NS_DiaChi != null ? NhanSu.NS_DiaChi.StartsWith(findNhanSu.NS_DiaChi) : NhanSu != null) &&
                            (findNhanSu.NS_SoCCCD != null ? NhanSu.NS_SoCCCD.StartsWith(findNhanSu.NS_SoCCCD) : NhanSu != null) &&
                            (findNhanSu.NS_SoTaiKhoanNganHang != null ? NhanSu.NS_SoTaiKhoanNganHang.StartsWith(findNhanSu.NS_SoTaiKhoanNganHang) : NhanSu != null) &&
                            (findNhanSu.NS_TenChuTaiKhoan != null ? NhanSu.NS_TenChuTaiKhoan.StartsWith(findNhanSu.NS_TenChuTaiKhoan) : NhanSu != null) &&
                            (findNhanSu.NS_HocVan != null ? NhanSu.NS_HocVan.StartsWith(findNhanSu.NS_HocVan) : NhanSu != null) &&
                            findNhanSu.NS_NgayVao == findNhanSu.NS_NgayVao
                            select NhanSu);
                }
                return context.NhanSus;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public NhanSu timKiemMotNhanSu(string maNhanSu)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                return context.NhanSus.Find(maNhanSu);
            } catch { }
            return null;
        }
        public string themNhanSu(NhanSu nhanSu)
        {
            try
            {
                init();
                if (context == null)
                    return errorDB;
                NhanSu nhanSu1 = context.NhanSus.Where(
                    item => item.NS_Ma == nhanSu.NS_Ma || item.NS_SoCCCD == nhanSu.NS_SoCCCD ||
                    item.NS_Email == nhanSu.NS_Email || item.NS_SoDienThoai == nhanSu.NS_SoDienThoai).FirstOrDefault();
                if (nhanSu1 == null)
                {
                    context.NhanSus.Add(nhanSu);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return errorDB;
                    return string.Empty;
                }
                string error = "";
                if (nhanSu1.NS_Ma == nhanSu.NS_Ma)
                    error += "[Mã nhân sự]";
                if (nhanSu1.NS_SoCCCD == nhanSu.NS_SoCCCD)
                    error += "[Số căn cước công dân]";
                if (nhanSu1.NS_Email == nhanSu.NS_Email)
                    error += "[Email]";
                if (nhanSu1.NS_SoDienThoai == nhanSu.NS_SoDienThoai)
                    error += "[Số điện thoại]";
                return error + " đã tồn tại";
            }
            catch
            {
                return errorDB;
            }
        }
        public string suaNhanSu(NhanSu nhanSuNew)
        {
            try
            {
                init();
                if (context == null)
                    return errorDB;
                NhanSu nhanSu = context.NhanSus.Find(nhanSuNew.NS_Ma);
                if (nhanSu == null)
                    return "Nhân sự cần chỉnh sửa không tồn tại trong hệ thống";
                NhanSu nhanSuCheck = context.NhanSus.Where(
                    item => item.NS_Ma != nhanSuNew.NS_Ma && (item.NS_SoCCCD == nhanSuNew.NS_SoCCCD ||
                    item.NS_Email == nhanSuNew.NS_Email || item.NS_SoDienThoai == nhanSuNew.NS_SoDienThoai)).FirstOrDefault();
                if (nhanSuCheck == null)
                {
                    nhanSu.NS_HoVaTen = nhanSuNew.NS_HoVaTen;
                    nhanSu.NS_NgaySinh = nhanSuNew.NS_NgaySinh;
                    nhanSu.NS_GioiTinh = nhanSuNew.NS_GioiTinh;
                    nhanSu.NS_Email = nhanSuNew.NS_Email;
                    nhanSu.NS_SoDienThoai = nhanSuNew.NS_SoDienThoai;
                    nhanSu.NS_SoTaiKhoanNganHang = nhanSuNew.NS_SoTaiKhoanNganHang;
                    nhanSu.NS_TenChuTaiKhoan = nhanSuNew.NS_TenChuTaiKhoan;
                    nhanSu.NS_NgayVao = nhanSuNew.NS_NgayVao;
                    nhanSu.NS_HocVan = nhanSuNew.NS_HocVan;
                    nhanSu.NS_DiaChi = nhanSuNew.NS_DiaChi;
                    nhanSu.NS_SoCCCD = nhanSuNew.NS_SoCCCD;

                    context.NhanSus.AddOrUpdate(nhanSu);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return errorDB;
                    return string.Empty;
                }
                string error = "";
                if (nhanSu.NS_SoCCCD == nhanSuNew.NS_SoCCCD)
                    error += "[Số căn cước công dân]";
                if (nhanSu.NS_Email == nhanSuNew.NS_Email)
                    error += "[Email]";
                if (nhanSu.NS_SoDienThoai == nhanSuNew.NS_SoDienThoai)
                    error += "[Số điện thoại]";
                return error + " đã tồn tại";
            }
            catch
            {
                return errorDB;
            }
        }
        public string xoaNhanSu(string maNhanSu)
        {
            try
            {
                init();
                if (context == null)
                    return errorDB;
                NhanSu nhanSu = context.NhanSus.Find(maNhanSu);
                if (nhanSu == null)
                    return "Nhân sự cần xóa không tồn tại.";
                TaiKhoan taiKhoan = timTaiKhoanBangMaNhanSu(nhanSu.NS_Ma);
                if (taiKhoan != null && (taiKhoan.TK_QuyenQuanLy == true || taiKhoan.TK_QuyenAdmin == true) && !this.quyenAdmin)
                    return "Bạn không thể xóa người có quyền quản lý hệ thống";
                if (taiKhoan != null && taiKhoan.TK_QuyenAdmin == true && this.quyenAdmin)
                    return "Bạn không thể xóa admin của hệ thống";
                context.NhanSus.Remove(nhanSu);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch {}
            return errorDB;
        }
        public string[] xoaNhieuNhanSu(string[]maNhanSus)
        {
            string[] results = new string[] { };
            try
            {
                if (maNhanSus == null)
                    return null;
                else
                {
                    init();
                    string error = "";
                    string success = "";
                    List <NhanSu> nhanSus = new List<NhanSu>();
                    foreach (string s in maNhanSus)
                    {
                        NhanSu nhanSu = timKiemMotNhanSu(s);
                        if (nhanSu != null)
                            nhanSus.Add(nhanSu);
                        else
                            error += ", " + s;
                    }
                    if (error.Length > 0)
                        error = "Không tồn tại nhân sự có mã: " + error + ".";
                    if (nhanSus.Count > 0)
                    {
                        context.NhanSus.RemoveRange(nhanSus);
                        int check = context.SaveChanges();
                        if (check > 0)
                            success = "Xóa thành công " + check + " nhân sự.";
                        error += "\nKhông thể xóa " + (maNhanSus.Length - check) + " nhân sự.";
                    }
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            } catch { }
            results[0] = errorDB;
            return results;
        }
        public TaiKhoan timTaiKhoanBangMaNhanSu(string maNhanSu)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                return context.TaiKhoans.Where(item => item.NS_Ma == maNhanSu).FirstOrDefault();
            }
            catch { }
            return null;
        }
    }
}