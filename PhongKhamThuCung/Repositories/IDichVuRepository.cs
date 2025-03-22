using PhongKhamThuCung.Models.EF;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhongKhamThuCung.Repositories
{
    public interface IDichVuRepository
    {
        Task<IEnumerable<DichVu>> GetAllAsync(string keyword);
        Task<DichVu> GetByIdAsync(int id);
    }
}
