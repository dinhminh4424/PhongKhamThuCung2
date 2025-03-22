using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhongKhamThuCung.Models.EF;
using PhongKhamThuCung.Repositories;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Controllers
{
    public class BinhLuanController : Controller
    {
        private readonly IBinhLuanRepository _binhLuanRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public BinhLuanController(IBinhLuanRepository binhLuanRepository, UserManager<ApplicationUser> userManager)
        {
            _binhLuanRepository = binhLuanRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ThemBinhLuan(string NoiDung, string CamXuc)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                var danhGia = new DanhGia
                {
                    NoiDung = NoiDung,
                    UserId = user.Id,
                    CamXuc = CamXuc
                };

                await _binhLuanRepository.AddAsync(danhGia);
                return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
            }

            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        public async Task<IActionResult> DanhSachBinhLuan()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var danhSachBinhLuan = await _binhLuanRepository.GetDanhGiaByUserIdAsync(user.Id);
            return View(danhSachBinhLuan);
        }

        public async Task<IActionResult> ChinhSuaBinhLuan(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            var danhGia = await _binhLuanRepository.GetByIdAsync(id);
            if (danhGia == null || danhGia.UserId != user.Id)
            {
                return NotFound();
            }

            return View(danhGia);
        }

        [HttpPost]
        public async Task<IActionResult> ChinhSuaBinhLuan(DanhGia danhGia)
        {
            if (!ModelState.IsValid)
            {
                return View(danhGia);
            }

            var existingDanhGia = await _binhLuanRepository.GetByIdAsync(danhGia.MaDanhGia);
            if (existingDanhGia == null)
            {
                return NotFound();
            }

            existingDanhGia.NoiDung = danhGia.NoiDung;
            existingDanhGia.CamXuc = danhGia.CamXuc;

            await _binhLuanRepository.UpdateAsync(existingDanhGia);
            return RedirectToAction(nameof(DanhSachBinhLuan));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _binhLuanRepository.DeleteAsync(id);
            return RedirectToAction(nameof(DanhSachBinhLuan));
        }
    }
}
