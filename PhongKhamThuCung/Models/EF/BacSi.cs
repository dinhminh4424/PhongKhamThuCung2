using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhongKhamThuCung.Models.EF
{
    [Table("BacSi")]
    public class BacSi
    {
        [Key]
        public int MaBacSi { get; set; }
        public string? TenBacSi { get; set; }
        public string? MoTa { get; set; }
        public string? Email { get; set; }
        public string? DiaChi { get; set; }
        public string? SDT { get; set; }
        public string? Facebook { get; set; }
        public string? HinhAnh { get; set; }
        public string? Alias { get; set; }
        public bool Active { get; set; } = true;
        public int? MaChuyenKhoa { get; set; }
        [ForeignKey("MaChuyenKhoa")]
        public virtual ChuyenKhoa? ChuyenKhoa { get; set; }
        //public virtual ICollection<LichHen> LichHen { get; set; } = new List<LichHen>();

    }
}
