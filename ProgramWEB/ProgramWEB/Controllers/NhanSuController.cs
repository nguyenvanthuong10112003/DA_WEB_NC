using ProgramWEB.Models.Data;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProgramWEB.Models.Object;
using ProgramWEB.Define;
using System.Linq;
using Newtonsoft.Json.Converters;
using Microsoft.Ajax.Utilities;
using System.Collections.Generic;
using PagedList;
using System.Globalization;
using System.Collections;
using System;

namespace ProgramWEB.Controllers
{
    public class NhanSuController : BaseController
    {
        public string getAll(NhanSu findBy = null, int page = 1, int pageSize = 10, string sortBy = "NS_Ma", bool sortTangDan = true)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null || !user.quyenQuanLy)
                    return "";
                List<IEnumerable<NhanSu>> results = user.layDanhSachNhanSu(findBy, page, pageSize, sortBy, sortTangDan);
                if (results == null)
                    return "";
                return JsonConvert.SerializeObject(new
                {
                    countData = results[0].Count(),
                    data = results[1]
                }, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
                );
            }
            catch { }
            return "";
        }
        public string add(NhanSu nhanSu)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này." });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này." });
                string error = ((QuanLy)user).themNhanSu(nhanSu);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                return JsonConvert.SerializeObject(new
                {
                    success = "Thêm thành công."
                });
            } catch { }
            return JsonConvert.SerializeObject(new 
            { 
                error = "Có lỗi xảy ra, vui lòng thử lại sau."
            });
        }
        public string edit(NhanSu nhanSu)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này" });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này" });
                string error = ((QuanLy)user).suaNhanSu(nhanSu);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                return JsonConvert.SerializeObject(new
                {
                    success = "Sửa thành công"
                });
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = "Có lỗi xảy ra, vui lòng thử lại sau."
            });
        }
        public string delete(string[] mas)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này" });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này" });
                string error = ((QuanLy)user).xoaNhieuNhanSu(mas);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                return JsonConvert.SerializeObject(new
                {
                    success = "Xóa nhân sự thành công"
                });
            } catch { }
            return JsonConvert.SerializeObject(new
            {
                error = "Có lỗi xảy ra, vui lòng thử lại sau."
            });
        }
    }
}