using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramWEB.Libary
{
    public class DateHelper
    {
        public static bool soSanhNgay(DateTime a, DateTime b)
        {
            if (a == null && b == null)
                return true;
            if (a == null ||  b == null) 
                return false;
            if (a.Day == b.Day && a.Month == b.Month && a.Year == b.Year)
                return true;
            return false;
        }
    }
}