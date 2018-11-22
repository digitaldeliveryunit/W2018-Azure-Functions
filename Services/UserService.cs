using System;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Services.Interfaces;
using System.Linq;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Repositories.Interfaces;

namespace com.petronas.myevents.api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User GetCurrentUser()
        {
            return _userRepository.GetAll().FirstOrDefault(x => !x.IsDeleted && x.Id == DefaultValue.UserId);
        }
    }
}
