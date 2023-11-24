using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models
{
    public class UserLogin
    {
        public string username { set; get; }
        public string maNhanSu { set; get; }
        public bool quyenAdmin { set; get; }
        public bool quyenQuanLy { set; get; }
        public string avatar { set; get; }
        public UserLogin() { } 
        public UserLogin(string username, string maNhanSu, bool quyenAdmin, bool quyenQuanLy, string avatar)
        {
            this.username = username;
            this.maNhanSu = maNhanSu;
            this.quyenAdmin = quyenAdmin;
            this.quyenQuanLy = quyenQuanLy;
            this.avatar = avatar;
        }
    }
}