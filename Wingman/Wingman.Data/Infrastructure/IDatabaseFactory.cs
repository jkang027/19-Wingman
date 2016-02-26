using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingman.Infrastructure;

namespace Wingman.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        WingmanDataContext GetDataContext();
    }
}
