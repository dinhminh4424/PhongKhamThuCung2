using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.ViewComponents
{
    public class QuangCaoViewComponent : ViewComponent
    {
        ApplicationDbContext db;

        public QuangCaoViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<QuangCao> ds = await db.QuangCaos.Where(i => i.Active == true).ToListAsync();
            return View("QuangCao", ds);
        }
    }
}
