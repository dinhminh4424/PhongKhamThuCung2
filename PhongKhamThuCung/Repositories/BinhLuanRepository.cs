using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public class BinhLuanRepository : IBinhLuanRepository
    {
        private readonly ApplicationDbContext _context;

        public BinhLuanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DanhGia>> GetDanhGiaByUserIdAsync(string userId)
        {
            return await _context.DanhGias
                .Include(u => u.ApplicationUser)
                .Where(i => i.UserId == userId)
                .ToListAsync();
        }

        public async Task<DanhGia> GetByIdAsync(int id)
        {
            return await _context.DanhGias
                .Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(i => i.MaDanhGia == id);
        }

        public async Task AddAsync(DanhGia danhGia)
        {
            _context.DanhGias.Add(danhGia);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DanhGia danhGia)
        {
            _context.DanhGias.Update(danhGia);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var danhGia = await _context.DanhGias.FindAsync(id);
            if (danhGia != null)
            {
                _context.DanhGias.Remove(danhGia);
                await _context.SaveChangesAsync();
            }
        }
    }
}
