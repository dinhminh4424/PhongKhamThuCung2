using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public class LichHenRepository : ILichHenRepository
    {
        private readonly ApplicationDbContext _context;

        public LichHenRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LichHen>> GetLichHenByUserIdAsync(string userId)
        {
            return await _context.LichHens
                .Include(u => u.DichVu)
                .Include(u => u.ThuCung)
                .Include(u => u.ApplicationUser)
                .Where(i => i.UserId == userId)
                .OrderByDescending(d => d.MaLichHen)
                .ToListAsync();
        }

        public async Task<LichHen> GetByIdAsync(int id)
        {
            return await _context.LichHens
                .Include(u => u.DichVu)
                .Include(u => u.ThuCung)
                .Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(i => i.MaLichHen == id);
        }

        public async Task AddAsync(LichHen lichHen)
        {
            _context.LichHens.Add(lichHen);
            await _context.SaveChangesAsync();
        }
    }
}
