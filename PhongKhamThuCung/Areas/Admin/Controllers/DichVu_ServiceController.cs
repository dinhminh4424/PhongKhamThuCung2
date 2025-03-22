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
    public class DichVu_ServiceController : Controller
    {
        private ApplicationDbContext db;

        public DichVu_ServiceController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task<IActionResult> Index(string TimKiem = "", int Page = 1)
        {
            int sodongtren1trang = 5;
            ViewBag.TimKiem = TimKiem;

            IEnumerable<DichVu> ds = db.DichVus.Include(u => u.ChuyenKhoa).Include(i => i.LichHens).ToList();
            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<DichVu> dsTimKiem = new List<DichVu>();
                foreach (DichVu i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenDichVu);
                    if (r1.ToUpper().Contains(TimKiem.ToUpper()))
                    {
                        dsTimKiem.Add(i);
                        continue;
                    }
                    string r2 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.ChuyenKhoa?.TenChuyenKhoa);
                    if (r2.ToUpper().Contains(TimKiem.ToUpper()))
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
            List<ChuyenKhoa> ds = db.ChuyenKhoas.ToList();
            ViewBag.DanhSachChuyenKhoa = new SelectList(ds, "MaChuyenKhoa", "TenChuyenKhoa");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DichVu t, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                t.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenDichVu);
                if (HinhAnh != null)
                {
                    t.HinhAnh = await SaveImage(HinhAnh, "DichVu_Service");
                }
                db.DichVus.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            List<ChuyenKhoa> ds = db.ChuyenKhoas.ToList();
            ViewBag.DanhSachChuyenKhoa = new SelectList(ds, "Id", "TenChuyenKhoa");
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
            DichVu x = await db.DichVus.FirstOrDefaultAsync(i => i.MaDichVu == id);
            if (x == null)
            {
                return RedirectToAction("Index");
            }
            List<ChuyenKhoa> ds = db.ChuyenKhoas.ToList();
            ViewBag.DanhSachChuyenKhoa = new SelectList(ds, "MaChuyenKhoa", "TenChuyenKhoa");
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DichVu t, IFormFile? HinhAnhCapNhat)
        {
            if (ModelState.IsValid)
            {
                DichVu x = await db.DichVus.FirstOrDefaultAsync(i => i.MaDichVu == t.MaDichVu);
                if (x == null)
                {
                    return RedirectToAction("Index");
                }

                if (HinhAnhCapNhat != null)
                {
                    x.TenDichVu = t.TenDichVu;
                    x.MoTa = t.MoTa;
                    x.GiaDichVu = t.GiaDichVu;
                    x.Active = t.Active;
                    x.MoTaNgan = t.MoTaNgan;
                    x.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenDichVu);
                    x.MaChuyenKhoa = t.MaChuyenKhoa;
                    if (!string.IsNullOrEmpty(x.HinhAnh))
                    {
                        DeleteImage(x.HinhAnh, "DichVu_Service");
                    }

                    x.HinhAnh = await SaveImage(HinhAnhCapNhat, "DichVu_Service");

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                    //return RedirectToAction("Update");
                }
                x.TenDichVu = t.TenDichVu;
                x.MoTaNgan = t.MoTaNgan;
                x.MoTa = t.MoTa;
                x.GiaDichVu = t.GiaDichVu;
                x.Active = t.Active;
                x.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenDichVu);
                x.MaChuyenKhoa = t.MaChuyenKhoa;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            List<ChuyenKhoa> ds = db.ChuyenKhoas.ToList();
            ViewBag.DanhSachChuyenKhoa = new SelectList(ds, "MaChuyenKhoa", "TenChuyenKhoa");
            return View(t);

        }


        [HttpPost]
        public async Task<IActionResult> HoatDong(int id)
        {

            try
            {
                DichVu y = db.DichVus.FirstOrDefault(i => i.MaDichVu == id);
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
                DichVu y = db.DichVus.FirstOrDefault(i => i.MaDichVu == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }
                DeleteImage(y.HinhAnh, "DichVu_Service");
                db.DichVus.Remove(y);

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
