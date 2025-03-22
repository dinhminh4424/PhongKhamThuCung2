using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.Controllers
{
    public class DichVuController : Controller
    {
        private ApplicationDbContext db;
        public DichVuController (ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string TimKiem="")
        {
            ViewBag.TimKiem = TimKiem;
            IEnumerable<DichVu> ds = await db.DichVus.ToListAsync();
            if (TimKiem.IsNullOrEmpty())
            {
                return View(ds);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<DichVu> dsDV = new List<DichVu>();
                foreach(DichVu i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenDichVu);
                    if(r1.ToUpper().Contains(TimKiem.ToUpper()))
                    {
                        dsDV.Add(i);
                        continue;
                    }
                }
                return View(dsDV);
            }
        }

        public async Task<IActionResult> Detail(int id)
        {
            DichVu x = await db.DichVus.FirstOrDefaultAsync(i => i.MaDichVu == id);
            if(x==null)
            {
                return RedirectToAction("Index","Home");
            }
            return View(x);
        }
    }
}
