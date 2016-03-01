using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Wingman.Core.Domain;
using Wingman.Core.Infrastructure;
using Wingman.Core.Models;
using Wingman.Data.Infrastructure;

namespace Wingman.Infrastructure
{
    public class AuthorizationRepository : IAuthorizationRepository, IDisposable
    {
        private readonly IUserStore<WingmanUser, string> _userStore;
        private readonly IDatabaseFactory _databaseFactory;
        private readonly UserManager<WingmanUser, string> _userManager;

        private WingmanDataContext db;
        protected WingmanDataContext Db => db ?? (db = _databaseFactory.GetDataContext());

        public AuthorizationRepository(IDatabaseFactory databaseFactory, IUserStore<WingmanUser, string> userStore)
        {
            _userStore = userStore;
            _databaseFactory = databaseFactory;
            _userManager = new UserManager<WingmanUser, string>(userStore);
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
