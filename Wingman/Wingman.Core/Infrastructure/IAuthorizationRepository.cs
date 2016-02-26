using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Wingman.Core.Domain;
using Wingman.Core.Models;

namespace Wingman.Core.Infrastructure
{
    public interface IAuthorizationRepository
    {
        Task<WingmanUser> FindUser(string username, string password);
        Task<IdentityResult> RegisterUser(RegistrationModel model);
    }
}