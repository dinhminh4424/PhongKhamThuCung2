using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public class DichVuRepository : IDichVuRepository
    {
        private readonly ApplicationDbContext _context;

        public DichVuRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DichVu>> GetAllAsync(string keyword)
        {
            var query = _context.DichVus.AsQueryable();

            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(keyword);
                query = query.Where(i => PhongKhamThuCung.BoTro.Filter
                        .ChuyenCoDauThanhKhongDau(i.TenDichVu)
                        .ToUpper()
                        .Contains(keyword.ToUpper()));
            }

            return await query.ToListAsync();
        }

        public async Task<DichVu> GetByIdAsync(int id)
        {
            return await _context.DichVus.FirstOrDefaultAsync(i => i.MaDichVu == id);
        }
    }
}
