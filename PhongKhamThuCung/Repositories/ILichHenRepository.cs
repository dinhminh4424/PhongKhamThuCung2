using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public interface ILichHenRepository
    {
        Task<List<LichHen>> GetLichHenByUserIdAsync(string userId);
        Task<LichHen> GetByIdAsync(int id);
        Task AddAsync(LichHen lichHen);
    }
}
