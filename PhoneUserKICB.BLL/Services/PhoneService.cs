using PhoneUserKICB.BLL.DTO;
using PhoneUserKICB.BLL.Interfaces;
using PhoneUserKICB.BLL.Validations;
using PhoneUserKICB.DAL.Entities;
using PhoneUserKICB.DAL.Repository;

namespace PhoneUserKICB.BLL.Services
{
    /// <summary>
    /// Service for phone
    /// </summary>
    public class PhoneService : IPhoneService
    {
        private readonly IRepository<Phone> _phoneRepository;

        public PhoneService(IRepository<Phone> phoneRepository)
        {
            _phoneRepository = phoneRepository;
        }

        public async Task<IEnumerable<PhoneDto>> GetAllPhonesAsync()
        {
            var phones = await _phoneRepository.GetAllAsync();
            return phones.Select(p => new PhoneDto
            {
                Id = p.Id,
                PhoneNumber = p.PhoneNumber,
                UserId = p.UserId
            });
        }

        public async Task<PhoneDto> GetPhoneByIdAsync(int id)
        {
            var phone = await _phoneRepository.GetByIdAsync(id);
            if (phone == null) throw new ArgumentException("Phone not found");

            return new PhoneDto
            {
                Id = phone.Id,
                PhoneNumber = phone.PhoneNumber,
                UserId = phone.UserId
            };
        }

        public async Task CreatePhoneAsync(PhoneDto phoneDto)
        {
            if (!PhoneNumberValidation.IsValid(phoneDto.PhoneNumber))
                throw new ArgumentException("Invalid phone number format");

            var phone = new Phone
            {
                PhoneNumber = phoneDto.PhoneNumber,
                UserId = phoneDto.UserId
            };

            await _phoneRepository.AddAsync(phone);
        }

        public async Task UpdatePhoneAsync(PhoneDto phoneDto)
        {
            var existingPhone = await _phoneRepository.GetByIdAsync(phoneDto.Id);
            if (existingPhone == null) throw new ArgumentException("Phone not found");

            if (!PhoneNumberValidation.IsValid(phoneDto.PhoneNumber))
                throw new ArgumentException("Invalid phone number format");

            existingPhone.PhoneNumber = phoneDto.PhoneNumber;
            existingPhone.UserId = phoneDto.UserId;

            await _phoneRepository.UpdateAsync(existingPhone);
        }
        
        public async Task DeletePhoneAsync(int id)
        {
            var phone = await _phoneRepository.GetByIdAsync(id);
            if (phone == null) throw new ArgumentException("Phone not found");
            await _phoneRepository.DeleteAsync(id);
        }
    }
}
