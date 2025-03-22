using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.ViewComponents
{
    public class UuDaiViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;
        public UuDaiViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<UuDai> ds = await db.UuDais.Where(i => i.Active ==  true).ToListAsync();
            return View("UuDaiDanhSach",ds);
        }
    }
}
