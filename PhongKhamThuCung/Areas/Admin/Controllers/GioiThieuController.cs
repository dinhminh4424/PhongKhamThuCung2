using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class GioiThieuController : Controller
    {
        ApplicationDbContext db;

        public GioiThieuController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string TimKiem = "", int Page = 1)
        {
            ViewBag.TimKiem = TimKiem;
            IEnumerable<GioiThieu> ds = await db.GioiThieus.ToListAsync();

            int sodongtren1trang = 5;

            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<GioiThieu> dsTimKiem = new List<GioiThieu>();
                foreach (GioiThieu i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenGioiThieu);
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
        public async Task<IActionResult> Create(GioiThieu t, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                t.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenGioiThieu);
                if (HinhAnh != null)
                {
                    t.HinhAnh = await SaveImage(HinhAnh, "GioiThieu");
                }
                db.GioiThieus.Add(t);
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
            GioiThieu x = await db.GioiThieus.FindAsync(id);
            if (x == null)
            {
                return RedirectToAction("Index");
            }
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GioiThieu t, IFormFile HinhAnhCapNhat)
        {
            if (ModelState.IsValid)
            {
                GioiThieu x = await db.GioiThieus.FindAsync(t.MaGioiThieu);
                if (x == null)
                {
                    return RedirectToAction("Index");
                }

                if (HinhAnhCapNhat != null)
                {
                    x.TenGioiThieu = t.TenGioiThieu;
                    x.NoiDung = t.NoiDung;
                    x.MoTaNgan = t.MoTaNgan;
                    x.Active = t.Active;
                    x.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenGioiThieu);

                    if (!string.IsNullOrEmpty(x.HinhAnh))
                    {
                        DeleteImage(x.HinhAnh, "GioiThieu");
                    }

                    x.HinhAnh = await SaveImage(HinhAnhCapNhat, "GioiThieu");

                    await db.SaveChangesAsync();
                    //return RedirectToAction("Index");
                    return RedirectToAction("Update");
                }

                x.TenGioiThieu = t.TenGioiThieu;
                x.NoiDung = t.NoiDung;
                x.MoTaNgan = t.MoTaNgan;
                x.Active = t.Active;
                x.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenGioiThieu);
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
                GioiThieu y = db.GioiThieus.FirstOrDefault(i => i.MaGioiThieu == id);
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
        public IActionResult Delete(int id)
        {
            try
            {
                GioiThieu y = db.GioiThieus.FirstOrDefault(i => i.MaGioiThieu == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }
                DeleteImage(y.HinhAnh, "TinTuc");
                db.GioiThieus.Remove(y);

                db.SaveChanges();

                return Json(new { success = true, message = "Thành Công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }
    }
}
