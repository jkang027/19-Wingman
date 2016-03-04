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
using Wingman.Core.Repository;
using AutoMapper;
using System.Web.Http.Description;
using Wingman.Core.Services.Finance;
using Wingman.Requests;

namespace Wingman.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly IKeyPaymentService _keyPaymentService;
        private readonly IAuthorizationRepository _authRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountsController(IKeyPaymentService keyPaymentService, IAuthorizationRepository authRepository, IWingmanUserRepository wingmanUserRepository, IUnitOfWork unitOfWork) : base(wingmanUserRepository)
        {
            _keyPaymentService = keyPaymentService;
            _authRepository = authRepository;
            _unitOfWork = unitOfWork;
           
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

        [Route("api/accounts/currentuser")]
        [HttpGet]
        [ResponseType(typeof(WingmanUserModel.Profile))]
        public IHttpActionResult GetCurrentUser()
        {
            return Ok(Mapper.Map<WingmanUserModel.Profile>(CurrentUser));
        }

        [Route("api/accounts/currentuser")]
        [HttpPut]
        public IHttpActionResult UpdateCurrentUser(string id, WingmanUserModel.Profile user)
        {
            // check to see that id == user.Id

            // check to see that user.Id == CurrentUser.Id
          
            // update the user

            // call repository.update
       
            // call unitofwork.commit
        
            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("api/accounts/payforkeys")]
        public IHttpActionResult PayForKeys(KeyPaymentRequest request)
        {
            _keyPaymentService.BuyKeys(CurrentUser, request.token, request.numberOfKeys);

            return Ok();
        }
    }
}
