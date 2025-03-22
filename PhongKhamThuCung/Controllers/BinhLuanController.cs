using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.Controllers
{
    public class BinhLuanController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> userManager;
        public BinhLuanController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            this.db = db;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ThemBinhLuan(string NoiDung, string CamXuc)
        {
            if(User.Identity.IsAuthenticated)
            {

                ApplicationUser x = await userManager.GetUserAsync(User);
                if (x == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                else
                {
                    DanhGia dg = new DanhGia(); 
                    dg.NoiDung = NoiDung;
                    dg.UserId = x.Id;
                    dg.CamXuc = CamXuc;

                    db.DanhGias.Add(dg);
                    await db.SaveChangesAsync();

                    return RedirectToPage("/Account/Manage/Index", new { area = "Identity" });
                }

            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });

        }

        public async Task<IActionResult> DanhSachBinhLuan()
        {
            if (User.Identity.IsAuthenticated)
            {

                ApplicationUser x = await userManager.GetUserAsync(User);
                if (x == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                else
                {
                    List<DanhGia> ds = await db.DanhGias
                                                            .Include(u => u.ApplicationUser)
                                                            .Where(i => i.UserId == x.Id)
                                                            .ToListAsync();
                    return View(ds);
                }

            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        public async Task<IActionResult> ChinhSuaBinhLuan (int id)
        {
            if (User.Identity.IsAuthenticated)
            {

                ApplicationUser x = await userManager.GetUserAsync(User);
                if (x == null)
                {
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }
                else
                {
                    DanhGia ds = await db.DanhGias
                                                            .Include(u => u.ApplicationUser)
                                                            .FirstOrDefaultAsync(i => i.UserId == x.Id && i.MaDanhGia == id);
                    return View(ds);
                }

            }
            return RedirectToAction("Login", "Account", new { area = "Identity" });
        }

        [HttpPost]
        public async Task<IActionResult> ChinhSuaBinhLuan(DanhGia danhGia)
        {

            if (ModelState.IsValid)
            {
                DanhGia x = await db.DanhGias
                                            .Include(u => u.ApplicationUser)
                                            .FirstOrDefaultAsync(i =>  i.MaDanhGia == danhGia.MaDanhGia);

                x.NoiDung = danhGia.NoiDung;
                x.CamXuc = danhGia.CamXuc;

                db.SaveChanges();
                return RedirectToAction(nameof(DanhSachBinhLuan));
            }
            return View(danhGia);
        }

        // Xóa bình luận

        public IActionResult Delete(int id)
        {
            var danhGia = db.DanhGias.Find(id);
            if (danhGia == null)
            {
                return NotFound();
            }

            db.DanhGias.Remove(danhGia);
            db.SaveChanges();
            return RedirectToAction(nameof(DanhSachBinhLuan));
        }
    }
}
