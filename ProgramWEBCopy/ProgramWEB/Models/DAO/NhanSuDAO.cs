﻿using PagedList;
using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
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
        public IEnumerable<NhanSu> getAll(object[] findBy = null, string[] sortBy = null, int page = 1, int pageSize = 10)
        {
            try
            {
                if (context != null)
                {
                    //return findBy != null ?
                    //    context.NhanSus.Where(item =>
                    //    findBy[0] == null ? true : item.NS_Ma.Trim() == (string)findBy[0] &&
                    //    findBy[1] == null ? true : item.NS_HoVaTen.Trim() == (string)findBy[1] &&
                    //    findBy[2] == null ? true : item.NS_GioiTinh == (bool)findBy[2] &&
                    //    findBy[3] == null ? true :
                    //            DateHelper.soSanhNgay(item.NS_NgaySinh.GetValueOrDefault(), (DateTime)findBy[3]) &&
                    //    findBy[4] == null ? true : item.NS_SoDienThoai.Trim() == (string)findBy[4] &&
                    //    findBy[5] == null ? true : item.NS_Email.Trim() == (string)findBy[5] &&
                    //    findBy[6] == null ? true : item.NS_DiaChi.Trim() == (string)findBy[6] &&
                    //    findBy[7] == null ? true : item.NS_SoCCCD.Trim() == (string)findBy[7] &&
                    //    findBy[8] == null ? true : item.NS_SoTaiKhoanNganHang.Trim() == (string)findBy[8] &&
                    //    findBy[9] == null ? true : item.NS_TenChuTaiKhoan.Trim() == (string)findBy[9] &&
                    //    findBy[10] == null ? true : item.NS_HocVan.Trim() == (string)findBy[10] &&
                    //    findBy[11] == null ? true : DateHelper.soSanhNgay(item.NS_NgayVao, (DateTime)findBy[11])
                    //    )
                    //    :
                    //    context.NhanSus.Select(item => item);
                    return (from NhanSu in context.NhanSus orderby NhanSu.NS_Ma select NhanSu).ToPagedList(page, pageSize);
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