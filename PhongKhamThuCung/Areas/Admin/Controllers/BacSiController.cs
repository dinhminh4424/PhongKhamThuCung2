﻿using Microsoft.AspNetCore.Authorization;
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
    public class BacSiController : Controller
    {
        private ApplicationDbContext db;
        public BacSiController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index(string TimKiem="", int Page=1)
        {
            ViewBag.TimKiem = TimKiem;
            IEnumerable<BacSi> ds = await db.BacSis.Include(t => t.ChuyenKhoa).ToListAsync();

            int sodongtren1trang = 5;

            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<BacSi> dsTimKiem = new List<BacSi>();
                foreach (BacSi i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenBacSi);
                    if (r1.ToUpper().Contains(TimKiem.ToUpper()))
                    {
                        dsTimKiem.Add(i);
                        continue;
                    }
                    string r2 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.ChuyenKhoa.TenChuyenKhoa);
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
            var dsLTT = new SelectList(ds, "MaChuyenKhoa", "TenChuyenKhoa");
            ViewBag.DanhSachChuyenKhoa = dsLTT;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BacSi t, IFormFile HinhAnh)
        {
            if (ModelState.IsValid)
            {
                t.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenBacSi);
                if (HinhAnh != null)
                {

                    t.HinhAnh = await SaveImage(HinhAnh, "BacSi");
                }
                db.BacSis.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            List<ChuyenKhoa> ds = db.ChuyenKhoas.ToList();
            var dsLTT = new SelectList(ds, "MaChuyenKhoa", "TenChuyenKhoa");
            ViewBag.DanhSachChuyenKhoa = dsLTT;
            return View();
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
            BacSi x = await db.BacSis.FindAsync(id);
            if (x == null)
            {
                return RedirectToAction("Index");
            }
            List<ChuyenKhoa> ds = db.ChuyenKhoas.ToList();
            var dsLTT = new SelectList(ds, "MaChuyenKhoa", "TenChuyenKhoa");
            ViewBag.DanhSachChuyenKhoa = dsLTT;
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BacSi t, IFormFile? HinhAnhFile)
        {
            if (ModelState.IsValid)
            {
                BacSi x = await db.BacSis.FirstOrDefaultAsync(i => i.MaBacSi == t.MaBacSi);
                if (x == null)
                {
                    return RedirectToAction("Create");
                }

                if (HinhAnhFile != null)
                {

                    DeleteImage(x.HinhAnh, "BacSi");

                    x.HinhAnh = await SaveImage(HinhAnhFile, "BacSi");

                }

                x.MaChuyenKhoa = t.MaChuyenKhoa;
                x.TenBacSi = t.TenBacSi;
                x.MoTa = t.MoTa;
                x.Email = t.Email;
                x.DiaChi = t.DiaChi;
                x.SDT = t.SDT;
                x.Facebook = t.Facebook;
                x.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(t.TenBacSi);
                x.Active = t.Active;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            List<ChuyenKhoa> ds = db.ChuyenKhoas.ToList();
            var dsLTT = new SelectList(ds, "MaChuyenKhoa", "TenChuyenKhoa");
            ViewBag.DanhSachChuyenKhoa = dsLTT;
            return View(t);

        }


        [HttpPost]
        public IActionResult HoatDong(int id)
        {

            try
            {
                BacSi y = db.BacSis.FirstOrDefault(i => i.MaBacSi == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }

                y.Active = !y.Active;

                db.SaveChanges();
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
                BacSi y = db.BacSis.FirstOrDefault(i => i.MaBacSi == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }
                DeleteImage(y.HinhAnh, "BacSi");
                db.BacSis.Remove(y);

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
