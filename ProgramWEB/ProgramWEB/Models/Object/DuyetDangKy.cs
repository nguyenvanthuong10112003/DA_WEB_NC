﻿using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.Object
{
    public class DuyetDangKy
    {
        public long? DDK_Ma { get; set; }
        public DateTime? DDK_ThoiGian { get; set; }
        public string NS_Ma { get; set; }
        public DuyetDangKy() { 
            this.DDK_Ma = null;
            this.DDK_ThoiGian = null;
            this.NS_Ma = string.Empty;
        }
    }
}