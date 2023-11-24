namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKyNghiLam")]
    public partial class DangKyNghiLam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DangKyNghiLam()
        {
            DuyetDangKies = new HashSet<DuyetDangKy>();
        }

        [Key]
        [StringLength(10)]
        public string DKNL_Ma { get; set; }

        [Column(TypeName = "date")]
        public DateTime DKNL_Ngay { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DKNL_ThoiGianDangKy { get; set; }

        public bool? DKNL_NghiCoPhep { get; set; }

        [Column(TypeName = "ntext")]
        public string DKNL_LyDoNghi { get; set; }

        public bool? DKNL_DaDuocDuyet { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        [StringLength(10)]
        public string DDK_Ma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DuyetDangKy> DuyetDangKies { get; set; }

        public virtual DuyetDangKy DuyetDangKy { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
