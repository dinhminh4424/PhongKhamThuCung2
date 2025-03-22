using Microsoft.AspNetCore.Mvc;
using PhongKhamThuCung.Repositories;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Controllers
{
    public class DichVuController : Controller
    {
        private readonly IDichVuRepository _dichVuRepository;

        public DichVuController(IDichVuRepository dichVuRepository)
        {
            _dichVuRepository = dichVuRepository;
        }

        public async Task<IActionResult> Index(string TimKiem = "")
        {
            ViewBag.TimKiem = TimKiem;
            var ds = await _dichVuRepository.GetAllAsync(TimKiem);
            return View(ds);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var dichVu = await _dichVuRepository.GetByIdAsync(id);
            if (dichVu == null)
            {
                return NotFound();
            }
            return View(dichVu);
        }
    }
}
