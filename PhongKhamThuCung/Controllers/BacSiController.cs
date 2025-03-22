using Microsoft.AspNetCore.Mvc;
using PhongKhamThuCung.Repositories;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Controllers
{
    public class BacSiController : Controller
    {
        private readonly IBacSiRepository _bacSiRepository;

        public BacSiController(IBacSiRepository bacSiRepository)
        {
            _bacSiRepository = bacSiRepository;
        }

        public async Task<IActionResult> Index(string TimKiem = "", int IdChuyenKhoa = 0)
        {
            var ds = await _bacSiRepository.SearchAsync(TimKiem, IdChuyenKhoa);
            return View(ds);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var bacSi = await _bacSiRepository.GetByIdAsync(id);
            if (bacSi == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(bacSi);
        }
    }
}
