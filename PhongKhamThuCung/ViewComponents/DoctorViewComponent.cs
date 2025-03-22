using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.ViewComponents
{
    public class DoctorViewComponent : ViewComponent
    {

        private ApplicationDbContext db;
        public  DoctorViewComponent (ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {

            IEnumerable<BacSi> ds = await db.BacSis.Include(e => e.ChuyenKhoa).Where(i => i.Active == true).ToListAsync();
            return View("DanhSachBacSy",ds);
        }

    }
}