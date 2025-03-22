using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.ViewComponents
{
    public class AboutUsViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;
        public AboutUsViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            GioiThieu gioiThieu = await  db.GioiThieus.FirstOrDefaultAsync(i=> i.Active == true);
            return View("ThongTinGioiThieu", gioiThieu);
        }
    }
}
