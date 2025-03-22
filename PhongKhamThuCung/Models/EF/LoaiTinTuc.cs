using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhongKhamThuCung.Models.EF
{
    [Table("LoaiTinTuc")]
    public class LoaiTinTuc
    {
        [Key]
        public int MaLoaiTin { get; set; }
        public string? TenLoai { get; set; }
        public bool Active { get; set; } = true;
        public string? Alias { get; set; }

        public virtual ICollection<TinTuc> TinTucs { get; set; } = new HashSet<TinTuc>();

    }
}
