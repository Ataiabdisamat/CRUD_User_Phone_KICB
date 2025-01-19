

using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.DAL.Entities;

namespace PhoneUserKICB.BLL.Interfaces
{
    public interface IPhoneService
    {
        Task<IEnumerable<PhoneDto>> GetAllPhonesAsync();
        Task<IEnumerable<PhoneDto>> GetPhonesByUserIdAsync(int userId);
        Task<PhoneDto> GetPhoneByIdAsync(int id);
        Task CreatePhoneAsync(PhoneDto phone);
        Task UpdatePhoneAsync(PhoneDto phone);
        Task DeletePhoneAsync(int id);

        

    }
}
