using System.Linq;
using com.petronas.myevents.api.Constants;
using com.petronas.myevents.api.Models;
using com.petronas.myevents.api.Repositories.Interfaces;
using com.petronas.myevents.api.Services.Interfaces;
using Microsoft.Azure.Documents.Client;

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
            var feedOptions = new FeedOptions
            {
                MaxItemCount = 1,
                EnableCrossPartitionQuery = true
            };
            return _userRepository.GetAll(x => !x.IsDeleted && x.Id == DefaultValue.UserId, feedOptions).ToList()
                .FirstOrDefault();
        }
    }
}