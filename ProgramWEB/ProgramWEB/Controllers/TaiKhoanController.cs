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
                TaiKhoan tai = findBy;
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (user == null || !user.quyenQuanLy)
                    return string.Empty;
                List<IEnumerable<TaiKhoan>> results = user.layDanhSachTaiKhoan(findBy, page, pageSize, sortBy, sortTangDan);
                if (results == null)
                    return string.Empty;
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
            return string.Empty;
        }
        public string khoaTaiKhoan(string username, int time = -1)
        {
            try
            {
                Admin user = (Admin)Session[DefineSession.userSession];
                if (user == null || !user.quyenAdmin)
                    return JsonConvert.SerializeObject(new
                    {
                        error = DefineError.khongCoQuyen
                    });

            } catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            }); 
        }
        public string capQuyen(string username, bool huy = false)
        {
            try
            {
                Admin user = (Admin)Session[DefineSession.userSession];
                if (user == null || !user.quyenAdmin)
                    return JsonConvert.SerializeObject(new
                    {
                        error = DefineError.khongCoQuyen
                    });
            }
            catch { }
            return JsonConvert.SerializeObject(new
            {
                error = DefineError.loiHeThong
            });
        }
    }
}