using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhongKhamThuCung.Models.EF
{
    [Table("DichVu")]
    public class DichVu
    {
        [Key]
        public int MaDichVu { get; set; }
        public string? TenDichVu { get; set; }
        public string? MoTaNgan { get; set; }
        public string? MoTa { get; set; }
        public double? GiaDichVu { get; set; }
        public string? HinhAnh { get; set; }
        public string? Alias { get; set; }
        public bool  Active { get; set; } = true;
        public int? MaChuyenKhoa { get; set; }
        [ForeignKey("MaChuyenKhoa")]
        public virtual ChuyenKhoa? ChuyenKhoa { get; set; }
        public virtual ICollection<LichHen> LichHens { get; set; } = new HashSet<LichHen>();
        
    }
}
