using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.Controllers
{
    public class DatLichController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        public DatLichController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                ApplicationUser x = await userManager.GetUserAsync(User);

                ViewBag.DichVu = new SelectList(db.DichVus.Where(i => i.Active == true).ToList(),"MaDichVu","TenDichVu");
                ViewBag.SDT = x.PhoneNumber;
                ViewBag.DiaChi = x.DiaChi;
                ViewBag.Name = x.HoVaTen;
                ViewBag.ThuCung = new SelectList(  db.ThuCungs.ToList(),"MaThuCung","TenThuCung");
                ViewBag.NguoiDung = x;
                return View();
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        [HttpPost]
        public async Task<IActionResult> Index(LichHen lh)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    lh.TrangThai = "Chưa Được Xét Lịch Hẹn";
                    db.LichHens.Add(lh);
                    await db.SaveChangesAsync();

                    return RedirectToAction("ChiTietLichHen","DatLich", new { id = lh.MaLichHen });
                    
                }
                ApplicationUser x = await userManager.GetUserAsync(User);
                ViewBag.DichVu = new SelectList(db.DichVus.Where(i => i.Active == true).ToList(), "MaDichVu", "TenDichVu");
                ViewBag.SDT = lh.SoDienThoai;
                ViewBag.HoVaTen = lh.HoTen;
                ViewBag.ThuCung = new SelectList(db.ThuCungs.ToList(), "MaThuCung", "TenThuCung");
                ViewBag.NguoiDung = x;
                return View();
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        public async Task<IActionResult> ChiTietLichHen(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                LichHen x = db.LichHens
                    .Include(u => u.DichVu)
                    .Include(u => u.ThuCung)
                    .Include(u => u.ApplicationUser)
                    .FirstOrDefault(i => i.MaLichHen == id);
                return View(x);
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        public async Task<IActionResult> DanhSachLichHen()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationUser x = await userManager.GetUserAsync(User);
                List<LichHen> ds = db.LichHens
                     .Include(u => u.DichVu)
                     .Include(u => u.ThuCung)
                     .Include(u => u.ApplicationUser)
                     .OrderByDescending(d => d.MaLichHen)
                     .Where(i => i.UserId == x.Id)
                     .ToList();

                return View(ds);
            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }
    }
}
