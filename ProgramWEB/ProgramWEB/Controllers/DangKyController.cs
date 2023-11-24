using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProgramWEB.Controllers
{
    public class DangKyController : BaseController
    {
        // GET: DangKy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LichLamViec()
        {
            return View();
        }
    }
}