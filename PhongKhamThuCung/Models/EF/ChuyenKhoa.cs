using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhongKhamThuCung.Models.EF
{
    [Table("ChuyenKhoa")]
    public class ChuyenKhoa
    {
        public  ChuyenKhoa()
        {
            this.BacSis = new HashSet<BacSi>();
            this.DichVus = new HashSet<DichVu>();
        }
        [Key]
        public int MaChuyenKhoa { get; set; }
        public string? TenChuyenKhoa { get; set; }
        public string? MoTaNgan { get; set; }
        public string? NoiDung { get; set; }
        public string? HinhAnh { get; set; }
        public bool Active { get; set; } = true;
        public string? Alias { get; set; }

        public virtual ICollection<BacSi> BacSis { get; set; }
        public virtual ICollection<DichVu> DichVus { get; set; }

    }
}
