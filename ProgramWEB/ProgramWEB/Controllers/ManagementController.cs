﻿using Newtonsoft.Json;
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
            QuanLy user = (QuanLy)Session[DefineSession.userSession];
            if (!(user != null && user.quyenQuanLy))
                return RedirectToAction("NotFound", "Error");
            List<Dictionary<string, object>> listManagement = new List<Dictionary<string, object>>();
            List<Dictionary<string, object>> listItem = new List<Dictionary<string, object>>();
            Dictionary<string, object> item1 = new Dictionary<string, object>();
            Dictionary<string, object> item = new Dictionary<string, object>();
            //Nhan Su
            item.Add("define", DefinePage.management_NhanSu);
            item.Add("count", user.layDanhSachNhanSu().Count());
            listItem.Add(item);

            //Bao hiem
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_BaoHiem);
            item.Add("count", user.layDanhSachBaoHiem().Count());
            listItem.Add(item);

            //Tai khoan
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_TaiKhoan);
            item.Add("count", user.layDanhSachTaiKhoan().Count());
            listItem.Add(item);

            item1.Add("name", "Quản lý thông tin nhân sự");
            item1.Add("data", listItem);
            listManagement.Add(item1);
            item1= new Dictionary<string, object>();
            listItem = new List<Dictionary<string, object>>();

            //Dang Ky ca lam
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_DangKyCaLam);
            item.Add("count", user.layDanhSachDangKyCaLam().Count());
            listItem.Add(item);

            //Dang ký nghi lam
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_DangKyNghiLam);
            item.Add("count", user.layDanhSachDangKyNghiLam().Count());
            listItem.Add(item);

            //Cham cong
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_ChamCong);
            item.Add("count", user.layDanhSachChamCong().Count());
            listItem.Add(item);

            //Khen thuong ky luat
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_KhenThuongKyLuat);
            item.Add("count", user.layDanhSachKhenThuongKyLuat().Count());
            listItem.Add(item);

            //Lich su lam viec
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_LichSuLamViec);
            item.Add("count", user.layDanhSachLichSuLamViec().Count());
            listItem.Add(item);

            //Hop Dong
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_HopDong);
            item.Add("count", user.layDanhSachHopDong().Count());
            listItem.Add(item);

            item1.Add("name", "Quản lý thông tin làm việc");
            item1.Add("data", listItem);
            listManagement.Add(item1);
            item1 = new Dictionary<string, object>();
            listItem = new List<Dictionary<string, object>>();

            //Phong ban
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_PhongBan);
            item.Add("count", user.layDanhSachPhongBan().Count());
            listItem.Add(item);

            //Bo phan
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_BoPhan);
            item.Add("count", user.layDanhSachBoPhan().Count());
            listItem.Add(item);

            //Ca lam
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_CaLam);
            item.Add("count", user.layDanhSachCaLam().Count());
            listItem.Add(item);

            //Ngay Nghi
            item = new Dictionary<string, object>();
            item.Add("define", DefinePage.management_NgayNghi);
            item.Add("count", user.layDanhSachNgayNghi().Count());
            listItem.Add(item);

            item1.Add("name", "Quản lý thông tin công ty");
            item1.Add("data", listItem);
            listManagement.Add(item1);

            ViewBag.data = JsonConvert.SerializeObject(new
            {
                data = listManagement
            });
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
        public ActionResult ChamCong()
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.chamCong.thuocTinhs,
                    nameRender = DefineTable.chamCong.tenTiengViet,
                    sortBy = DefineTable.chamCong.thuocTinhs[0],
                    action = action
                });
            }
            catch { }
            return View();
        }
        public ActionResult DangKyCaLam()
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                action.Add("duyetDangKy", true);
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.dangKyCaLam.thuocTinhs,
                    nameRender = DefineTable.dangKyCaLam.tenTiengViet,
                    sortBy = DefineTable.dangKyCaLam.thuocTinhs[0],
                    action = action
                });
            }
            catch { }
            return View();
        }
        public ActionResult DangKyNghiLam()
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                action.Add("duyetDangKy", true);
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.dangKyNghiLam.thuocTinhs,
                    nameRender = DefineTable.dangKyNghiLam.tenTiengViet,
                    sortBy = DefineTable.dangKyNghiLam.thuocTinhs[0],
                    action = action
                });
            }
            catch { }
            return View();
        }
        public ActionResult DuyetDangKy(long ma)
        {
            try
            {
                QuanLy user = (QuanLy)Session[DefineSession.userSession];
                if (!(user != null && user.quyenQuanLy))
                    return RedirectToAction("NotFound", "Error");
                Dictionary<string, long> findBy = new Dictionary<string, long>();
                if (ma >= 0)
                    findBy.Add(DefineTable.ngayNghi.thuocTinhs[0], ma);
                Dictionary<string, bool> action = new Dictionary<string, bool>();
                action.Add("search", true);
                ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
                {
                    nameModel = DefineTable.duyetDangKy.thuocTinhs,
                    nameRender = DefineTable.duyetDangKy.tenTiengViet,
                    sortBy = DefineTable.duyetDangKy.thuocTinhs[0],
                    action = action,
                    findBy
                });
            }
            catch { }
            return View();
        }
    }
}