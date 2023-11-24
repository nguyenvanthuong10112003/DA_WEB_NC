namespace ProgramWEB.Models.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DangKyCaLam")]
    public partial class DangKyCaLam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DangKyCaLam()
        {
            DuyetDangKies = new HashSet<DuyetDangKy>();
        }

        [Key]
        [StringLength(10)]
        public string DKCL_Ma { get; set; }

        [Column(TypeName = "date")]
        public DateTime DKCL_Ngay { get; set; }

        public DateTime? DKCL_ThoiGianDangKy { get; set; }

        public bool? DKCL_DaDuocDuyet { get; set; }

        [Required]
        [StringLength(10)]
        public string NS_Ma { get; set; }

        [StringLength(10)]
        public string CL_Ma { get; set; }

        public virtual CaLam CaLam { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DuyetDangKy> DuyetDangKies { get; set; }

        public virtual NhanSu NhanSu { get; set; }
    }
}
