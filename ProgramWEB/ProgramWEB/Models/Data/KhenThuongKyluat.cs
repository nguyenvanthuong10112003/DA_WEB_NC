namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KhenThuongKyluat")]
    public partial class KhenThuongKyLuat
    {
        [Key]
        [StringLength(10)]
        public string KTKL_Ma { get; set; }

        [Column(TypeName = "ntext")]
        public string KTKL_MoTa { get; set; }

        public DateTime? KTKL_ThoiGian { get; set; }

        [StringLength(20)]
        public string KTKL_HinhThuc { get; set; }

        public double KTKL_SoTien { get; set; }

        [StringLength(10)]
        public string NS_Ma { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
