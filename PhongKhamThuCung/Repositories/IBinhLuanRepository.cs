using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public interface IBinhLuanRepository
    {
        Task<List<DanhGia>> GetDanhGiaByUserIdAsync(string userId);
        Task<DanhGia> GetByIdAsync(int id);
        Task AddAsync(DanhGia danhGia);
        Task UpdateAsync(DanhGia danhGia);
        Task DeleteAsync(int id);
    }
}
