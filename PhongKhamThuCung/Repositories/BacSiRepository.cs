using Microsoft.EntityFrameworkCore;
using PhongKhamThuCung.Data;
using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public class BacSiRepository : IBacSiRepository
    {
        private readonly ApplicationDbContext _context;

        public BacSiRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BacSi>> GetAllAsync()
        {
            return await _context.BacSis.Include(b => b.ChuyenKhoa).ToListAsync();
        }

        public async Task<BacSi> GetByIdAsync(int id)
        {
            return await _context.BacSis.Include(b => b.ChuyenKhoa)
                                        .FirstOrDefaultAsync(b => b.MaBacSi == id);
        }

        public async Task<IEnumerable<BacSi>> SearchAsync(string keyword, int idChuyenKhoa)
        {
            var query = _context.BacSis.Include(b => b.ChuyenKhoa).AsQueryable();

            if (idChuyenKhoa > 0)
            {
                query = query.Where(b => b.MaChuyenKhoa == idChuyenKhoa);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(keyword);
                query = query.Where(b => PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(b.TenBacSi).ToUpper().Contains(keyword.ToUpper()) ||
                                         (b.ChuyenKhoa != null && PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(b.ChuyenKhoa.TenChuyenKhoa).ToUpper().Contains(keyword.ToUpper())) ||
                                         (b.MoTa != null && PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(b.MoTa).ToUpper().Contains(keyword.ToUpper())) ||
                                         (b.SDT != null && PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(b.SDT).ToUpper().Contains(keyword.ToUpper())) ||
                                         (b.Email != null && PhongKhamThuCung.BoTro.Filter.ChuyenCoDauThanhKhongDau(b.Email).ToUpper().Contains(keyword.ToUpper())));
            }

            return await query.ToListAsync();
        }
    }
}
