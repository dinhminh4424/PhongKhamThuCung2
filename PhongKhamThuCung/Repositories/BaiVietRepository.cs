using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public class BaiVietRepository : IBaiVietRepository
    {
        private readonly ApplicationDbContext _context;

        public BaiVietRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TinTuc>> GetAllAsync()
        {
            return await _context.TinTucs.Include(t => t.LoaiTinTuc).ToListAsync();
        }

        public async Task<TinTuc> GetByIdAsync(int id)
        {
            return await _context.TinTucs.Include(t => t.LoaiTinTuc)
                                         .FirstOrDefaultAsync(t => t.MaTinTuc == id);
        }
    }
}
