using Microsoft.AspNetCore.Mvc;
using PhongKhamThuCung.Repositories;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Controllers
{
    public class BaiVietController : Controller
    {
        private readonly IBaiVietRepository _baiVietRepository;

        public BaiVietController(IBaiVietRepository baiVietRepository)
        {
            _baiVietRepository = baiVietRepository;
        }

        public async Task<IActionResult> Index()
        {
            var danhSachTinTuc = await _baiVietRepository.GetAllAsync();
            return View(danhSachTinTuc);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var tinTuc = await _baiVietRepository.GetByIdAsync(id);
            if (tinTuc == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(tinTuc);
        }
    }
}
