using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingman.Core.Domain;

namespace Wingman.Core.Services.Finance
{
    public interface IKeyPaymentService
    {
        void BuyKeys(WingmanUser user, string token, int numberOfKeys);
        //TODO: void RedeemKeys(string token);
    }
}
