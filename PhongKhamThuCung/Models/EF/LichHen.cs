using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PhongKhamThuCung.Models.EF
{
    [Table("LichHen")]
    public class LichHen
    {
        [Key]
        public int MaLichHen { get; set; }
        public DateTime? NgayHen { get; set; }
        public string TrangThai { get; set; } = "Chưa Được Xét Lịch Hẹn";

        public bool Active { get; set; } = false;

        /// <summary>
        /// Đã Lập Lịch Hẹn Thành Công
        /// Chưa Được Xét Lịch Hẹn
        /// Xét Lịch Hẹn Thất Bại
        /// </summary>
        public string SoDienThoai { get; set; }
        public string HoTen {  get; set; }
        public string? GhiChu { get; set; }
        public int? MaThuCung { get; set; }
        [ForeignKey("MaThuCung")]
        public virtual ThuCung? ThuCung { get; set; }
        public int? MaDichVu { get; set; }
        [ForeignKey("MaDichVu")]
        public virtual DichVu? DichVu { get; set; }

        //public int? MaBacSi { get; set; }
        //public virtual BacSi? BacSi { get; set; }
         

        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser? ApplicationUser { get; set; }
    }
}
