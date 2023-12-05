using Newtonsoft.Json;
using PagedList;
using ProgramWEB.Define;
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
//Nhan su
        public IEnumerable<Object.NhanSu> layDanhSachNhanSu()
        {
            try
            {
                init();
                if (context != null) 
                    return Convert<Object.NhanSu, Data.NhanSu>.ConvertObjs(context.NhanSus);      
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.NhanSu>> layDanhSachNhanSu(Object.NhanSu findBy = null, int page = 1, int pageSize = 10, string sortBy = "NS_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.NhanSu> results =
                    (findBy != null ? timKiemNhieuNhanSu(findBy)
                    : Convert<Object.NhanSu, Data.NhanSu>.ConvertObjs(from NhanSu in context.NhanSus select NhanSu));
                results = ObjectHelper.OrderByDynamic<Object.NhanSu>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.NhanSu>>()
                    {
                        new List<Object.NhanSu>(),
                        new List<Object.NhanSu>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.NhanSu>>()
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
        public IEnumerable<Object.NhanSu> timKiemNhieuNhanSu(Object.NhanSu findNhanSu = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (findNhanSu != null)
                {
                    return Convert<Object.NhanSu, Data.NhanSu>.ConvertObjs(from NhanSu in context.NhanSus
                            where
                            (!string.IsNullOrEmpty(findNhanSu.NS_Ma) ? NhanSu.NS_Ma.StartsWith(findNhanSu.NS_Ma) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_HoVaTen) ? NhanSu.NS_HoVaTen.StartsWith(findNhanSu.NS_HoVaTen) : NhanSu != null) &&
                            (findNhanSu.NS_GioiTinh != null ? NhanSu.NS_GioiTinh == findNhanSu.NS_GioiTinh : NhanSu != null) &&
                            (findNhanSu.NS_NgaySinh != null ? NhanSu.NS_NgaySinh == findNhanSu.NS_NgaySinh : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_SoDienThoai) ? NhanSu.NS_SoDienThoai.StartsWith(findNhanSu.NS_SoDienThoai) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_Email) ? NhanSu.NS_Email.StartsWith(findNhanSu.NS_Email) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_DiaChi) ? NhanSu.NS_DiaChi.StartsWith(findNhanSu.NS_DiaChi) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_SoCCCD) ? NhanSu.NS_SoCCCD.StartsWith(findNhanSu.NS_SoCCCD) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_SoTaiKhoanNganHang) ? NhanSu.NS_SoTaiKhoanNganHang.StartsWith(findNhanSu.NS_SoTaiKhoanNganHang) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_TenChuTaiKhoan) ? NhanSu.NS_TenChuTaiKhoan.StartsWith(findNhanSu.NS_TenChuTaiKhoan) : NhanSu != null) &&
                            (!string.IsNullOrEmpty(findNhanSu.NS_HocVan) ? NhanSu.NS_HocVan.StartsWith(findNhanSu.NS_HocVan) : NhanSu != null) &&
                            (findNhanSu.NS_NgayVao != null ? NhanSu.NS_NgayVao == findNhanSu.NS_NgayVao : NhanSu != null)
                            select NhanSu);
                }
                return Convert<Object.NhanSu,Data.NhanSu>.ConvertObjs(context.NhanSus);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.NhanSu timKiemMotNhanSu(string maNhanSu)
        {
            try
            {
                Object.NhanSu convert = new NhanSu();
                if (string.IsNullOrEmpty(maNhanSu))
                    return null;
                init();
                if (context == null)
                    return null;
                Convert<Object.NhanSu, Data.NhanSu>.ConvertObj(ref convert, context.NhanSus.Find(maNhanSu));
                return convert;
            } catch { }
            return null;
        }
        public string themNhanSu(Object.NhanSu nhanSu)
        {
            try
            {
                if (nhanSu == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.NhanSu nhanSu1 = context.NhanSus.Where(
                    item => item.NS_Ma == nhanSu.NS_Ma || item.NS_SoCCCD == nhanSu.NS_SoCCCD ||
                    item.NS_Email == nhanSu.NS_Email || item.NS_SoDienThoai == nhanSu.NS_SoDienThoai).FirstOrDefault();
                if (nhanSu1 == null)
                {
                    nhanSu1 = new Data.NhanSu();
                    Convert<Data.NhanSu, Object.NhanSu>.ConvertObj(ref nhanSu1, nhanSu);
                    context.NhanSus.Add(nhanSu1);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
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
                return DefineError.loiHeThong;
            }
        }
        public string suaNhanSu(Object.NhanSu nhanSuNew)
        {
            try
            {
                if (nhanSuNew == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.NhanSu nhanSu = context.NhanSus.Find(nhanSuNew.NS_Ma);
                if (nhanSu == null)
                    return DefineError.khongTonTai;
                Data.NhanSu nhanSuCheck = context.NhanSus.Where(
                    item => item.NS_Ma != nhanSuNew.NS_Ma && (item.NS_SoCCCD == nhanSuNew.NS_SoCCCD ||
                    item.NS_Email == nhanSuNew.NS_Email || item.NS_SoDienThoai == nhanSuNew.NS_SoDienThoai)).FirstOrDefault();
                if (nhanSuCheck == null)
                {
                    Convert<Data.NhanSu, Object.NhanSu>.ConvertObj(ref nhanSu, nhanSuNew);
                    context.NhanSus.AddOrUpdate(nhanSu);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
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
                return DefineError.loiHeThong;
            }
        }
        public string xoaNhanSu(string maNhanSu)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhanSu))
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.NhanSu nhanSu = context.NhanSus.Find(maNhanSu);
                if (nhanSu == null)
                    return DefineError.khongTonTai;
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
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuNhanSu(string[]maNhanSus)
        {
            string[] results = new string[] {string.Empty, string.Empty};
            try
            {
                if (maNhanSus == null)
                    return null;
                else
                {
                    string error = "";
                    string success = "";
                    int countSuccess = 0;
                    foreach (string s in maNhanSus)
                    {
                        string message = xoaNhanSu(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " nhân sự.";
                    if (countSuccess < maNhanSus.Length)
                        error = "Không thể xóa " + (maNhanSus.Length - countSuccess) + " nhân sự.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            } catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Bao Hiem
        public IEnumerable<Object.BaoHiem> layDanhSachBaoHiem()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(context.BaoHiems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.BaoHiem>> layDanhSachBaoHiem(Object.BaoHiem findBy = null, int page = 1, int pageSize = 10, string sortBy = "BH_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.BaoHiem> results =
                    (findBy != null ? timKiemNhieuBaoHiem(findBy)
                    : Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(from BaoHiem in context.BaoHiems select BaoHiem));
                results = ObjectHelper.OrderByDynamic<Object.BaoHiem>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.BaoHiem>>()
                    {
                        new List<Object.BaoHiem>(),
                        new List<Object.BaoHiem>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<BaoHiem>>()
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
        public IEnumerable<Object.BaoHiem> timKiemNhieuBaoHiem(Object.BaoHiem find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(from BaoHiem in context.BaoHiems
                            where
                            (!string.IsNullOrEmpty(find.BH_Ma) ? BaoHiem.BH_Ma.StartsWith(find.BH_Ma) : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.BH_SoBaoHiem) ? BaoHiem.BH_SoBaoHiem.StartsWith(find.BH_SoBaoHiem) : BaoHiem != null) &&
                            (find.BH_NgayCap != null ? BaoHiem.BH_NgayCap == find.BH_NgayCap : BaoHiem != null) &&
                            (find.BH_NgayHetHan != null ? BaoHiem.BH_NgayHetHan == find.BH_NgayHetHan : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.BH_NoiCap) ? BaoHiem.BH_NoiCap.StartsWith(find.BH_NoiCap) : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.BH_NoiKhamBenh) ? BaoHiem.BH_NoiKhamBenh.StartsWith(find.BH_NoiKhamBenh) : BaoHiem != null) &&
                            (!string.IsNullOrEmpty(find.NS_Ma) ? BaoHiem.NS_Ma.StartsWith(find.NS_Ma) : BaoHiem != null)
                            select BaoHiem);
                }
                return Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObjs(context.BaoHiems);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.BaoHiem timKiemMotBaoHiem(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.BaoHiem convert = new BaoHiem();
                Convert<Object.BaoHiem, Data.BaoHiem>.ConvertObj(ref convert, context.BaoHiems.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themBaoHiem(Object.BaoHiem BaoHiem)
        {
            try
            {
                if (BaoHiem == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.BaoHiem BaoHiem1 = context.BaoHiems.Where(
                    item => item.BH_Ma == BaoHiem.BH_Ma || 
                    item.BH_SoBaoHiem == BaoHiem.BH_SoBaoHiem).FirstOrDefault();
                if (BaoHiem1 == null)
                {
                    BaoHiem1 = new Data.BaoHiem();
                    Convert<Data.BaoHiem, Object.BaoHiem>.ConvertObj(ref BaoHiem1, BaoHiem);
                    context.BaoHiems.Add(BaoHiem1);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (BaoHiem1.BH_Ma == BaoHiem.BH_Ma)
                    error += "[Mã bảo hiểm]";
                if (BaoHiem1.BH_SoBaoHiem == BaoHiem.BH_SoBaoHiem)
                    error += "[Số bảo hiểm]";
                return error + " đã tồn tại";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string suaBaoHiem(Object.BaoHiem New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.BaoHiem Old = context.BaoHiems.Find(New.BH_Ma);
                if (Old == null)
                    return DefineError.khongTonTai;
                Data.BaoHiem check = context.BaoHiems.Where(
                    item => item.BH_Ma != New.BH_Ma 
                    && (item.BH_SoBaoHiem == New.BH_SoBaoHiem)).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.BaoHiem, Object.BaoHiem>.ConvertObj(ref Old, New);
                    context.BaoHiems.AddOrUpdate(check);
                    int checkSuccess = context.SaveChanges();
                    if (checkSuccess == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (check.BH_Ma == check.BH_Ma)
                    error += "[Mã bảo hiểm]";
                if (check.BH_SoBaoHiem == check.BH_SoBaoHiem)
                    error += "[Số bảo hiểm]";
                return error + " đã tồn tại";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaBaoHiem(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.BaoHiem xoa = context.BaoHiems.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.BaoHiems.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuBaoHiem(string[] mas)
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
                        string message = xoaBaoHiem(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " bảo hiểm.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " bảo hiểm.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Hop Dong
        public IEnumerable<Object.HopDong> layDanhSachHopDong()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.HopDong, Data.HopDong>.ConvertObjs(context.HopDongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.HopDong>> layDanhSachHopDong(Object.HopDong findBy = null, int page = 1, int pageSize = 10, string sortBy = "HD_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.HopDong> results =
                    (findBy != null ? timKiemNhieuHopDong(findBy)
                    : Convert<Object.HopDong, Data.HopDong>.ConvertObjs(from HopDong in context.HopDongs select HopDong));
                results = ObjectHelper.OrderByDynamic<Object.HopDong>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.HopDong>>()
                    {
                        new List<Object.HopDong>(),
                        new List<Object.HopDong>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.HopDong>>()
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
        public IEnumerable<Object.HopDong> timKiemNhieuHopDong(Object.HopDong find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.HopDong, Data.HopDong>.ConvertObjs(from HopDong in context.HopDongs
                        where
                        (!string.IsNullOrEmpty(find.HD_Ma) ? HopDong.HD_Ma.StartsWith(find.HD_Ma) : HopDong != null) &&
                        (find.HD_NgayBatDau != null ? HopDong.HD_NgayBatDau == find.HD_NgayBatDau : HopDong != null) &&
                        (find.HD_NgayKetThuc != null ? HopDong.HD_NgayKetThuc == find.HD_NgayKetThuc : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.HD_HinhThucLamViec) ? HopDong.HD_HinhThucLamViec.StartsWith(find.HD_HinhThucLamViec) : HopDong != null) &&
                        (find.HD_Luong != null ? HopDong.HD_Luong == find.HD_Luong : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.HD_DonViTinhuong) ? HopDong.HD_DonViTinhuong.StartsWith(find.HD_DonViTinhuong) : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.HD_CongViec) ? HopDong.HD_CongViec.StartsWith(find.HD_CongViec) : HopDong != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? HopDong.NS_Ma.StartsWith(find.NS_Ma) : HopDong != null) 
                        select HopDong);
                }
                return Convert<Object.HopDong, Data.HopDong>.ConvertObjs(context.HopDongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.HopDong timKiemMotHopDong(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.HopDong convert = new HopDong();
                Convert<Object.HopDong, Data.HopDong>.ConvertObj(ref convert, context.HopDongs.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themHopDong(Object.HopDong New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.HopDong vali = context.HopDongs.Where(
                    item => item.HD_Ma == New.HD_Ma ||
                    (
                        item.NS_Ma == New.NS_Ma &&
                        (
                            item.HD_NgayKetThuc == null ||
                            item.HD_NgayKetThuc > New.HD_NgayBatDau
                        )    
                    )
                ).FirstOrDefault();
                if (vali == null)
                {
                    vali = new Data.HopDong();
                    Convert<Data.HopDong, Object.HopDong>.ConvertObj(ref vali, New);
                    context.HopDongs.Add(vali);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (vali.HD_Ma == New.HD_Ma)
                    error += "[Mã hợp đồng]";
                else
                    error += "[Hợp đồng cũ của người này chưa kết thúc, không thể thêm mới]";
                return error;
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string suaHopDong(Object.HopDong New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.HopDong Old = context.HopDongs.Find(New.HD_Ma);
                if (Old == null)
                    return DefineError.khongTonTai;
                Data.HopDong check = context.HopDongs.Where(
                    item => item.HD_Ma != New.HD_Ma
                ).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.HopDong, Object.HopDong>.ConvertObj(ref Old, New);
                    context.HopDongs.AddOrUpdate(check);
                    int checkSuccess = context.SaveChanges();
                    if (checkSuccess == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (check.HD_Ma == check.HD_Ma)
                    error += "[Mã hợp đồng]";
                return error + " đã tồn tại";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaHopDong(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.HopDong xoa = context.HopDongs.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.HopDongs.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuHopDong(string[] mas)
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
                        string message = xoaHopDong(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " hợp đồng.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " hợp đồng.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Lich su lam viec
        public IEnumerable<Object.LichSuLamViec> layDanhSachLichSuLamViec()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(context.LichSuLamViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.LichSuLamViec>> layDanhSachLichSuLamViec(Object.LichSuLamViec findBy = null, int page = 1, int pageSize = 10, string sortBy = "LSLV_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.LichSuLamViec> results =
                    (findBy != null ? timKiemNhieuLichSuLamViec(findBy)
                    : Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(from LichSuLamViec in context.LichSuLamViecs select LichSuLamViec));
                results = ObjectHelper.OrderByDynamic<Object.LichSuLamViec>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.LichSuLamViec>>()
                    {
                        new List<Object.LichSuLamViec>(),
                        new List<Object.LichSuLamViec>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.LichSuLamViec>>()
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
        public IEnumerable<Object.LichSuLamViec> timKiemNhieuLichSuLamViec(Object.LichSuLamViec find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(from LichSuLamViec in context.LichSuLamViecs
                        where
                        (!string.IsNullOrEmpty(find.LSLV_Ma) ? LichSuLamViec.LSLV_Ma.StartsWith(find.LSLV_Ma) : LichSuLamViec != null) &&
                        (find.LSLV_NgayBatDau != null ? LichSuLamViec.LSLV_NgayBatDau == find.LSLV_NgayBatDau : LichSuLamViec != null) &&
                        (find.LSLV_NgayKetThuc != null ? LichSuLamViec.LSLV_NgayKetThuc == find.LSLV_NgayKetThuc : LichSuLamViec != null) &&
                        (!string.IsNullOrEmpty(find.LSLV_ChucVu) ? LichSuLamViec.LSLV_ChucVu.StartsWith(find.LSLV_ChucVu) : LichSuLamViec != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? LichSuLamViec.NS_Ma.StartsWith(find.NS_Ma) : LichSuLamViec != null) &&
                        (!string.IsNullOrEmpty(find.BP_Ma) ? LichSuLamViec.BP_Ma.StartsWith(find.BP_Ma) : LichSuLamViec != null) 
                        select LichSuLamViec);
                }
                return Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObjs(context.LichSuLamViecs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.LichSuLamViec timKiemMotLichSuLamViec(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.LichSuLamViec convert = new LichSuLamViec();
                Convert<Object.LichSuLamViec, Data.LichSuLamViec>.ConvertObj(ref convert, context.LichSuLamViecs.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themLichSuLamViec(Object.LichSuLamViec New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.LichSuLamViec vali = context.LichSuLamViecs
                    .Where(item => item.LSLV_Ma == New.LSLV_Ma).FirstOrDefault();
                if (vali == null)
                {
                    vali = new Data.LichSuLamViec();
                    Convert<Data.LichSuLamViec, Object.LichSuLamViec>.ConvertObj(ref vali, New);
                    context.LichSuLamViecs.Add(vali);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (vali.LSLV_Ma == New.LSLV_Ma)
                    error += "[Mã] đã tồn tại";
                return error;
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string suaLichSuLamViec(Object.LichSuLamViec New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.LichSuLamViec Old = context.LichSuLamViecs.Find(New.LSLV_Ma);
                if (Old == null)
                    return DefineError.khongTonTai;
                Data.LichSuLamViec check = context.LichSuLamViecs.Where(
                    item => item.LSLV_Ma != New.LSLV_Ma
                ).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.LichSuLamViec, Object.LichSuLamViec>.ConvertObj(ref Old, New);
                    context.LichSuLamViecs.AddOrUpdate(check);
                    int checkSuccess = context.SaveChanges();
                    if (checkSuccess == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (check.LSLV_Ma == check.LSLV_Ma)
                    error += "[Mã]";
                return error + " đã tồn tại";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaLichSuLamViec(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.LichSuLamViec xoa = context.LichSuLamViecs.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.LichSuLamViecs.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuLichSuLamViec(string[] mas)
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
                        string message = xoaLichSuLamViec(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " lịch sử làm việc.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " lịch sử làm việc.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Tai khoan
        public Object.TaiKhoan timTaiKhoanBangMaNhanSu(string maNhanSu)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhanSu))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.TaiKhoan convert = new Object.TaiKhoan();
                Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObj(ref convert, context.TaiKhoans.Where(item => item.NS_Ma == maNhanSu).FirstOrDefault());
                return convert;
            }
            catch { }
            return null;
        }
        public IEnumerable<Object.TaiKhoan> layDanhSachTaiKhoan()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(context.TaiKhoans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.TaiKhoan>> layDanhSachTaiKhoan(Object.TaiKhoan findBy = null, int page = 1, int pageSize = 10, string sortBy = "TK_TenDangNhap", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<TaiKhoan> results =
                    (findBy != null ? timKiemNhieuTaiKhoan(findBy)
                    : Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(from TaiKhoan in context.TaiKhoans select TaiKhoan));
                results = ObjectHelper.OrderByDynamic<TaiKhoan>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<TaiKhoan>>()
                    {
                        new List<TaiKhoan>(),
                        new List<TaiKhoan>() 
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<TaiKhoan>>()
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
        public IEnumerable<Object.TaiKhoan> timKiemNhieuTaiKhoan(Object.TaiKhoan findTaiKhoan = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (findTaiKhoan != null)
                {
                    return Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(from TaiKhoan in context.TaiKhoans
                            where
                            (!string.IsNullOrEmpty(findTaiKhoan.TK_TenDangNhap) ? TaiKhoan.TK_TenDangNhap.StartsWith(findTaiKhoan.TK_TenDangNhap) : TaiKhoan != null) &&
                            (findTaiKhoan.TK_ThoiGianMoKhoa != null ? TaiKhoan.TK_ThoiGianMoKhoa == findTaiKhoan.TK_ThoiGianMoKhoa : TaiKhoan != null) &&
                            (findTaiKhoan.TK_BiKhoa != null ? TaiKhoan.TK_BiKhoa == findTaiKhoan.TK_BiKhoa : TaiKhoan != null) &&
                            (findTaiKhoan.TK_QuyenAdmin != null ? TaiKhoan.TK_QuyenAdmin == findTaiKhoan.TK_QuyenAdmin : TaiKhoan != null) &&
                            (findTaiKhoan.TK_QuyenQuanLy != null ? TaiKhoan.TK_QuyenQuanLy == findTaiKhoan.TK_QuyenQuanLy : TaiKhoan != null) &&
                            (!string.IsNullOrEmpty(findTaiKhoan.NS_Ma) ? TaiKhoan.NS_Ma.StartsWith(findTaiKhoan.NS_Ma) : TaiKhoan != null) 
                            select TaiKhoan);
                }
                return Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObjs(context.TaiKhoans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.TaiKhoan timKiemMotTaiKhoan(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(maNhanSu))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.TaiKhoan convert = new TaiKhoan();
                Convert<Object.TaiKhoan, Data.TaiKhoan>.ConvertObj(ref convert, context.TaiKhoans.Find(username));
                return convert;
            }
            catch { }
            return null;
        }
//Khen thuong ky luat
        public IEnumerable<Object.KhenThuongKyLuat> layDanhSachKhenThuongKyLuat()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(context.KhenThuongKyLuats);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.KhenThuongKyLuat>> layDanhSachKhenThuongKyLuat(Object.KhenThuongKyLuat findBy = null, int page = 1, int pageSize = 10, string sortBy = "KTKL_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.KhenThuongKyLuat> results =
                    (findBy != null ? timKiemNhieuKhenThuongKyLuat(findBy)
                    : Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(from KhenThuongKyLuat in context.KhenThuongKyLuats select KhenThuongKyLuat));
                results = ObjectHelper.OrderByDynamic<Object.KhenThuongKyLuat>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.KhenThuongKyLuat>>()
                    {
                        new List<Object.KhenThuongKyLuat>(),
                        new List<Object.KhenThuongKyLuat>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.KhenThuongKyLuat>>()
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
        public IEnumerable<Object.KhenThuongKyLuat> timKiemNhieuKhenThuongKyLuat(Object.KhenThuongKyLuat find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(from KhenThuongKyLuat in context.KhenThuongKyLuats
                        where
                        (!string.IsNullOrEmpty(find.KTKL_Ma) ? KhenThuongKyLuat.KTKL_Ma.StartsWith(find.KTKL_Ma) : KhenThuongKyLuat != null) &&
                        (!string.IsNullOrEmpty(find.KTKL_MoTa) ? KhenThuongKyLuat.KTKL_MoTa.StartsWith(find.KTKL_MoTa) : KhenThuongKyLuat != null) &&
                        (find.KTKL_ThoiGian != null ? KhenThuongKyLuat.KTKL_ThoiGian == find.KTKL_ThoiGian : KhenThuongKyLuat != null) &&
                        (!string.IsNullOrEmpty(find.KTKL_HinhThuc) ? KhenThuongKyLuat.KTKL_HinhThuc.StartsWith(find.KTKL_HinhThuc) : KhenThuongKyLuat != null) &&
                        (find.KTKL_SoTien != null ? KhenThuongKyLuat.KTKL_SoTien == find.KTKL_SoTien : KhenThuongKyLuat != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? KhenThuongKyLuat.NS_Ma.StartsWith(find.NS_Ma) : KhenThuongKyLuat != null) 
                        select KhenThuongKyLuat);
                }
                return Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObjs(context.KhenThuongKyLuats);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.KhenThuongKyLuat timKiemMotKhenThuongKyLuat(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.KhenThuongKyLuat convert = new KhenThuongKyLuat();
                Convert<Object.KhenThuongKyLuat, Data.KhenThuongKyLuat>.ConvertObj(ref convert, context.KhenThuongKyLuats.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string themKhenThuongKyLuat(Object.KhenThuongKyLuat New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.KhenThuongKyLuat vali = context.KhenThuongKyLuats
                    .Where(item => item.KTKL_Ma == New.KTKL_Ma).FirstOrDefault();
                if (vali == null)
                {
                    vali = new Data.KhenThuongKyLuat();
                    Convert<Data.KhenThuongKyLuat, Object.KhenThuongKyLuat>.ConvertObj(ref vali, New);
                    context.KhenThuongKyLuats.Add(vali);
                    int check = context.SaveChanges();
                    if (check == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (vali.KTKL_Ma == New.KTKL_Ma)
                    error += "[Mã] đã tồn tại";
                return error;
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string suaKhenThuongKyLuat(Object.KhenThuongKyLuat New)
        {
            try
            {
                if (New == null)
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.KhenThuongKyLuat Old = context.KhenThuongKyLuats.Find(New.KTKL_Ma);
                if (Old == null)
                    return DefineError.khongTonTai;
                Data.KhenThuongKyLuat check = context.KhenThuongKyLuats.Where(
                    item => item.KTKL_Ma != New.KTKL_Ma
                ).FirstOrDefault();
                if (check == null)
                {
                    Convert<Data.KhenThuongKyLuat, Object.KhenThuongKyLuat>.ConvertObj(ref Old, New);
                    context.KhenThuongKyLuats.AddOrUpdate(check);
                    int checkSuccess = context.SaveChanges();
                    if (checkSuccess == 0)
                        return DefineError.loiHeThong;
                    return string.Empty;
                }
                string error = "";
                if (check.KTKL_Ma == check.KTKL_Ma)
                    error += "[Mã]";
                return error + " đã tồn tại";
            }
            catch
            {
                return DefineError.loiHeThong;
            }
        }
        public string xoaKhenThuongKyLuat(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return DefineError.loiDuLieuKhongHopLe;
                init();
                if (context == null)
                    return DefineError.loiHeThong;
                Data.KhenThuongKyLuat xoa = context.KhenThuongKyLuats.Find(ma);
                if (xoa == null)
                    return DefineError.khongTonTai;
                context.KhenThuongKyLuats.Remove(xoa);
                int check = context.SaveChanges();
                if (check > 0)
                    return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
        public string[] xoaNhieuKhenThuongKyLuat(string[] mas)
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
                        string message = xoaKhenThuongKyLuat(s);
                        if (string.IsNullOrEmpty(message))
                            countSuccess++;
                    }
                    if (countSuccess > 0)
                        success = "Xoá thành công " + countSuccess + " khen thưởng kỷ luật.";
                    if (countSuccess < mas.Length)
                        error = "Không thể xóa " + (mas.Length - countSuccess) + " khen thưởng kỷ luật.";
                    results[0] = error;
                    results[1] = success;
                    return results;
                }
            }
            catch { }
            results[0] = DefineError.loiHeThong;
            return results;
        }
//Phong ban
        public IEnumerable<Object.PhongBan> layDanhSachPhongBan()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(context.PhongBans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.PhongBan>> layDanhSachPhongBan(Object.PhongBan findBy = null, int page = 1, int pageSize = 10, string sortBy = "PB_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.PhongBan> results =
                    (findBy != null ? timKiemNhieuPhongBan(findBy)
                    : Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(from PhongBan in context.PhongBans select PhongBan));
                results = ObjectHelper.OrderByDynamic<Object.PhongBan>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.PhongBan>>()
                    {
                        new List<Object.PhongBan>(),
                        new List<Object.PhongBan>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.PhongBan>>()
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
        public IEnumerable<Object.PhongBan> timKiemNhieuPhongBan(Object.PhongBan find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(from PhongBan in context.PhongBans
                        where
                        (!string.IsNullOrEmpty(find.PB_Ma) ? PhongBan.PB_Ma.StartsWith(find.PB_Ma) : PhongBan != null) &&
                        (!string.IsNullOrEmpty(find.PB_Ten) ? PhongBan.PB_Ten.StartsWith(find.PB_Ten) : PhongBan != null) &&
                        (!string.IsNullOrEmpty(find.PB_VaiTro) ? PhongBan.PB_VaiTro.StartsWith(find.PB_VaiTro) : PhongBan != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? PhongBan.NS_Ma.StartsWith(find.NS_Ma) : PhongBan != null)
                        select PhongBan);
                }
                return Convert<Object.PhongBan, Data.PhongBan>.ConvertObjs(context.PhongBans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.PhongBan timKiemMotPhongBan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.PhongBan convert = new PhongBan();
                Convert<Object.PhongBan, Data.PhongBan>.ConvertObj(ref convert, context.PhongBans.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Bo Phan
        public IEnumerable<Object.BoPhan> layDanhSachBoPhan()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(context.BoPhans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.BoPhan>> layDanhSachBoPhan(Object.BoPhan findBy = null, int page = 1, int pageSize = 10, string sortBy = "BP_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.BoPhan> results =
                    (findBy != null ? timKiemNhieuBoPhan(findBy)
                    : Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(from BoPhan in context.BoPhans select BoPhan));
                results = ObjectHelper.OrderByDynamic<Object.BoPhan>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.BoPhan>>()
                    {
                        new List<Object.BoPhan>(),
                        new List<Object.BoPhan>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.BoPhan>>()
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
        public IEnumerable<Object.BoPhan> timKiemNhieuBoPhan(Object.BoPhan find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(from BoPhan in context.BoPhans
                        where
                        (!string.IsNullOrEmpty(find.BP_Ma) ? BoPhan.BP_Ma.StartsWith(find.BP_Ma) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.BP_Ten) ? BoPhan.BP_Ten.StartsWith(find.BP_Ten) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.BP_ChuyenMon) ? BoPhan.BP_ChuyenMon.StartsWith(find.BP_ChuyenMon) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.PB_Ma) ? BoPhan.PB_Ma.StartsWith(find.PB_Ma) : BoPhan != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? BoPhan.NS_Ma.StartsWith(find.NS_Ma) : BoPhan != null)
                        select BoPhan);
                }
                return Convert<Object.BoPhan, Data.BoPhan>.ConvertObjs(context.BoPhans);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.BoPhan timKiemMotBoPhan(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.BoPhan convert = new BoPhan();
                Convert<Object.BoPhan, Data.BoPhan>.ConvertObj(ref convert, context.BoPhans.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Ca Lam
        public IEnumerable<Object.CaLam> layDanhSachCaLam()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.CaLam, Data.CaLam>.ConvertObjs(context.CaLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.CaLam>> layDanhSachCaLam(Object.CaLam findBy = null, int page = 1, int pageSize = 10, string sortBy = "CL_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.CaLam> results =
                    (findBy != null ? timKiemNhieuCaLam(findBy)
                    : Convert<Object.CaLam, Data.CaLam>.ConvertObjs(from CaLam in context.CaLams select CaLam));
                results = ObjectHelper.OrderByDynamic<Object.CaLam>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.CaLam>>()
                    {
                        new List<Object.CaLam>(),
                        new List<Object.CaLam>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.CaLam>>()
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
        public IEnumerable<Object.CaLam> timKiemNhieuCaLam(Object.CaLam find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.CaLam, Data.CaLam>.ConvertObjs(from CaLam in context.CaLams
                        where
                        (!string.IsNullOrEmpty(find.CL_Ma) ? CaLam.CL_Ma.StartsWith(find.CL_Ma) : CaLam != null) &&
                        (!string.IsNullOrEmpty(find.CL_TenCa) ? CaLam.CL_TenCa.StartsWith(find.CL_TenCa) : CaLam != null) &&
                        (find.CL_GioBatDau != null ? CaLam.CL_GioBatDau == find.CL_GioBatDau : CaLam != null) &&
                        (find.CL_PhutBatDau != null ? CaLam.CL_PhutBatDau == find.CL_PhutBatDau : CaLam != null) &&
                        (find.CL_GioKetThuc != null ? CaLam.CL_GioKetThuc == find.CL_GioKetThuc : CaLam != null) &&
                        (find.CL_PhutKetThuc != null ? CaLam.CL_PhutKetThuc == find.CL_PhutKetThuc : CaLam != null) 
                        select CaLam);
                }
                return Convert<Object.CaLam, Data.CaLam>.ConvertObjs(context.CaLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.CaLam timKiemMotCaLam(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.CaLam convert = new CaLam();
                Convert<Object.CaLam, Data.CaLam>.ConvertObj(ref convert, context.CaLams.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Ngay nghi
        public IEnumerable<Object.NgayNghi> layDanhSachNgayNghi()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(context.NgayNghis);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.NgayNghi>> layDanhSachNgayNghi(Object.NgayNghi findBy = null, int page = 1, int pageSize = 10, string sortBy = "NN_Ngay", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.NgayNghi> results =
                    (findBy != null ? timKiemNhieuNgayNghi(findBy)
                    : Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(from NgayNghi in context.NgayNghis select NgayNghi));
                results = ObjectHelper.OrderByDynamic<Object.NgayNghi>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.NgayNghi>>()
                    {
                        new List<Object.NgayNghi>(),
                        new List<Object.NgayNghi>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.NgayNghi>>()
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
        public IEnumerable<Object.NgayNghi> timKiemNhieuNgayNghi(Object.NgayNghi find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(from NgayNghi in context.NgayNghis
                        where
                        (!string.IsNullOrEmpty(find.NN_GhiChu) ? NgayNghi.NN_GhiChu.StartsWith(find.NN_GhiChu) : NgayNghi != null) &&
                        (find.NN_Ngay != null ? NgayNghi.NN_Ngay == find.NN_Ngay : NgayNghi != null) 
                        select NgayNghi);
                }
                return Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObjs(context.NgayNghis);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.NgayNghi timKiemMotNgayNghi(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.NgayNghi convert = new NgayNghi();
                Convert<Object.NgayNghi, Data.NgayNghi>.ConvertObj(ref convert, context.NgayNghis.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Cham cong
        public IEnumerable<Object.ChamCong> layDanhSachChamCong()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(context.ChamCongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.ChamCong>> layDanhSachChamCong(Object.ChamCong findBy = null, int page = 1, int pageSize = 10, string sortBy = "CC_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.ChamCong> results =
                    (findBy != null ? timKiemNhieuChamCong(findBy)
                    : Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(from ChamCong in context.ChamCongs select ChamCong));
                results = ObjectHelper.OrderByDynamic<Object.ChamCong>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.ChamCong>>()
                    {
                        new List<Object.ChamCong>(),
                        new List<Object.ChamCong>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.ChamCong>>()
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
        public IEnumerable<Object.ChamCong> timKiemNhieuChamCong(Object.ChamCong find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(from ChamCong in context.ChamCongs
                        where
                        (find.CC_Ma != null ? ChamCong.CC_Ma == find.CC_Ma : ChamCong != null) &&
                        (find.CC_ThoiGianDen != null ? ChamCong.CC_ThoiGianDen == find.CC_ThoiGianDen : ChamCong != null) &&
                        (find.CC_ThoiGianVe != null ? ChamCong.CC_ThoiGianVe == find.CC_ThoiGianVe : ChamCong != null) &&
                        (find.CC_Ngay != null ? ChamCong.CC_Ngay == find.CC_Ngay : ChamCong != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? ChamCong.NS_Ma.StartsWith(find.NS_Ma) : ChamCong != null)
                        select ChamCong);
                }
                return Convert<Object.ChamCong, Data.ChamCong>.ConvertObjs(context.ChamCongs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.ChamCong timKiemMotChamCong(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.ChamCong convert = new ChamCong();
                Convert<Object.ChamCong, Data.ChamCong>.ConvertObj(ref convert, context.ChamCongs.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
//Dang Ky Ca Lam
        public IEnumerable<Object.DangKyCaLam> layDanhSachDangKyCaLam()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.DangKyCaLam, Data.DangKyCaLam>.ConvertObjs(context.DangKyCaLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.DangKyCaLam>> layDanhSachDangKyCaLam(Object.DangKyCaLam findBy = null, int page = 1, int pageSize = 10, string sortBy = "DKCL_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.DangKyCaLam> results =
                    (findBy != null ? timKiemNhieuDangKyCaLam(findBy)
                    : Convert<Object.DangKyCaLam, Data.DangKyCaLam>.ConvertObjs(from DangKyCaLam in context.DangKyCaLams select DangKyCaLam));
                results = ObjectHelper.OrderByDynamic<Object.DangKyCaLam>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.DangKyCaLam>>()
                    {
                        new List<Object.DangKyCaLam>(),
                        new List<Object.DangKyCaLam>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.DangKyCaLam>>()
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
        public IEnumerable<Object.DangKyCaLam> timKiemNhieuDangKyCaLam(Object.DangKyCaLam find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.DangKyCaLam, Data.DangKyCaLam>.ConvertObjs(from DangKyCaLam in context.DangKyCaLams
                        where
                        (!string.IsNullOrEmpty(find.DKCL_Ma) ? DangKyCaLam.DKCL_Ma.StartsWith(find.DKCL_Ma) : DangKyCaLam != null) &&
                        (find.DKCL_Ngay != null ? DangKyCaLam.DKCL_Ngay == find.DKCL_Ngay : DangKyCaLam != null) &&
                        (find.DKCL_ThoiGianDangKy != null ? DangKyCaLam.DKCL_ThoiGianDangKy == find.DKCL_ThoiGianDangKy : DangKyCaLam != null) &&
                        (find.DKCL_DaDuocDuyet != null ? DangKyCaLam.DKCL_DaDuocDuyet == find.DKCL_DaDuocDuyet : DangKyCaLam != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? DangKyCaLam.NS_Ma.StartsWith(find.NS_Ma) : DangKyCaLam != null) &&
                        (!string.IsNullOrEmpty(find.CL_Ma) ? DangKyCaLam.CL_Ma.StartsWith(find.CL_Ma) : DangKyCaLam != null) 
                        select DangKyCaLam);
                }
                return Convert<Object.DangKyCaLam, Data.DangKyCaLam>.ConvertObjs(context.DangKyCaLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.DangKyCaLam timKiemMotDangKyCaLam(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.DangKyCaLam convert = new DangKyCaLam();
                Convert<Object.DangKyCaLam, Data.DangKyCaLam>.ConvertObj(ref convert, context.DangKyCaLams.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string duyetDangKyCaLam(string maDK)
        {
            try
            {
                if (string.IsNullOrEmpty(maDK))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.DangKyCaLam dangKyCaLam = context.DangKyCaLams.Find(maDK);
                if (dangKyCaLam == null)
                    return DefineError.khongTonTai;
                if (dangKyCaLam.DKCL_DaDuocDuyet && dangKyCaLam.DDK_Ma != null)
                    return "Đăng ký này đã được duyệt rồi.";
                if (dangKyCaLam.NS_Ma.Trim() == this.maNhanSu.Trim())
                    return "Bạn không thể tự duyệt đăng ký của chính bản thân.";
                Data.DuyetDangKy duyetDangKy = new Data.DuyetDangKy();
                duyetDangKy.NS_Ma = this.maNhanSu;
                duyetDangKy.DDK_ThoiGian = DateTime.Now;
                context.DuyetDangKies.Add(duyetDangKy);
                int check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                dangKyCaLam.DKCL_DaDuocDuyet = true;
                dangKyCaLam.DDK_Ma = duyetDangKy.DDK_Ma;
                context.DangKyCaLams.AddOrUpdate(dangKyCaLam);
                check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
//Dang Ky Nghi Lam
        public IEnumerable<Object.DangKyNghiLam> layDanhSachDangKyNghiLam()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.DangKyNghiLam, Data.DangKyNghiLam>.ConvertObjs(context.DangKyNghiLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.DangKyNghiLam>> layDanhSachDangKyNghiLam(Object.DangKyNghiLam findBy = null, int page = 1, int pageSize = 10, string sortBy = "DKCL_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.DangKyNghiLam> results =
                    (findBy != null ? timKiemNhieuDangKyNghiLam(findBy)
                    : Convert<Object.DangKyNghiLam, Data.DangKyNghiLam>.ConvertObjs(from DangKyNghiLam in context.DangKyNghiLams select DangKyNghiLam));
                results = ObjectHelper.OrderByDynamic<Object.DangKyNghiLam>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.DangKyNghiLam>>()
                    {
                        new List<Object.DangKyNghiLam>(),
                        new List<Object.DangKyNghiLam>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.DangKyNghiLam>>()
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
        public IEnumerable<Object.DangKyNghiLam> timKiemNhieuDangKyNghiLam(Object.DangKyNghiLam find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.DangKyNghiLam, Data.DangKyNghiLam>.ConvertObjs(from DangKyNghiLam in context.DangKyNghiLams
                        where
                        (!string.IsNullOrEmpty(find.DKNL_Ma) ? DangKyNghiLam.DKNL_Ma.StartsWith(find.DKNL_Ma) : DangKyNghiLam != null) &&
                        (find.DKNL_Ngay != null ? DangKyNghiLam.DKNL_Ngay == find.DKNL_Ngay : DangKyNghiLam != null) &&
                        (find.DKNL_ThoiGianDangKy != null ? DangKyNghiLam.DKNL_ThoiGianDangKy == find.DKNL_ThoiGianDangKy : DangKyNghiLam != null) &&
                        (find.DKNL_NghiCoPhep != null ? DangKyNghiLam.DKNL_NghiCoPhep == find.DKNL_NghiCoPhep : DangKyNghiLam != null) &&
                        (!string.IsNullOrEmpty(find.DKNL_LyDoNghi) ? DangKyNghiLam.DKNL_LyDoNghi.StartsWith(find.DKNL_LyDoNghi) : DangKyNghiLam != null) &&
                        (find.DKNL_DaDuocDuyet != null ? DangKyNghiLam.DKNL_DaDuocDuyet == find.DKNL_DaDuocDuyet : DangKyNghiLam != null) &&
                        (!string.IsNullOrEmpty(find.NS_Ma) ? DangKyNghiLam.NS_Ma.StartsWith(find.NS_Ma) : DangKyNghiLam != null) &&
                        (find.DDK_Ma != null ? DangKyNghiLam.DDK_Ma == find.DDK_Ma : DangKyNghiLam != null) 
                        select DangKyNghiLam);
                }
                return Convert<Object.DangKyNghiLam, Data.DangKyNghiLam>.ConvertObjs(context.DangKyNghiLams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.DangKyNghiLam timKiemMotDangKyNghiLam(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.DangKyNghiLam convert = new DangKyNghiLam();
                Convert<Object.DangKyNghiLam, Data.DangKyNghiLam>.ConvertObj(ref convert, context.DangKyNghiLams.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
        public string duyetDangKyNghiLam(string maDK)
        {
            try
            {
                if (string.IsNullOrEmpty(maDK))
                    return DefineError.loiDuLieuKhongHopLe;
                Data.DangKyNghiLam dangKyNghiLam = context.DangKyNghiLams.Find(maDK);
                if (dangKyNghiLam == null)
                    return DefineError.khongTonTai;
                if (dangKyNghiLam.DKNL_DaDuocDuyet && dangKyNghiLam.DDK_Ma != null && dangKyNghiLam.DDK_Ma.Value >= 0)
                    return "Đăng ký này đã được duyệt rồi.";
                if (dangKyNghiLam.NS_Ma.Trim() == this.maNhanSu.Trim())
                    return "Bạn không thể tự duyệt đăng ký của chính bản thân.";
                Data.DuyetDangKy duyetDangKy = new Data.DuyetDangKy();
                duyetDangKy.NS_Ma = this.maNhanSu;
                duyetDangKy.DDK_ThoiGian = DateTime.Now;
                context.DuyetDangKies.Add(duyetDangKy);
                int check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                dangKyNghiLam.DKNL_DaDuocDuyet = true;
                dangKyNghiLam.DDK_Ma = duyetDangKy.DDK_Ma;
                context.DangKyNghiLams.AddOrUpdate(dangKyNghiLam);
                check = context.SaveChanges();
                if (check == 0)
                    return DefineError.loiHeThong;
                return string.Empty;
            }
            catch { }
            return DefineError.loiHeThong;
        }
//Duyet dang ky
        public IEnumerable<Object.DuyetDangKy> layDanhSachDuyetDangKy()
        {
            try
            {
                init();
                if (context != null)
                    return Convert<Object.DuyetDangKy, Data.DuyetDangKy>.ConvertObjs(context.DuyetDangKies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public List<IEnumerable<Object.DuyetDangKy>> layDanhSachDuyetDangKy(Object.DuyetDangKy findBy = null, int page = 1, int pageSize = 10, string sortBy = "DKCL_Ma", bool sortTangDan = true)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                IEnumerable<Object.DuyetDangKy> results =
                    (findBy != null ? timKiemNhieuDuyetDangKy(findBy)
                    : Convert<Object.DuyetDangKy, Data.DuyetDangKy>.ConvertObjs(from DuyetDangKy in context.DuyetDangKies select DuyetDangKy));
                results = ObjectHelper.OrderByDynamic<Object.DuyetDangKy>(results, sortBy, sortTangDan ? "asc" : "desc");
                int p = results.Count();
                if (p == 0)
                    return new List<IEnumerable<Object.DuyetDangKy>>()
                    {
                        new List<Object.DuyetDangKy>(),
                        new List<Object.DuyetDangKy>()
                    };
                else
                    pageSize = pageSize < p ? pageSize : p;
                p = (p % pageSize) == 0 ? (p / pageSize) : (p / pageSize + 1);
                page = page > p ? p : page <= 0 ? 1 : page;
                return new List<IEnumerable<Object.DuyetDangKy>>()
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
        public IEnumerable<Object.DuyetDangKy> timKiemNhieuDuyetDangKy(Object.DuyetDangKy find = null)
        {
            try
            {
                init();
                if (context == null)
                    return null;
                if (find != null)
                {
                    return Convert<Object.DuyetDangKy, Data.DuyetDangKy>.ConvertObjs(from DuyetDangKy in context.DuyetDangKies
                            where
                            (find.DDK_Ma != null ? DuyetDangKy.DDK_Ma == find.DDK_Ma : DuyetDangKy != null) &&
                            (find.DDK_ThoiGian != null ? DuyetDangKy.DDK_ThoiGian == find.DDK_ThoiGian : DuyetDangKy != null) &&
                            (!string.IsNullOrEmpty(find.NS_Ma) ? DuyetDangKy.NS_Ma.StartsWith(find.NS_Ma) : DuyetDangKy != null)
                            select DuyetDangKy);
                }
                return Convert<Object.DuyetDangKy, Data.DuyetDangKy>.ConvertObjs(context.DuyetDangKies);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public Object.DuyetDangKy timKiemMotDuyetDangKy(string ma)
        {
            try
            {
                if (string.IsNullOrEmpty(ma))
                    return null;
                init();
                if (context == null)
                    return null;
                Object.DuyetDangKy convert = new DuyetDangKy();
                Convert<Object.DuyetDangKy, Data.DuyetDangKy>.ConvertObj(ref convert, context.DuyetDangKies.Find(ma));
                return convert;
            }
            catch { }
            return null;
        }
    }
}