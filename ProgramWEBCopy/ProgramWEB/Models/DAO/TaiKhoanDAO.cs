using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace ProgramWEB.Models.DAO
{
    public class TaiKhoanDAO
    {
        private QuanLyNhanSuContext context { set; get; } = null;
        public TaiKhoanDAO()
        {
            context = new QuanLyNhanSuContext();
        }
        public TaiKhoan getTaiKhoanByUsername(string username)
        {
            try
            {
                return context.TaiKhoans.Find(username);
            } catch { return null; }
        }
        public bool checkLocked(TaiKhoan taiKhoan)
        {
            if (taiKhoan != null)
            {
                if (taiKhoan.TK_BiKhoa == true)
                {
                    if (taiKhoan.TK_ThoiGianMoKhoa != null &&
                        taiKhoan.TK_ThoiGianMoKhoa <= DateTime.Now)
                    {
                        taiKhoan.TK_BiKhoa = !taiKhoan.TK_BiKhoa;
                        int check = context.SaveChanges();
                        if (check == 0)
                            return true;
                        return false;
                    }
                }
                else
                    return false;
            }
            return true;
        }
        public TaiKhoan GetTaiKhoanByMaNhanSu(string code)
        {
            return context.TaiKhoans.Where(e => e.NS_Ma == code).FirstOrDefault();
        }
        public bool Insert(TaiKhoan taiKhoan)
        {
            context.TaiKhoans.Add(taiKhoan);
            int count = context.SaveChanges();
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        public bool updateTaiKhoan(TaiKhoan taiKhoan)
        {
            if (taiKhoan != null)
            {
                context.TaiKhoans.AddOrUpdate(taiKhoan);
                int count = context.SaveChanges();
                if (count >= 1)
                    return true;
            }
            return false;
        }
        public IEnumerable<TaiKhoan> getAll()
        {
            return context.TaiKhoans.Select(item => item);
        }
    }
}