using Newtonsoft.Json;
using ProgramWEB.Define;
using ProgramWEB.Models.DAO;
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
            UserLogin userLogin = (UserLogin)Session[DefineSession.userSession];
            if (!(userLogin != null && userLogin.quyenQuanLy))
                return RedirectToAction("NotFound", "Error");
            NhanSuDAO nhanSuDAO = new NhanSuDAO();
            var count = nhanSuDAO.getCount();
            ViewBag.jsonString = ViewBag.jsonString = System.Text.Json.JsonSerializer.Serialize(new
            {
                countData = count,
                nameModel = DefineTable.nhanSu.thuocTinhs,
                nameRender = DefineTable.nhanSu.tenTiengViet
            });
            return View();
        }
    }
}