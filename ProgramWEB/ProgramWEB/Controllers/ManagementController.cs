using Newtonsoft.Json;
using ProgramWEB.Define;
using ProgramWEB.Models.Object;
using System.Linq;
using System.Web.Mvc;
using System.Text.Json;
using ProgramWEB.Models;
using System.Collections.Generic;
using ProgramWEB.Models.Data;
namespace ProgramWEB.Controllers
{
    public class ManagementController : BaseController
    {
        // GET: Management
        public ActionResult Index()
        {
            return View(); 
        }
        public ActionResult NhanSu(string ma)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, string> findBy = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ma))
                    findBy.Add(DefineTable.nhanSu.thuocTinhs[0], ma);
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("add", true);
                action.Add("edit", true);
                action.Add("delete", true);
                action.Add("search", true);
                action.Add("view", true);
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.nhanSu.thuocTinhs,
                    nameRender = DefineTable.nhanSu.tenTiengViet,
                    sortBy = DefineTable.nhanSu.thuocTinhs[0],
                    use = DefineTable.nhanSu.suDung,
                    findBy = findBy,
                    action = action
                });
            } catch { }
            return View();
        }

        public ActionResult PhongBan()
        {
            return View();
        }
        
        public ActionResult TaiKhoan()
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                try
                {
                    Admin admin = (Admin)user;
                    if (admin != null)
                    {
                        action.Add("capVaHuyQuyen", true);
                        action.Add("khoaTaiKhoan", true);
                    }
                }
                catch { }
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.taiKhoan.thuocTinhs,
                    nameRender = DefineTable.taiKhoan.tenTiengViet,
                    sortBy = DefineTable.taiKhoan.thuocTinhs[0],
                    use = DefineTable.taiKhoan.suDung,
                    action = action
                });
            } catch { }
            return View();
        }
        public ActionResult BaoHiem()
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("add", true);
                action.Add("edit", true);
                action.Add("delete", true);
                action.Add("search", true);
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.baoHiem.thuocTinhs,
                    nameRender = DefineTable.baoHiem.tenTiengViet,
                    sortBy = DefineTable.baoHiem.thuocTinhs[0],
                    use = DefineTable.baoHiem.suDung,
                    action = action
                });
            }
            catch { }
            return View();
        }
    }
}