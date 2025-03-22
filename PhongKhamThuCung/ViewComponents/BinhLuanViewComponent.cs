using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.ViewComponents
{
    public class BinhLuanViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;
        public BinhLuanViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<DanhGia> gioiThieu = await db.DanhGias.Include(u => u.ApplicationUser).Where(i => i.Active == true).ToListAsync();
            return View("DanhSachBinhLuan", gioiThieu);
        }
    }
}
