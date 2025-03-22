using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhongKhamThuCung.Models.EF
{
    [Table("GioiThieu")]
    public class GioiThieu
    {
        [Key]
        public int MaGioiThieu { get; set; }
        public string TenGioiThieu { get; set; }
        public string? HinhAnh { get; set; }
        public string? Alias { get; set; }
        public string? NoiDung { get; set; }
        public string? MoTaNgan { get; set; }

        public bool Active { get; set; } = true;
    }
}
