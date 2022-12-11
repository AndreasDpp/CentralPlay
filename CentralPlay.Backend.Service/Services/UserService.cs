using AutoMapper;
using CentralPlay.Backend.Repository.Domain.Entities;
using CentralPlay.Backend.Repository.Interfaces;
using CentralPlay.Backend.Service.DTO;
using CentralPlay.Backend.Service.Interfaces;

namespace CentralPlay.Backend.Service.Services
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="mapper"></param>
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        #endregion

        public async Task<UserDTO> GetById(string id)
        {
            return _mapper.Map<UserDTO>(await _userRepository.GetAsync(id, id));
        }

        public async Task AddAsync(UserDTO userDTO)
        {
            await _userRepository.AddAsync(_mapper.Map<User>(userDTO));
        }

        public async Task UpdateCountAsync(string userId, int amountOfModelsAdded = 1)
        {
            var user = await GetById(userId);

            user.AmountOfModels += amountOfModelsAdded;

            await _userRepository.UpdateAsync(user.Id, _mapper.Map<User>(user));
        }
    }
}
