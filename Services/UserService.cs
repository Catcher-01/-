using System.Threading.Tasks;
using HealthSupervision.Entities;
using HealthSupervision.DTOs;
using HealthSupervision.DAOs;
using HealthSupervision.Helpers;
using AutoMapper;

namespace HealthSupervision.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserAccount> RegisterAsync(UserRegisterDTO dto)
        {
            return await _userRepository.RegisterAsync(dto);
        }

        public async Task UpdateUserAsync(string userId, UserUpdateDTO dto)
        {
            await _userRepository.UpdateAsync(userId, dto);
        }

        public async Task<UserAccount> GetUserByIdAsync(string userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}