using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.Controllers
{
    public class BaiVietController : Controller
    {
        private ApplicationDbContext db;
        public BaiVietController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            List<TinTuc> DanhSachTinTuc = db.TinTucs.Include(u => u.LoaiTinTuc).ToList();
            return View(DanhSachTinTuc);
        }

        public async Task<IActionResult> Detail(int id)
        {
            TinTuc x = await db.TinTucs.Include(u => u.LoaiTinTuc).FirstOrDefaultAsync(i => i.MaTinTuc == id);
            if (x == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(x);
        }
    }
}
