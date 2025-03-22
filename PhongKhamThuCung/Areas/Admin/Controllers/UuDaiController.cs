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
    public class UuDaiController : Controller
    {
        private ApplicationDbContext db;

        public UuDaiController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string TimKiem = "", int Page = 1)
        {
            ViewBag.TimKiem = TimKiem;

            int sodongtren1trang = 5;

            IEnumerable<UuDai> ds = db.UuDais.ToList();
            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<UuDai> dsTimKiem = new List<UuDai>();
                foreach (UuDai i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenUuDai);
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
        public async Task<IActionResult> Create(UuDai t, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    t.HinhAnh = await SaveImage(HinhAnh, "UuDai");
                }
                db.UuDais.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(t);
        }
        public async Task<string> SaveImage(IFormFile ImageURL, string subFolder)
        {
            if (ImageURL == null || ImageURL.Length == 0)
            {
                throw new ArgumentException("File không hợp lệ!");
            }

            // Đường dẫn thư mục lưu ảnh trong wwwroot/uploads/tin-tuc
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", subFolder);

            // Tạo thư mục nếu chưa tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            // Tạo tên file duy nhất để tránh trùng lặp
            string fileExtension = Path.GetExtension(ImageURL.FileName);
            string fileName = Path.GetFileNameWithoutExtension(ImageURL.FileName);
            string uniqueFileName = fileName + "_" + Guid.NewGuid().ToString("N") + fileExtension;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file vào thư mục
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ImageURL.CopyToAsync(fileStream);
            }

            // Trả về đường dẫn tương đối để hiển thị ảnh trên web
            return $"/uploads/{subFolder}/{uniqueFileName}";
        }

        public void DeleteImage(string ImageURL, string subFolder)
        {
            if (string.IsNullOrEmpty(ImageURL))
            {
                throw new ArgumentException("Đường dẫn ảnh không hợp lệ!");
            }

            // Lấy đường dẫn tuyệt đối của ảnh trong thư mục wwwroot/uploads/
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", subFolder);
            string filePath = Path.Combine(uploadsFolder, Path.GetFileName(ImageURL));

            // Kiểm tra nếu file tồn tại thì xóa
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            UuDai x = await db.UuDais.FindAsync(id);
            if (x == null)
            {
                return RedirectToAction("Index");
            }
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UuDai t, IFormFile? HinhAnhCapNhat)
        {
            if (ModelState.IsValid)
            {
                UuDai x = await db.UuDais.FindAsync(t.MaUuDai);
                if (x == null)
                {
                    return RedirectToAction("Index");
                }

                if (HinhAnhCapNhat != null)
                {
                    x.TenUuDai = t.TenUuDai;
                    x.MoTa = t.MoTa;
                    x.GiamGia = t.GiamGia;
                    x.Active = t.Active;

                    if (!string.IsNullOrEmpty(x.HinhAnh))
                    {
                        DeleteImage(x.HinhAnh, "UuDai");
                    }

                    x.HinhAnh = await SaveImage(HinhAnhCapNhat, "UuDai");

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                    //return RedirectToAction("Update");
                }
                x.TenUuDai = t.TenUuDai;
                x.MoTa = t.MoTa;
                x.GiamGia = t.GiamGia;
                x.Active = t.Active;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(t);

        }


        [HttpPost]
        public async Task<IActionResult> HoatDong(int id)
        {

            try
            {
                UuDai y = db.UuDais.FirstOrDefault(i => i.MaUuDai == id);
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

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                UuDai y = db.UuDais.FirstOrDefault(i => i.MaUuDai == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }
                DeleteImage(y.HinhAnh, "UuDai");
                db.UuDais.Remove(y);

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
