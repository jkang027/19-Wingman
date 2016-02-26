using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wingman.Core.Domain;
using Wingman.Core.Repository;
using Wingman.Data.Infrastructure;

namespace Wingman.Data.Repository
{
    public class SubmissionRepository : Repository<Submission>, ISubmissionRepository
    {
        public SubmissionRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}
