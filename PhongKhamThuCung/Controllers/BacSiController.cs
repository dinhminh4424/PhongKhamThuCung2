using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.Controllers
{
    public class BacSiController : Controller
    {
        private ApplicationDbContext db;
        public  BacSiController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public async Task< IActionResult> Index(string TimKiem="", int IdChuyenKhoa=0)
        {
            
            List<ChuyenKhoa> DanhSachChuyenKhoa = db.ChuyenKhoas.ToList();
            ViewBag.DanhSachChuyenKhoa = DanhSachChuyenKhoa;
            ViewBag.TimKiem = TimKiem;
            IEnumerable<BacSi> ds = await db.BacSis.Include(u => u.ChuyenKhoa).ToListAsync();


            if(string.IsNullOrWhiteSpace(TimKiem))
            {
                if(IdChuyenKhoa==0)
                {
                    return View(ds);
                }
                else
                {
                    ds = ds.Where(i => i.MaChuyenKhoa == IdChuyenKhoa).ToList();
                    string tenCk = db.ChuyenKhoas.FirstOrDefault(i => i.MaChuyenKhoa == IdChuyenKhoa).TenChuyenKhoa;
                    ViewBag.TimKiem = "Chuyên Khoa: "+ tenCk;
                    return View(ds);
                }
            }
            else
            {
                List<BacSi> dsTimKiem = new List<BacSi>();
                TimKiem = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(TimKiem);
                foreach(BacSi i in ds)
                {
                    string r1 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.TenBacSi);
                    if(r1.ToUpper().Contains(TimKiem.ToUpper()))
                    {
                        dsTimKiem.Add(i);
                        continue;
                    }
                    if (i.ChuyenKhoa?.TenChuyenKhoa != null)
                    {
                        string r2 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.ChuyenKhoa?.TenChuyenKhoa);
                        if (r2.ToUpper().Contains(TimKiem.ToUpper()))
                        {
                            dsTimKiem.Add(i);
                            continue;
                        }
                    }
                    
                    if(i.MoTa != null)
                    {
                        string r3 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.MoTa);
                        if (r3.ToUpper().Contains(TimKiem.ToUpper()))
                        {
                            dsTimKiem.Add(i);
                            continue;
                        }
                    }
                    if (i.SDT != null)
                    {
                        string r4 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.SDT);
                        if (r4.ToUpper().Contains(TimKiem.ToUpper()))
                        {
                            dsTimKiem.Add(i);
                            continue;
                        }
                    }
                    if (i.Email != null)
                    {
                        string r5 = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(i.Email);
                        if (r5.ToUpper().Contains(TimKiem.ToUpper()))
                        {
                            dsTimKiem.Add(i);
                            continue;
                        }
                    }

                }
                return View(dsTimKiem);
            }
           
        }

        public async Task<IActionResult> Detail(int id)
        {
            BacSi x = await db.BacSis.Include(u => u.ChuyenKhoa).FirstOrDefaultAsync(i => i.MaBacSi == id);
            if (x == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(x);
        }
    }
}
