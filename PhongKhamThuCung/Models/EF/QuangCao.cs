using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhongKhamThuCung.Models.EF
{
    [Table("QuangCao")]
    public class QuangCao
    {
        [Key]
        public int MaQuangCao { get; set; }
        [MaxLength(49, ErrorMessage = "Nội Dung Quảng Cáo Không Được Quá 49 Kí Tự")]
        public string? MoTa { get; set; }
        [MaxLength(99,ErrorMessage = "Nội Dung Quảng Cáo Không Được Quá 99 Kí Tự")]
        public string? NoiDung { get; set; }
        public string? HinhAnh { get; set; }
        public bool Active { get; set; } = true;
        public string? Alias { get; set; }
    }
}
