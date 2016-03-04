using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wingman.Requests
{
    public class KeyPaymentRequest
    {
        public string token { get; set; }
        public int numberOfKeys { get; set; }
    }
}