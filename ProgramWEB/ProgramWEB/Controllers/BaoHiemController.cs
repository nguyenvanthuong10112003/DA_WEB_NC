using Newtonsoft.Json;
using ProgramWEB.Define;
using ProgramWEB.Models.Data;
using ProgramWEB.Models.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramWEB.Controllers
{
    public class BaoHiemController : Controller
    {
        public string getAll(BaoHiem findBy = null, int page = 1, int pageSize = 10, string sortBy = "BH_Ma", bool sortTangDan = true)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null || !user.quyenQuanLy)
                    return "";
                List<IEnumerable<BaoHiem>> results = user.layDanhSachBaoHiem(findBy, page, pageSize, sortBy, sortTangDan);
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
        public string add(BaoHiem baoHiem)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này." });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này." });
                string error = ((QuanLy)user).themBaoHiem(baoHiem);
                if (!string.IsNullOrEmpty(error))
                    return JsonConvert.SerializeObject(new { error = error });
                return JsonConvert.SerializeObject(new
                {
                    success = "Thêm thành công."
                });
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
        public string edit(BaoHiem baoHiem)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null)
                    return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này" });
                if (!user.quyenQuanLy)
                    return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này" });
                string error = ((QuanLy)user).themBaoHiem(baoHiem);
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
                error = DefineError.loiHeThong
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
                if (mas != null && mas.Length == 1)
                {
                    string error = ((QuanLy)user).xoaBaoHiem(mas[0]);
                    if (!string.IsNullOrEmpty(error))
                        return JsonConvert.SerializeObject(new { error = error });
                    return JsonConvert.SerializeObject(new
                    {
                        success = "Xóa thành công"
                    });
                }
                if (mas != null && mas.Length > 1)
                {
                    string[] message = user.xoaNhieuNhanSu(mas);
                    if (message.Length > 0)
                    {
                        string error = message[0];
                        string success = message.Length > 1 ? message[1] : string.Empty;
                        return JsonConvert.SerializeObject(new
                        {
                            success = success,
                            error = error
                        });
                    }
                }
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
    }
}