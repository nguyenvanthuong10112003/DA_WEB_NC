using ProgramWEB.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class Admin : QuanLy
    {
        public Admin() : base()
        {

        }
        public Admin(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar) : base(username, maNhanSu, quyenAdmin, quyenQuanLy, avatar)
        {
        }
    }
}