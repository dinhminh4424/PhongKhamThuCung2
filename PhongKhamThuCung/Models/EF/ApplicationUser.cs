using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PhongKhamThuCung.Models.EF
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.LichHens = new HashSet<LichHen>();
            this.DanhGias = new HashSet<DanhGia>();
        }

       [Required]
        public string? HoVaTen { get; set; }
        public string? DiaChi { get; set; }
        public int? Age { get; set; }
        public int Active { get; set; } = 1;

        public virtual ICollection<LichHen> LichHens { get; set; }
        public virtual ICollection<DanhGia> DanhGias { get; set; }
    }
}
