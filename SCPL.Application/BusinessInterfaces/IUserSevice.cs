using Microsoft.AspNetCore.Http;
using SCPL.Core.DBEntities;

namespace SCPL.Application.BusinessInterfaces
{
    public interface IUserService
    {
        Task<string> LoginUserAsync(UserTable user, HttpContext context);
        UserTable SignUpUser(UserTable user);
    }
}
