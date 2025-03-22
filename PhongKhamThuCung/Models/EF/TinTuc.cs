using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhongKhamThuCung.Models.EF
{
    [Table("TinTuc")]
    public class TinTuc
    {
        [Key]
        public int MaTinTuc { get; set; }
        public string? TieuDe { get; set; }
        public string? MoTaNgan { get; set; }
        [DataType(DataType.Html)]
        public string? NoiDung { get; set; }
        public string? HinhAnh { get; set; }
        public DateTime? NgayDang { get; set; } = DateTime.Now;
        public string? Alias { get; set; }
        public bool Active { get; set; } = true;
        public int? MaLoaiTin { get; set; }
        [ForeignKey("MaLoaiTin")]
        public virtual LoaiTinTuc? LoaiTinTuc { get; set; }
    }
}
