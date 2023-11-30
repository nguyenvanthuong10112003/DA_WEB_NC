using Newtonsoft.Json;
using ProgramWEB.Define;
using ProgramWEB.Models.Object;
using System.Linq;
using System.Web.Mvc;
using System.Text.Json;
using ProgramWEB.Models;
namespace ProgramWEB.Controllers
{
    public class ManagementController : BaseController
    {
        // GET: Management
        public ActionResult Index()
        {
            return View(); 
        }
        public ActionResult NhanSu()
        {
            User User = (User)Session[DefineSession.userSession];
            if (!(User != null && User.quyenQuanLy))
                return RedirectToAction("NotFound", "Error");
            ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
            {
                nameModel = DefineTable.nhanSu.thuocTinhs,
                nameRender = DefineTable.nhanSu.tenTiengViet,
                sortBy = DefineTable.nhanSu.thuocTinhs[0]
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
                action = new string[] {}
            });
            return View();
        }
    }
}