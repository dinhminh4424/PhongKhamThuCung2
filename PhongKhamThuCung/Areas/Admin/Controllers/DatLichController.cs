using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhongKhamThuCung.Areas.Admin.Models;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using X.PagedList.Extensions;

namespace PhongKhamThuCung.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class DatLichController : Controller
    {
        ApplicationDbContext db;
        UserManager<ApplicationUser> userManager;
        public DatLichController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index(string TimKiem = "", int Page = 1)
        {
            ViewBag.TimKiem = TimKiem;
            IEnumerable<LichHen> ds = await db.LichHens
                                 .Include(u => u.DichVu)
                                 .Include(u => u.ThuCung)
                                 .Include(u => u.ApplicationUser)
                                 .OrderByDescending(d => d.MaLichHen)
                                 .ToListAsync();

            int sodongtren1trang = 5;

            ViewBag.SoLuongDong = 5;
            ViewBag.SoTrang = Page;

            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<LichHen> dsTimKiem = new List<LichHen>();
                foreach (LichHen i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.HoTen);
                    if (r1.ToUpper().Contains(TimKiem.ToUpper()))
                    {
                        dsTimKiem.Add(i);
                        continue;
                    }
                    string r2 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.DichVu.TenDichVu);
                    if (r2.ToUpper().Contains(TimKiem.ToUpper()))
                    {
                        dsTimKiem.Add(i);
                        continue;
                    }
                    string r3 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.ThuCung.TenThuCung);
                    if (r3.ToUpper().Contains(TimKiem.ToUpper()))
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
            List<DichVu> ds = db.DichVus.ToList();
            var dsDV = new SelectList(ds, "MaDichVu", "TenDichVu");
            List<ThuCung> dsTT = db.ThuCungs.ToList();
            var dsLTC = new SelectList(dsTT, "MaThuCung", "TenThuCung");

            ViewBag.DanhSachDichVu = dsDV;
            ViewBag.DanhSachThuCung = dsLTC;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LichHen t,string email)
        {
            List<DichVu> ds = db.DichVus.ToList();
            var dsDV = new SelectList(ds, "MaDichVu", "TenDichVu");
            List<ThuCung> dsTT = db.ThuCungs.ToList();
            var dsLTC = new SelectList(dsTT, "MaThuCung", "TenThuCung");

            ViewBag.DanhSachDichVu = dsDV;
            ViewBag.DanhSachThuCung = dsLTC;

            if (ModelState.IsValid)
            {
                ApplicationUser z = await userManager.FindByEmailAsync(email);

                if (z == null)
                {
                    ModelState.AddModelError("Lỗi", "Không Có Tài Khoản Với Email Này");
                    return View(t);
                }

                t.UserId = z.Id;

                t.TrangThai = "Chưa Được Xét Lịch Hẹn";
               
                db.LichHens.Add(t);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
           
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            LichHen x = await db.LichHens
                                            .Include(u => u.DichVu)
                                            .Include(u => u.ThuCung)
                                            .Include(u => u.ApplicationUser)
                                            .FirstOrDefaultAsync(i => i.MaLichHen == id);
            if (x == null)
            {
                return RedirectToAction("Index");
            }


            List<DichVu> ds = db.DichVus.ToList();
            var dsDV = new SelectList(ds, "MaDichVu", "TenDichVu");
            List<ThuCung> dsTT = db.ThuCungs.ToList();
            var dsLTC = new SelectList(dsTT, "MaThuCung", "TenThuCung");

            ViewBag.DanhSachDichVu = dsDV;
            ViewBag.DanhSachThuCung = dsLTC;
            
            return View(x);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LichHen t, string email)
        {
            List<DichVu> ds = db.DichVus.ToList();
            var dsDV = new SelectList(ds, "MaDichVu", "TenDichVu");
            List<ThuCung> dsTT = db.ThuCungs.ToList();
            var dsLTC = new SelectList(dsTT, "MaThuCung", "TenThuCung");

            ViewBag.DanhSachDichVu = dsDV;
            ViewBag.DanhSachThuCung = dsLTC;

            if (ModelState.IsValid)
            {
                ApplicationUser z = await userManager.FindByEmailAsync(email);

                if (z == null)
                {
                    ModelState.AddModelError("Lỗi", "Không Có Tài Khoản Với Email Này");
                    return View(t);
                }


                LichHen x = await db.LichHens.FirstOrDefaultAsync(i => i.MaLichHen == t.MaLichHen);
                if (x == null)
                {
                    return RedirectToAction("Create");
                }

                x.UserId = z.Id;
                x.MaDichVu = t.MaDichVu;
                x.HoTen = t.HoTen;
                x.GhiChu = t.GhiChu;
                x.MaThuCung = t.MaThuCung;
                x.NgayHen = t.NgayHen;
                x.SoDienThoai = t.SoDienThoai;
                x.TrangThai = t.TrangThai;
                x.Active = t.Active;

               
               

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(t);

        }


        [HttpPost]
        public IActionResult HoatDong(int id)
        {
            try
            {
                LichHen y = db.LichHens.FirstOrDefault(i => i.MaLichHen == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Không tìm thấy lịch hẹn cần chỉnh sửa" });
                }

                // Cập nhật trạng thái
                if (y.TrangThai == "Chưa Được Xét Lịch Hẹn")
                {
                    y.TrangThai = "Đã Lập Lịch Hẹn Thành Công";
                }
                else if (y.TrangThai == "Đã Lập Lịch Hẹn Thành Công")
                {
                    y.TrangThai = "Xét Lịch Hẹn Thất Bại";
                }
                else if (y.TrangThai == "Xét Lịch Hẹn Thất Bại")
                {
                    y.TrangThai = "Chưa Được Xét Lịch Hẹn";
                }

                db.SaveChanges();
                return Json(new { success = true, message = "Thành công", trangThai = y.TrangThai }); // Trả về trạng thái mới
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }


        [HttpPost]
        public IActionResult DaKham(int id)
        {
            try
            {
                LichHen y = db.LichHens.FirstOrDefault(i => i.MaLichHen == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Sửa" });
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
                LichHen y = db.LichHens.FirstOrDefault(i => i.MaLichHen == id);
                if (y == null)
                {
                    return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                }

                db.LichHens.Remove(y);

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
