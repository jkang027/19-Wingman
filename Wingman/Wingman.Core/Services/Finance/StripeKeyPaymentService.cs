using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingman.Core.Domain;
using Wingman.Core.Infrastructure;
using Wingman.Core.Repository;

namespace Wingman.Core.Services.Finance
{
    public class StripeKeyPaymentService : IKeyPaymentService
    {
        private readonly IWingmanUserRepository _wingmanUserRepository;
        private readonly IUnitOfWork _unitOfWork;

        public StripeKeyPaymentService(IWingmanUserRepository wingmanUserRepository, IUnitOfWork unitOfWork)
        {
            _wingmanUserRepository = wingmanUserRepository;
            _unitOfWork = unitOfWork;
        }

        public void BuyKeys(WingmanUser user, string token, int numberOfKeys)
        {
            var myCharge = new StripeChargeCreateOptions();

            // always set these properties
            myCharge.Amount = numberOfKeys * 10;
            myCharge.Currency = "usd";

            // setting up the card
            myCharge.Source = new StripeSourceOptions()
            {
                // set this property if using a token
                TokenId = token,
            };

            var chargeService = new StripeChargeService("sk_test_eNNtPIlw4UnBEBwUUS7xPkRn");

            StripeCharge stripeCharge = chargeService.Create(myCharge);

            if(stripeCharge.Paid)
            {
                user.NumberOfKeys = user.NumberOfKeys.GetValueOrDefault() + numberOfKeys;

                _wingmanUserRepository.Update(user);

                _unitOfWork.Commit();
            }
            else
            {
                throw new Exception("Payment was unsuccessful");
            }
        }
    }
}
