using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhongKhamThuCung.Areas.Admin.Models;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using System;
using X.PagedList.Extensions;

namespace PhongKhamThuCung.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class LoaiTinTucController : Controller
    {
        ApplicationDbContext db;
        
        public LoaiTinTucController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index(string TimKiem = "", int Page = 1)
        {
            ViewBag.TimKiem = TimKiem;

            int sodongtren1trang = 5;
            IEnumerable<LoaiTinTuc> ds = db.LoaiTinTucs.ToList();

            if (TimKiem.IsNullOrEmpty())
            {
                var dsTrang = ds.ToPagedList(Page, sodongtren1trang);
                return View(dsTrang);
            }
            else
            {
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                List<LoaiTinTuc> dsTimKiem = new List<LoaiTinTuc>();
                foreach (LoaiTinTuc i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenLoai);
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
        public IActionResult Create(LoaiTinTuc ltt)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ltt.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(ltt.TenLoai);
                    db.LoaiTinTucs.Add(ltt);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ltt);
            }
            catch (Exception ex)
            {
                return View(ltt);
            }
        }

        public IActionResult Update(int id)
        {
            LoaiTinTuc x = db.LoaiTinTucs.FirstOrDefault(i => i.MaLoaiTin == id);
            return View(x);
        }

        [HttpPost]
        public IActionResult Update(LoaiTinTuc x)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LoaiTinTuc y = db.LoaiTinTucs.FirstOrDefault(i => i.MaLoaiTin == x.MaLoaiTin);

                    y.Alias = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(x.TenLoai);
                    y.TenLoai = x.TenLoai;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(x);
            }
            catch (Exception ex)
            {
                return View(x);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                    LoaiTinTuc y = db.LoaiTinTucs.FirstOrDefault(i => i.MaLoaiTin == id);
                    if(y==null)
                    {
                        return Json(new { success = false, message = "Ko Có Id Cần Xóa" });
                    }
                    
                    db.LoaiTinTucs.Remove(y);

                    db.SaveChanges();
                    return Json(new { success = true, message = "Thành Công" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: "+ ex.Message });
            }
        }

        [HttpPost]
        public IActionResult HoatDong(int id)
        {

            try
            {
                LoaiTinTuc y = db.LoaiTinTucs.FirstOrDefault(i => i.MaLoaiTin == id);
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
    }


}
