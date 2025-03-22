using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using PhongKhamThuCung.Areas.Admin.Models;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using X.PagedList.Extensions;

namespace PhongKhamThuCung.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ThuCungController : Controller
    {
        private ApplicationDbContext db;

        public ThuCungController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string TimKiem = "", int Page = 1)
        {
            ViewBag.TimKiem = TimKiem;

            int sodongtren1trang = 5;

            IEnumerable<ThuCung> ds = db.ThuCungs.ToList();
            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<ThuCung> dsTimKiem = new List<ThuCung>();
                foreach (ThuCung i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenThuCung);
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
        public async Task<IActionResult> Create(ThuCung t, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                if (HinhAnh != null)
                {
                    t.HinhAnh = await SaveImage(HinhAnh, "ThuCung");
                }
                db.ThuCungs.Add(t);
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
            ThuCung x = await db.ThuCungs.FindAsync(id);
            if (x == null)
            {
                return RedirectToAction("Index");
            }
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ThuCung t, IFormFile? HinhAnhFile)
        {
            if (ModelState.IsValid)
            {
                ThuCung x = await db.ThuCungs.FindAsync(t.MaThuCung);
                if (x == null)
                {
                    return RedirectToAction("Index");
                }

                if (HinhAnhFile != null)
                {
                    x.TenThuCung  = t.TenThuCung;

                    if (!string.IsNullOrEmpty(x.HinhAnh))
                    {
                        DeleteImage(x.HinhAnh, "ThuCung");
                    }

                    x.HinhAnh = await SaveImage(HinhAnhFile, "ThuCung");

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                    //return RedirectToAction("Update");
                }

                x.TenThuCung = t.TenThuCung;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(t);

        }


        [HttpPost]
        

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                ThuCung y = db.ThuCungs.FirstOrDefault(i => i.MaThuCung == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }
                DeleteImage(y.HinhAnh, "ThuCung");
                db.ThuCungs.Remove(y);

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
