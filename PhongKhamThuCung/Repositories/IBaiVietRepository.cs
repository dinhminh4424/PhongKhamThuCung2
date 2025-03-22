using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public interface IBaiVietRepository
    {
        Task<IEnumerable<TinTuc>> GetAllAsync();
        Task<TinTuc> GetByIdAsync(int id);
    }
}
