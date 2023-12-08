using ProgramWEB.Define;
using ProgramWEB.Libary;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace ProgramWEB.Models.Object
{
    public class Admin : QuanLy
    {
        public Admin() : base()
        {

        }
        public Admin(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar) : base(username, maNhanSu, quyenAdmin, quyenQuanLy, avatar)
        {
        }
//Phong ban
        public string themPhongBan(Object.PhongBan result)
        {
            try
            {
                if (result == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(result.PB_Ma) || string.IsNullOrEmpty(result.PB_Ten)) 
                    return DefineError.loiDuLieuKhongHopLe;
                Data.PhongBan old = context.PhongBans.Where(item => item.PB_Ma == result.PB_Ma).FirstOrDefault();)
                if (old == null)
                {
                    if (!string.IsNullOrEmpty(result.NS_Ma)) {
                        if (context.NhanSus.Find(result.NS_Ma) == null)
                            return "Nhân sự không tồn tại.";
                        old = context.PhongBans.Where(item => item.NS_Ma == result.NS_Ma).FirstOrDefault();
                        if (old != null)
                            return "Một nhân sự không thể cai quản nhiều hơn một phòng ban.";
                    }
                    old = new Data.PhongBan();
                    Convert<Data.PhongBan, Object.PhongBan>.ConvertObj(ref old, result);
                    context.PhongBans.Add(old);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                return "Mã phòng ban đã tồn tại.";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string suaPhongBan(Object.PhongBan New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(New.PB_Ma) || string.IsNullOrEmpty(New.PB_Ten))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.PhongBan old = context.PhongBans.Find(New.PB_Ma);
                if (old == null)
                    return DefineError.khongTonTai;
                Data.PhongBan check = context.PhongBans.Where(
                    item => (item.PB_Ma != New.PB_Ma && (item.NS_Ma == New.NS_Ma))).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.PhongBan, Object.PhongBan>.ConvertObj(ref old, New);
                    int checkInt = context.SaveChanges();
                    if (checkInt == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                return "Một nhân sự không thể cai quản nhiều hơn một phòng ban.";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaPhongBan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.PhongBan del = context.PhongBans.Find(ma);
                if (del == null)
                    return DefineError.khongTonTai;
                context.PhongBans.Remove(del);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuPhongBan(string[] mas)
        {
            string[] results = new string[] { string.Empty, string.Empty };
            try
            {
                if (mas == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (string s in mas)
                    {
                        string message = xoaPhongBan(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " phòng ban.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " phòng ban.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Bo phan
        public string themBoPhan(Object.BoPhan result)
        {
            try
            {
                if (result == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(result.PB_Ma) || string.IsNullOrEmpty(result.BP_Ma) || string.IsNullOrEmpty(result.BP_Ten))
                    return DefineError.loiDuLieuKhongHopLe;
                if (context.PhongBans.Find(result.PB_Ma) == null)
                    return "Mã phòng ban không tồn tại.";
                Data.BoPhan old = context.BoPhans.Where(item => item.BP_Ma == result.BP_Ma).FirstOrDefault();
                if (old == null)
                {
                    if (!string.IsNullOrEmpty(result.NS_Ma))
                    {
                        old = context.BoPhans.Where(item => item.NS_Ma == result.NS_Ma).FirstOrDefault();
                        if (old != null)
                            return "Một nhân sự không thể cai quản nhiều hơn một bộ phận.";
                    }
                    old = new Data.BoPhan();
                    Convert<Data.BoPhan, Object.BoPhan>.ConvertObj(ref old, result);
                    context.BoPhans.Add(old);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                return "Mã bộ phận đã tồn tại.";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string suaBoPhan(Object.BoPhan New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                if (string.IsNullOrEmpty(New.PB_Ma) || string.IsNullOrEmpty(New.BP_Ma) || string.IsNullOrEmpty(New.BP_Ten))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.BoPhan old = context.BoPhans.Find(New.BP_Ma);
                if (old == null)
                    return DefineError.khongTonTai;
                if (context.PhongBans.Find(New.PB_Ma) == null)
                    return "Mã phòng ban không tồn tại.";
                Data.BoPhan check = context.BoPhans.Where(
                    item => (item.BP_Ma != New.BP_Ma && (item.NS_Ma == New.NS_Ma))).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.BoPhan, Object.BoPhan>.ConvertObj(ref old, New);
                    int checkInt = context.SaveChanges();
                    if (checkInt == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                return "Một nhân sự không thể cai quản nhiều hơn một bộ phận.";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaBoPhan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.BoPhan del = context.BoPhans.Find(ma);
                if (del == null)
                    return DefineError.khongTonTai;
                context.BoPhans.Remove(del);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuBoPhan(string[] mas)
        {
            string[] results = new string[] { string.Empty, string.Empty };
            try
            {
                if (mas == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (string s in mas)
                    {
                        string message = xoaBoPhan(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " bộ phận.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " bộ phận.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
    }
}