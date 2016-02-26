using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Wingman.Infrastructure;
using Wingman.Core.Models;
using Wingman.Core.Infrastructure;

namespace Wingman.Controllers
{
    public class AccountsController : ApiController
    {
        private readonly IAuthorizationRepository _authRepository;

        public AccountsController(IAuthorizationRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [AllowAnonymous]
        [Route("api/accounts/register")]
        public async Task<IHttpActionResult> Register(RegistrationModel registration)
        {
            //Server Side Validation
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Pass the Registration onto AuthRepository
            var result = await _authRepository.RegisterUser(registration);

            //Check to see the Registration was Successful
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Registration form was invalid.");
            }

        }
    }
}
