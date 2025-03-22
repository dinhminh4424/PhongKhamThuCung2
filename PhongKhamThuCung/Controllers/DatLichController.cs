using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using PhongKhamThuCung.Repositories;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Controllers
{
    public class DatLichController : Controller
    {
        private readonly ILichHenRepository _lichHenRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;

        public DatLichController(ILichHenRepository lichHenRepository, UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _lichHenRepository = lichHenRepository;
            _userManager = userManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var user = await _userManager.GetUserAsync(User);
            ViewBag.DichVu = new SelectList(_db.DichVus.Where(i => i.Active == true).ToList(), "MaDichVu", "TenDichVu");
            ViewBag.ThuCung = new SelectList(_db.ThuCungs.ToList(), "MaThuCung", "TenThuCung");
            ViewBag.SDT = user.PhoneNumber;
            ViewBag.DiaChi = user.DiaChi;
            ViewBag.Name = user.HoVaTen;
            ViewBag.NguoiDung = user;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LichHen lh)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            if (ModelState.IsValid)
            {
                lh.TrangThai = "Chưa Được Xét Lịch Hẹn";
                await _lichHenRepository.AddAsync(lh);
                return RedirectToAction("ChiTietLichHen", "DatLich", new { id = lh.MaLichHen });
            }

            var user = await _userManager.GetUserAsync(User);
            ViewBag.DichVu = new SelectList(_db.DichVus.Where(i => i.Active == true).ToList(), "MaDichVu", "TenDichVu");
            ViewBag.SDT = lh.SoDienThoai;
            ViewBag.HoVaTen = lh.HoTen;
            ViewBag.ThuCung = new SelectList(_db.ThuCungs.ToList(), "MaThuCung", "TenThuCung");
            ViewBag.NguoiDung = user;
            return View();
        }

        public async Task<IActionResult> ChiTietLichHen(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var lichHen = await _lichHenRepository.GetByIdAsync(id);
            return View(lichHen);
        }

        public async Task<IActionResult> DanhSachLichHen()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var user = await _userManager.GetUserAsync(User);
            var danhSachLichHen = await _lichHenRepository.GetLichHenByUserIdAsync(user.Id);
            return View(danhSachLichHen);
        }
    }
}
