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
            QuanLy user = (QuanLy)Session[DefineSession.userSession];
            if (!(user != null && user.quyenQuanLy))
                return RedirectToAction("NotFound", "Error");
            Dictionary<string, string> findBy = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(ma))
                findBy.Add(DefineTable.nhanSu.thuocTinhs[0], ma);  
            ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
            {
                nameModel = DefineTable.nhanSu.thuocTinhs,
                nameRender = DefineTable.nhanSu.tenTiengViet,
                sortBy = DefineTable.nhanSu.thuocTinhs[0],
                use = DefineTable.nhanSu.suDung,
                findBy = findBy
            });
            return View();
        }

        public ActionResult PhongBan()
        {
            return View();
        }
        
        public ActionResult TaiKhoan()
        {
            User User = (User)Session[DefineSession.userSession];
            if (!(User != null && User.quyenQuanLy))
                return RedirectToAction("NotFound", "Error");
            ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
            {
                nameModel = DefineTable.taiKhoan.thuocTinhs,
                nameRender = DefineTable.taiKhoan.tenTiengViet,
                sortBy = DefineTable.taiKhoan.thuocTinhs[0],
                use = DefineTable.taiKhoan.suDung
            });
            return View();
        }
    }
}