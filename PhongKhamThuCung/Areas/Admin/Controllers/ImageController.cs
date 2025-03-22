using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhongKhamThuCung.Areas.Admin.Models;

namespace PhongKhamThuCung.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ImageController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Vui lòng chọn ảnh.");
            }

            // Kiểm tra định dạng ảnh hợp lệ (jpg, png, gif)
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (!Array.Exists(allowedExtensions, ext => ext == fileExtension))
            {
                return BadRequest("Chỉ chấp nhận các file ảnh (.jpg, .jpeg, .png, .gif)");
            }

            // Tạo đường dẫn lưu ảnh
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu ảnh vào thư mục wwwroot/uploads
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn ảnh
            return Json(new { fileName = uniqueFileName, url = "/uploads/" + uniqueFileName });
        }
    }
}
