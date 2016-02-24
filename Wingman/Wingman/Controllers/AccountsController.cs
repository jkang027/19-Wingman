using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Wingman.Infrastructure;
using Wingman.Models;

namespace Wingman.Controllers
{
    public class AccountsController : ApiController
    {
        private AuthorizationRepository _repo = new AuthorizationRepository();

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
            var result = await _repo.RegisterUser(registration);

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

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
        }
    }
}
