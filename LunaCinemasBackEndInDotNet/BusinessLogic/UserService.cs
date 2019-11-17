using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class UserService
    {
        private readonly IUserContext _userContext;

        public UserService(IUserContext userContext)
        {
            _userContext = userContext;
        }


    }
}