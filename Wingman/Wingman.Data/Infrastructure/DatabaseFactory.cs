using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingman.Core.Infrastructure;
using Wingman.Core.Repository;
using Wingman.Infrastructure;

namespace Wingman.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private readonly WingmanDataContext _dataContext;

        public WingmanDataContext GetDataContext()
        {
            return _dataContext ?? new WingmanDataContext();
        }

        public DatabaseFactory()
        {
            _dataContext = new WingmanDataContext();
        }

        protected override void DisposeCore()
        {
            if (_dataContext != null) _dataContext.Dispose();
        }
    }
}
