using com.petronas.myevents.api.Models;

namespace com.petronas.myevents.api.Services.Interfaces
{
    public interface IUserService
    {
        User GetCurrentUser();
    }
}