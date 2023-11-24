namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LichSuHanhDong")]
    public partial class LichSuHanhDong
    {
        [Key]
        [StringLength(10)]
        public string LSHD_Ma { get; set; }

        [StringLength(30)]
        public string LSHD_TieuDe { get; set; }

        [Column(TypeName = "ntext")]
        public string LSHD_MoTa { get; set; }

        public DateTime? LSHD_ThoiGian { get; set; }

        [StringLength(10)]
        public string NS_Ma { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
