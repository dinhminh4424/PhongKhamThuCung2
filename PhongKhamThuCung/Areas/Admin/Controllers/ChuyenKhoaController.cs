using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhongKhamThuCung.Areas.Admin.Models;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using X.PagedList.Extensions;

namespace PhongKhamThuCung.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ChuyenKhoaController : Controller
    {
        ApplicationDbContext db;

        public ChuyenKhoaController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string TimKiem = "", int Page = 1)
        {
            int sodongtren1trang = 5;
            ViewBag.TimKiem = TimKiem;

            IEnumerable<ChuyenKhoa> ds = await db.ChuyenKhoas.ToListAsync();
            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<ChuyenKhoa> dsTimKiem = new List<ChuyenKhoa>();
                foreach (ChuyenKhoa i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenChuyenKhoa);
                    if (r1.ToUpper().Contains(TimKiem.ToUpper()))
                    {
                        dsTimKiem.Add(i);
                        continue;
                    }
                }
                var dsTrang = dsTimKiem.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task< IActionResult> Create(ChuyenKhoa ltt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ltt.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(ltt.TenChuyenKhoa);
                    await db.ChuyenKhoas.AddAsync(ltt);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(ltt);
            }
            catch (Exception ex)
            {
                return View(ltt);
            }
        }

        public async Task<IActionResult>  Update(int id)
        {
            ChuyenKhoa x = await db.ChuyenKhoas.FirstOrDefaultAsync(i => i.MaChuyenKhoa == id);
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult>  Update(ChuyenKhoa x)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ChuyenKhoa y = await db.ChuyenKhoas.FirstOrDefaultAsync(i => i.MaChuyenKhoa == x.MaChuyenKhoa);

                    y.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(x.TenChuyenKhoa);
                    y.TenChuyenKhoa = x.TenChuyenKhoa;
                    y.MoTaNgan = x.MoTaNgan;
                    y.NoiDung = x.NoiDung;
                    y.Active = x.Active;

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(x);
            }
            catch (Exception ex)
            {
                return View(x);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ChuyenKhoa y = await db.ChuyenKhoas.FirstOrDefaultAsync(i => i.MaChuyenKhoa == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }

                db.ChuyenKhoas.Remove(y);

                await db.SaveChangesAsync();
                return Json(new { success = true, message = "Thành Công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> HoatDong(int id)
        {

            try
            {
                ChuyenKhoa y = await db.ChuyenKhoas.FirstOrDefaultAsync(i => i.MaChuyenKhoa == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }

                y.Active = !y.Active;

                await db.SaveChangesAsync();
                return Json(new { success = true, message = "Thành Công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }
    }
}
