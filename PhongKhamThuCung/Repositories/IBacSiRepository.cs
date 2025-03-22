using PhongKhamThuCung.Models.EF;

namespace PhongKhamThuCung.Repositories
{
    public interface IBacSiRepository
    {
        Task<IEnumerable<BacSi>> GetAllAsync();
        Task<BacSi> GetByIdAsync(int id);
        Task<IEnumerable<BacSi>> SearchAsync(string keyword, int idChuyenKhoa);
    }
}
