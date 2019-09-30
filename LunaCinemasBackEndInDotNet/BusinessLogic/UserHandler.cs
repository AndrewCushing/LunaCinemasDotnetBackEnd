using LunaCinemasBackEndInDotNet.Persistence;

namespace LunaCinemasBackEndInDotNet.BusinessLogic
{
    public class UserHandler
    {
        private readonly IUserContext _userContext;

        public UserHandler(IUserContext userContext)
        {
            _userContext = userContext;
        }


    }
}