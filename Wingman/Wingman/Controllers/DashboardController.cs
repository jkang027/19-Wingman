using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wingman.Core.Infrastructure;
using Wingman.Core.Models;
using Wingman.Core.Repository;
using Wingman.Infrastructure;

namespace Wingman.Controllers
{
    [Authorize]
    public class DashboardController : BaseApiController
    {
        private readonly ISubmissionRepository _submissionRepository;
        private readonly IResponseRepository _responseRepository;
        private readonly IUnitOfWork _unitOfWork;

        //Constructor based dependency injection
        public DashboardController(ISubmissionRepository submissionRepository, IWingmanUserRepository wingmanUserRepository, IResponseRepository responseRepository, IUnitOfWork unitOfWork) : base(wingmanUserRepository)
        {
            _submissionRepository = submissionRepository;
            _unitOfWork = unitOfWork;
            _responseRepository = responseRepository;
        }

        [Route("api/dashboard/feed")]
        public DashboardModel GetDashboardFeed()
        {
            var dashboard = new DashboardModel();

            // Get 3 random submissions from the database
            var randomClosedSubmissions = _submissionRepository.GetWhere(s => s.Responses.Any(r => r.Picked))
                                                               .OrderBy(s => Guid.NewGuid())
                                                               .Take(3);

            // Reduce responses to these submissions to the first picked submission
            foreach (var submission in randomClosedSubmissions)
            {
                submission.Responses = submission.Responses.Where(r => r.Picked).Take(1).ToList();
            }

            dashboard.RecentlyPickedSubmissions = Mapper.Map<IEnumerable<SubmissionModel>>(randomClosedSubmissions);

            dashboard.TopWingmen = Mapper.Map<IEnumerable<WingmanUserModel>>(
                _wingmanUserRepository.GetAll().OrderByDescending(u => u.NumberOfAnswersPicked).Take(5)
            );

            return dashboard;
        }
    }
}
