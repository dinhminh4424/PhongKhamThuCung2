using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.ViewComponents
{
    public class PricingPlanViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext db;
        public PricingPlanViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<DichVu> ds = await db.DichVus.Where(i => i.Active == true).ToListAsync();
            return View("KeHoachGia",ds);
        }
    }
}
