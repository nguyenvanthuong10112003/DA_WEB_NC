namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DuyetDangKy")]
    public partial class DuyetDangKy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DuyetDangKy()
        {
            DangKyNghiLams = new HashSet<DangKyNghiLam>();
        }

        [Key]
        [StringLength(10)]
        public string DDK_Ma { get; set; }

        public DateTime? DDK_ThoiGian { get; set; }

        [StringLength(10)]
        public string DKCL_Ma { get; set; }

        [StringLength(10)]
        public string DKNL_Ma { get; set; }

        [StringLength(10)]
        public string NS_Ma { get; set; }

        public virtual DangKyCaLam DangKyCaLam { get; set; }

        public virtual DangKyNghiLam DangKyNghiLam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DangKyNghiLam> DangKyNghiLams { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
