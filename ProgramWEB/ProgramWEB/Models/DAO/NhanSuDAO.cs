using PagedList;
using ProgramWEB.Define;
using ProgramWEB.Libary;
using ProgramWEB.Models.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Web.UI.WebControls;

namespace ProgramWEB.Models.DAO
{
    public class NhanSuDAO
    {
        private string errorDB { get; } = "Có lỗi xảy ra, vui lòng thử lại sau";
        private QuanLyNhanSuContext context { set; get; } = null;
        public NhanSuDAO()
        {
            context = new QuanLyNhanSuContext();
        }
        public List<IEnumerable<NhanSu>> getAll(NhanSu findNhanSu, string sortBy = "NS_Ma", int page = 1, int pageSize = 10)
        {
            try
            {
                if (context != null)
                {
                    IEnumerable<NhanSu> results = 
                        (from NhanSu in context.NhanSus
                        where NhanSu.NS_Ma.Contains(findNhanSu.NS_Ma) &&
                                NhanSu.NS_HoVaTen.Contains(findNhanSu.NS_HoVaTen) &&
                                NhanSu.NS_GioiTinh == findNhanSu.NS_GioiTinh &&
                                findNhanSu.NS_NgaySinh != null ? NhanSu.NS_NgaySinh == findNhanSu.NS_NgaySinh : true &&
                                NhanSu.NS_SoDienThoai.Contains(findNhanSu.NS_SoDienThoai) &&
                                NhanSu.NS_Email.Contains(findNhanSu.NS_Email) &&
                                NhanSu.NS_DiaChi.Contains(findNhanSu.NS_DiaChi) &&
                                NhanSu.NS_SoCCCD.Contains(findNhanSu.NS_SoCCCD) &&
                                NhanSu.NS_SoTaiKhoanNganHang.Contains(findNhanSu.NS_SoTaiKhoanNganHang) &&
                                NhanSu.NS_TenChuTaiKhoan.Contains(findNhanSu.NS_TenChuTaiKhoan) &&
                                NhanSu.NS_HocVan.Contains(findNhanSu.NS_HocVan) &&
                                findNhanSu.NS_NgayVao != null ? NhanSu.NS_NgayVao == findNhanSu.NS_NgayVao : true
                         select NhanSu).SortBy(sortBy);
                    return new List<IEnumerable<NhanSu>>()
                    {
                        results,
                        results.Count() > pageSize ? results.ToPagedList(page, pageSize) : results
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public int getCount()
        {
            int count = 0;
            try
            {
                count = context.NhanSus.Count();
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return count;
        }
        public string kiemTraTonTai(NhanSu nhanSu)
        {
            try
            {
                NhanSu nhanSu1 = context.NhanSus.Where(
                    item => item.NS_Ma == nhanSu.NS_Ma || item.NS_SoCCCD == nhanSu.NS_SoCCCD || 
                    item.NS_Email == nhanSu.NS_Email || item.NS_SoDienThoai == nhanSu.NS_SoDienThoai).FirstOrDefault();
                if (nhanSu1 == null)
                    return string.Empty;
                string error = "";
                if (nhanSu1.NS_Ma == nhanSu.NS_Ma)
                    error += "[Mã nhân sự]";
                if (nhanSu1.NS_SoCCCD == nhanSu.NS_SoCCCD)
                    error += "[Số căn cước công dân]";
                if (nhanSu1.NS_Email == nhanSu.NS_Email)
                    error += "[Email]";
                if (nhanSu1.NS_SoDienThoai == nhanSu.NS_SoDienThoai)
                    error += "[Số điện thoại]";
                return error + "đã tồn tại";
            } catch
            {
                return "Có lỗi xảy ra";
            }
        }
        public string edit(NhanSu nhanSuNew)
        {
            try
            {
                NhanSu nhanSu = context.NhanSus.Find(nhanSuNew.NS_Ma);
                if (nhanSu == null)
                    return "Nhân sự cần chỉnh sửa không tồn tại trong hệ thống";
                NhanSu nhanSuCheck = context.NhanSus.Where(
                    item => item.NS_Ma != nhanSuNew.NS_Ma && (item.NS_SoCCCD == nhanSuNew.NS_SoCCCD ||
                    item.NS_Email == nhanSuNew.NS_Email || item.NS_SoDienThoai == nhanSuNew.NS_SoDienThoai)).FirstOrDefault();
                if (nhanSuCheck == null)
                {
                    convert(ref nhanSu, nhanSuNew);
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
        public string add(NhanSu nhanSu)
        {
            try
            {
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
        public bool delete(string NS_Ma)
        {
            try
            {
                NhanSu nhanSu = context.NhanSus.Find(NS_Ma);
                if (nhanSu == null)
                    return true;
                context.NhanSus.Remove(nhanSu);
                int check = context.SaveChanges();
                return check == 0 ? false : true;
            } catch
            {
                return false;
            }
        }
        public void convert(ref NhanSu nhanSuOld, NhanSu nhanSuNew)
        {
            if (nhanSuOld == null || nhanSuNew == null)
                return;
            nhanSuOld.NS_HoVaTen = nhanSuNew.NS_HoVaTen;
            nhanSuOld.NS_NgaySinh = nhanSuNew .NS_NgaySinh;
            nhanSuOld.NS_GioiTinh = nhanSuNew.NS_GioiTinh;
            nhanSuOld.NS_Email = nhanSuNew.NS_Email;
            nhanSuOld.NS_SoDienThoai = nhanSuNew.NS_SoDienThoai;
            nhanSuOld.NS_SoTaiKhoanNganHang = nhanSuNew.NS_SoTaiKhoanNganHang;
            nhanSuOld.NS_TenChuTaiKhoan = nhanSuNew.NS_TenChuTaiKhoan;
            nhanSuOld.NS_NgayVao = nhanSuNew.NS_NgayVao;
            nhanSuOld.NS_HocVan = nhanSuNew.NS_HocVan;
            nhanSuOld.NS_DiaChi = nhanSuNew.NS_DiaChi;
            nhanSuOld.NS_SoCCCD = nhanSuNew.NS_SoCCCD;
        }
    }
}