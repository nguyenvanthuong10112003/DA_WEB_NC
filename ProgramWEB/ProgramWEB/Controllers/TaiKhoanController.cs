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
    public class TaiKhoanController : BaseController
    {
        public string getAll(TaiKhoan findBy = null, int page = 1, int pageSize = 10, string sortBy = "TK_TenDangNhap", bool sortTangDan = true)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null || !user.quyenQuanLy)
                    return "";
                List<IEnumerable<TaiKhoan>> results = user.layDanhSachTaiKhoan(findBy, page, pageSize, sortBy, sortTangDan);
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
        public string khoaTaiKhoan(string ma)
        {
            try
            {
                Admin user = (Admin)Session[DefineSession.userSession];
                if (user == null || !user.quyenAdmin)
                    return JsonConvert.SerializeObject(new
                    {
                        error = "Bạn không có quyền sử dụng chức năng này"
                    });
            } catch { }
            return DefineError.loiHeThong;
        }
    }
}