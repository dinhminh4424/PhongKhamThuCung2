using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<ThuCung> ThuCungs { get; set; }
        // Bác sĩ & chuyên khoa
        public DbSet<BacSi> BacSis { get; set; }
        public DbSet<ChuyenKhoa> ChuyenKhoas { get; set; }
        // Lịch hẹn & dịch vụ
        public DbSet<LichHen> LichHens { get; set; }
        public DbSet<DanhGia> DanhGias { get; set; }
        public DbSet<DichVu> DichVus { get; set; }


        public DbSet<UuDai> UuDais { get; set; }

        // Tin tức & quảng cáo
        public DbSet<TinTuc> TinTucs { get; set; }
        public DbSet<LoaiTinTuc> LoaiTinTucs { get; set; }
        public DbSet<QuangCao> QuangCaos { get; set; }
        public DbSet<GioiThieu> GioiThieus { get; set; }
    }
}
