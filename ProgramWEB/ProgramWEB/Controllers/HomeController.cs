using Newtonsoft.Json;
using ProgramWEB.Define;
using ProgramWEB.Models.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramWEB.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            try
            {
                User user = (User)Session[DefineSession.userSession];
                if (user == null)
                    return RedirectToAction("NotFound", "Error");
                List<Dictionary<string, object>> listManagement = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> listItem = new List<Dictionary<string, object>>();
                Dictionary<string, object> item1 = new Dictionary<string, object>();
                Dictionary<string, object> item = new Dictionary<string, object>();

                //Thong tin ca nhan
                item.Add("define", DefinePage.user_Profile);
                item.Add("icon", "fa-solid fa-address-card");
                listItem.Add(item);

                item = new Dictionary<string, object>();
                item.Add("define", DefinePage.user);
                item.Add("icon", "fa-solid fa-user");
                listItem.Add(item);

                item1.Add("name", "Người dùng");
                item1.Add("data", listItem);
                listManagement.Add(item1);
                item1 = new Dictionary<string, object>();
                listItem = new List<Dictionary<string, object>>();

                //Cac chuc nang
                if (user.quyenQuanLy)
                {
                    item = new Dictionary<string, object>();
                    item.Add("define", DefinePage.management);
                    item.Add("icon", "fa-solid fa-bars-progress");
                    listItem.Add(item);
                }

                item = new Dictionary<string, object>();
                item.Add("define", DefinePage.dangKy);
                item.Add("icon", "fa-solid fa-file-pen");
                listItem.Add(item);

                item = new Dictionary<string, object>();
                item.Add("define", DefinePage.chamCong);
                item.Add("icon", "fa-regular fa-calendar-check");
                listItem.Add(item);

                item1.Add("name", "Chức năng");
                item1.Add("data", listItem);
                listManagement.Add(item1);
                item1 = new Dictionary<string, object>();
                listItem = new List<Dictionary<string, object>>();

                ViewBag.data = JsonConvert.SerializeObject(new
                {
                    data = listManagement
                });
            } catch { }
            return View();
        }
    }
}