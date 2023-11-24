using PagedList;
using ProgramWEB.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ProgramWEB.Models.DAO
{
    public class NhanSuDAO
    {
        private QuanLyNhanSuContext context { set; get; } = null;
        public NhanSuDAO()
        {
            context = new QuanLyNhanSuContext();
        }
        public IEnumerable<NhanSu> getAll(object[] findBy = null, string[] sortBy = null, int page = 1, int pageSize = 10)
        {
            try
            {
                if (context != null)
                {
                    //return findBy != null ?
                    //    context.NhanSus.Where(item =>
                    //    findBy[0] == null ? true : item.NS_Ma.Trim() == (string)findBy[0] &&
                    //    findBy[1] == null ? true : item.NS_HoVaTen.Trim() == (string)findBy[1] &&
                    //    findBy[2] == null ? true : item.NS_GioiTinh == (bool)findBy[2] &&
                    //    findBy[3] == null ? true :
                    //            DateHelper.soSanhNgay(item.NS_NgaySinh.GetValueOrDefault(), (DateTime)findBy[3]) &&
                    //    findBy[4] == null ? true : item.NS_SoDienThoai.Trim() == (string)findBy[4] &&
                    //    findBy[5] == null ? true : item.NS_Email.Trim() == (string)findBy[5] &&
                    //    findBy[6] == null ? true : item.NS_DiaChi.Trim() == (string)findBy[6] &&
                    //    findBy[7] == null ? true : item.NS_SoCCCD.Trim() == (string)findBy[7] &&
                    //    findBy[8] == null ? true : item.NS_SoTaiKhoanNganHang.Trim() == (string)findBy[8] &&
                    //    findBy[9] == null ? true : item.NS_TenChuTaiKhoan.Trim() == (string)findBy[9] &&
                    //    findBy[10] == null ? true : item.NS_HocVan.Trim() == (string)findBy[10] &&
                    //    findBy[11] == null ? true : DateHelper.soSanhNgay(item.NS_NgayVao, (DateTime)findBy[11])
                    //    )
                    //    :
                    //    context.NhanSus.Select(item => item);
                    return (from NhanSu in context.NhanSus orderby NhanSu.NS_Ma select NhanSu).ToPagedList(page, pageSize);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
        public int getCount()
        {
            int count = 0;
            try
            {
                count = context.NhanSus.Count();
            } catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return count;
        }
        public void add(NhanSu nhanSu)
        {

        }
    }
}