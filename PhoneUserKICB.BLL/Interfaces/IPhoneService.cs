

using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.DAL.Entities;

namespace PhoneUserKICB.BLL.Interfaces
{
    /// <summary>
    /// Inerface for phone services
    /// </summary>
    public interface IPhoneService
    {
        Task<IEnumerable<PhoneDto>> GetAllPhonesAsync();        
        Task<PhoneDto> GetPhoneByIdAsync(int id);
        Task CreatePhoneAsync(PhoneDto phone);
        Task UpdatePhoneAsync(PhoneDto phone);
        Task DeletePhoneAsync(int id);

        

    }
}
