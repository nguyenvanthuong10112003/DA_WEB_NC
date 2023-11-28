using ProgramWEB.Models.Data;
using ProgramWEB.Models.DAO;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProgramWEB.Models;
using ProgramWEB.Define;
using System.Linq;
using Newtonsoft.Json.Converters;
using Microsoft.Ajax.Utilities;

namespace ProgramWEB.Controllers
{
    public class NhanSuController : BaseController
    {
        public string getAll(int page = 1, int pageSize = 10)
        {
            UserLogin userLogin = (UserLogin)Session[DefineSession.userSession];
            if (userLogin == null || !userLogin.quyenQuanLy)
                return "";
            try
            {
                var data = new NhanSuDAO().getAll(null, null, page, pageSize);
                return JsonConvert.SerializeObject(new
                {
                    data = data
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
            UserLogin userLogin = (UserLogin)Session[DefineSession.userSession];
            if (userLogin == null)
                return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này" });
            if (!userLogin.quyenQuanLy)
                return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này" });
            if (nhanSu == null)
                return JsonConvert.SerializeObject(new { error = "Thông tin gửi lên không hợp lệ" });
            NhanSuDAO nhanSuDAO = new NhanSuDAO();
            string error = nhanSuDAO.add(nhanSu);
            if (!string.IsNullOrEmpty(error))
                return JsonConvert.SerializeObject(new { error = error });
            return JsonConvert.SerializeObject(new
            {
                success = "Thêm thành công"
            });
        }
        public string edit(NhanSu nhanSu)
        {
            UserLogin userLogin = (UserLogin)Session[DefineSession.userSession];
            if (userLogin == null)
                return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này" });
            if (!userLogin.quyenQuanLy)
                return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này" });
            if (nhanSu == null) 
                return JsonConvert.SerializeObject(new { error = "Thông tin gửi lên không hợp lệ" });
            NhanSuDAO nhanSuDAO = new NhanSuDAO();
            string error = nhanSuDAO.edit(nhanSu);
            if (!string.IsNullOrEmpty(error))
                return JsonConvert.SerializeObject(new { error = error });
            return JsonConvert.SerializeObject(new
            {
                success = "Sửa thành công"
            });
        }
        public string delete(string ma)
        {
            UserLogin userLogin = (UserLogin)Session[DefineSession.userSession];
            if (userLogin == null)
                return JsonConvert.SerializeObject(new { error = "Bạn cần phải đăng nhập mới có thể sử dụng chức năng này" });
            if (!userLogin.quyenQuanLy)
                return JsonConvert.SerializeObject(new { error = "Bạn không có quyền sử dụng chức năng này" });
            TaiKhoan taiKhoan = new TaiKhoanDAO().GetTaiKhoanByMaNhanSu(ma);
            if (taiKhoan != null)
            {
                if (!userLogin.quyenAdmin)
                    if ((taiKhoan.TK_QuyenQuanLy != null && taiKhoan.TK_QuyenQuanLy.Value == true) ||
                        (taiKhoan.TK_QuyenAdmin != null && taiKhoan.TK_QuyenAdmin.Value == true))
                        return JsonConvert.SerializeObject(new { error = "Bạn không thể xóa người có quyền quản lý hệ thống này" });
                if (userLogin.quyenAdmin)
                    if (taiKhoan.TK_QuyenAdmin != null && taiKhoan.TK_QuyenAdmin.Value == true)
                        return JsonConvert.SerializeObject(new { error = "Bạn không thể xóa admin của hệ thống" });
            }
            bool check = new NhanSuDAO().delete(ma);
            return JsonConvert.SerializeObject(new 
            { 
                success = check ? "Xóa nhân sự thành công" : string.Empty,
                error = !check ? "Xóa thất bại, vui lòng thử lại sau" : string.Empty
            }); 
        }
    }
}