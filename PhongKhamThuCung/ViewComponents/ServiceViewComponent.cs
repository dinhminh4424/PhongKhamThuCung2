using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        ApplicationDbContext db;

        public ServiceViewComponent(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<DichVu> ds = await db.DichVus.Where(i => i.Active == true).ToListAsync();
            return View("DanhSachDichVu" , ds);
        }
    }
}
