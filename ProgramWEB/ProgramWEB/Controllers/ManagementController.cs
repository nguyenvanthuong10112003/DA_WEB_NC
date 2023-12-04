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
                    findBy = findBy,
                    action = action
                });
            } catch { }
            return View();
        }
        public ActionResult PhongBan(string ma)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, string> findBy = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ma))
                    findBy.Add(DefineTable.phongBan.thuocTinhs[0], ma);
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                try
                {
                    Admin admin = (Admin)user;
                    if (admin != null)
                    {
                        action.Add("add", true);
                        action.Add("edit", true);
                        action.Add("delete", true);
                    }
                }
                catch { }
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.phongBan.thuocTinhs,
                    nameRender = DefineTable.phongBan.tenTiengViet,
                    sortBy = DefineTable.phongBan.thuocTinhs[0],
                    action = action,
                    findBy = findBy
                });
            }
            catch { }
            return View();
        }  
        public ActionResult BoPhan(string ma)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, string> findBy = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ma))
                    findBy.Add(DefineTable.boPhan.thuocTinhs[0], ma);
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                try
                {
                    Admin admin = (Admin)user;
                    if (admin != null)
                    {
                        action.Add("add", true);
                        action.Add("edit", true);
                        action.Add("delete", true);
                    }
                }
                catch { }
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.boPhan.thuocTinhs,
                    nameRender = DefineTable.boPhan.tenTiengViet,
                    sortBy = DefineTable.boPhan.thuocTinhs[0],
                    action = action,
                    findBy = findBy
                });
            }
            catch { }
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
                    action = action
                });
            }
            catch { }
            return View();
        }
        public ActionResult HopDong()
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
                    nameModel = DefineTable.hopDong.thuocTinhs,
                    nameRender = DefineTable.hopDong.tenTiengViet,
                    sortBy = DefineTable.hopDong.thuocTinhs[0],
                    action = action
                });
            }
            catch { }
            return View();
        }
        public ActionResult LichSuLamViec()
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
                    nameModel = DefineTable.lichSuLamViec.thuocTinhs,
                    nameRender = DefineTable.lichSuLamViec.tenTiengViet,
                    sortBy = DefineTable.lichSuLamViec.thuocTinhs[0],
                    action = action
                });
            }
            catch { }
            return View();
        }
        public ActionResult KhenThuongKyLuat()
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
                    nameModel = DefineTable.khenThuongKyLuat.thuocTinhs,
                    nameRender = DefineTable.khenThuongKyLuat.tenTiengViet,
                    sortBy = DefineTable.khenThuongKyLuat.thuocTinhs[0],
                    action = action
                });
            }
            catch { }
            return View();
        }
        public ActionResult CaLam(string ma)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, string> findBy = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ma))
                    findBy.Add(DefineTable.caLam.thuocTinhs[0], ma);
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                try
                {
                    Admin admin = (Admin)user;
                    if (admin != null)
                    {
                        action.Add("add", true);
                        action.Add("edit", true);
                        action.Add("delete", true);
                    }
                }
                catch { }
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.caLam.thuocTinhs,
                    nameRender = DefineTable.caLam.tenTiengViet,
                    sortBy = DefineTable.caLam.thuocTinhs[0],
                    action = action,
                    findBy = findBy
                });
            }
            catch { }
            return View();
        }
        public ActionResult NgayNghi(string ma)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, string> findBy = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ma))
                    findBy.Add(DefineTable.ngayNghi.thuocTinhs[0], ma);
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                try
                {
                    Admin admin = (Admin)user;
                    if (admin != null)
                    {
                        action.Add("add", true);
                        action.Add("edit", true);
                        action.Add("delete", true);
                    }
                }
                catch { }
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.ngayNghi.thuocTinhs,
                    nameRender = DefineTable.ngayNghi.tenTiengViet,
                    sortBy = DefineTable.ngayNghi.thuocTinhs[0],
                    action = action,
                    findBy = findBy
                });
            }
            catch { }
            return View();
        }
    }
}