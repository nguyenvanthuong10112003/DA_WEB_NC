using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class NgayNghi
    {
        public DateTime? NN_Ngay { get; set; }
        public string NN_GhiChu { get; set; }
        public NgayNghi()
        {
            this.NN_Ngay = null;
            this.NN_GhiChu = string.Empty;
        }
    }
}