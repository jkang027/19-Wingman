using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Wingman.Domain;
using Wingman.Models;

namespace Wingman.Infrastructure
{
    public class AuthorizationRepository : IDisposable
    {
        private WingmanDataContext _db;
        private UserManager<WingmanUser> _userManager;

        public AuthorizationRepository()
        {
            _db = new WingmanDataContext();
            _userManager = new UserManager<WingmanUser>(new UserStore<WingmanUser>(_db));
        }

        public async Task<IdentityResult> RegisterUser(RegistrationModel model)
        {
            var wingmanUser = new WingmanUser
            {
                UserName = model.Username,
                Email = model.EmailAddress,
                Gender = model.Gender
            };

            var result = await _userManager.CreateAsync(wingmanUser, model.Password);

            return result;
        }

        public async Task<WingmanUser> FindUser(string username, string password)
        {
            return await _userManager.FindAsync(username, password);
        }

        public void Dispose()
        {
            _userManager.Dispose();
        }
    }
}
