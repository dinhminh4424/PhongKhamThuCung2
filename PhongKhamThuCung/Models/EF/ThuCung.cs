using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhongKhamThuCung.Models.EF
{
    [Table("ThuCung")]
    public class ThuCung
    {
        [Key]
        public int MaThuCung { get; set; }
        public string TenThuCung { get; set; }
        public string? GiongLoai { get; set; }
        public int? Tuoi { get; set; }
        public string? TinhTrang { get; set; }
        public string? HinhAnh { get; set; }

        //public int? MaKhachHang { get; set; }
        //[ForeignKey("MaKhachHang")]
        //public virtual KhachHang? KhachHang { get; set; }


    }
}
