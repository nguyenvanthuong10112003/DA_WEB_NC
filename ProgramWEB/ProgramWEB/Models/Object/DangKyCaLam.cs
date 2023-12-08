using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class DangKyCaLam
    {
        public long? DKCL_Ma { get; set; }
        public DateTime? DKCL_Ngay { get; set; }
        public DateTime? DKCL_ThoiGianDangKy { get; set; }
        public bool? DKCL_DaDuocDuyet { get; set; }
        public string NS_Ma { get; set; }
        public string CL_Ma { get; set; }
        public long? DDK_Ma { get; set; }
        public DangKyCaLam()
        {
            this.DKCL_Ma = null;
            this.DKCL_Ngay = null;
            this.DKCL_ThoiGianDangKy = null;
            this.DKCL_DaDuocDuyet = null;
            this.NS_Ma = string.Empty;
            this.CL_Ma = string.Empty;
            this.DDK_Ma = null;
        }
    }
}