using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhongKhamThuCung.Models.EF
{
    [Table("DanhGia")]
    public class DanhGia
    {
        [Key]
        public int MaDanhGia { get; set; }

        public string? NoiDung { get; set; }
        public string? CamXuc { get; set; }

        // Dịch Vụ Tốt
        // Dịch Vụ Cực Tốt
        // Dịch Vụ Thất Vọng

        public DateTime NgayBinhLuan { get; set; } = DateTime.Now;

        public bool Active { get; set; } = false;
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }

        public static implicit operator DanhGia(List<DanhGia> v)
        {
            throw new NotImplementedException();
        }
    }
}
