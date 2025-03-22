using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhongKhamThuCung.Models.EF
{
    [Table("UuDai")]
    public class UuDai
    {
        [Key]
        public int MaUuDai { get; set; }
        public string TenUuDai { get; set; }
        public string? MoTa { get; set; }
        public string? HinhAnh { get; set; }
        public double? GiamGia { get; set; }
        public bool Active { get; set; } = true;
    }
}
