using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Wingman.Core.Domain;
using Wingman.Core.Repository;

namespace Wingman.Infrastructure
{
    public class BaseApiController : ApiController
    {
        protected readonly IWingmanUserRepository _wingmanUserRepository;

        public BaseApiController(IWingmanUserRepository wingmanUserRepository)
        {
            _wingmanUserRepository = wingmanUserRepository;
        }

        protected WingmanUser CurrentUser
        {
            get
            {
                return _wingmanUserRepository.GetFirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }
    }
}