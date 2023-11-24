using ProgramWEB.Models.Data;
using ProgramWEB.Models.DAO;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProgramWEB.Models;
using ProgramWEB.Define;
using System.Linq;
using Newtonsoft.Json.Converters;

namespace ProgramWEB.Controllers
{
    public class NhanSuController : BaseController
    {
        // GET: NhanSu
        [HttpPost]
        public string getAll(int page = 1, int pageSize = 10)
        {
            UserLogin userLogin = (UserLogin)Session[DefineSession.userSession];
            if (userLogin != null && userLogin.quyenQuanLy)
            {
                var data = new NhanSuDAO().getAll(null, null, page, pageSize);
                return JsonConvert.SerializeObject(new
                {
                    data = data
                },Formatting.Indented, 
                new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                    }
                );
            }
            return "";
        }
    }
}