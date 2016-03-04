using Microsoft.AspNet.Identity;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wingman.Core.Domain;
using Wingman.Core.Infrastructure;
using Wingman.Core.Repository;
using Wingman.Data.Infrastructure;
using Wingman.Data.Repository;
using Wingman.Infrastructure;

namespace Wingman.Jeeves
{
    class Program
    {
        static void writeJeeves()
        {
            Console.WriteLine("     #####                 ");
            Console.WriteLine("    #### _\\_  ________     ");
            Console.WriteLine("    ##=-[.].]| \\      \\    ");
            Console.WriteLine("    #(    _\\ |  |------|   ");
            Console.WriteLine("     #   __| |  ||||||||   ");
            Console.WriteLine("      \\  _ /  |  ||||||||  ");
            Console.WriteLine("   .--'--' -. |  | ____ |  ");
            Console.WriteLine("  / __      `| __ |[o__o] |");
            Console.WriteLine("_(____nm_______ / ____\\____");
            Console.WriteLine();
        }

        static IKernel kernel;
        static Dictionary<int, string> Options = new Dictionary<int, string>
        {
            { 1, "Create Database" }
        };

        static void SetupNinject()
        {
            kernel = new StandardKernel();

            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            kernel.Bind<IResponseRepository>().To<ResponseRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<ISubmissionRepository>().To<SubmissionRepository>();
            kernel.Bind<ITopicRepository>().To<TopicRepository>();
            kernel.Bind<IUserRoleRepository>().To<UserRoleRepository>();
            kernel.Bind<IWingmanUserRepository>().To<WingmanUserRepository>();
            kernel.Bind<IUserStore<WingmanUser, string>>().To<UserStore>();
            kernel.Bind<IAuthorizationRepository>().To<AuthorizationRepository>();
        }
        static void Main(string[] args)
        {
            SetupNinject();

            while (true)
            {
                Console.Clear();
                // say hello to the user
                writeJeeves();

                Console.WriteLine("Hello! My name is Jeeves. What would you like me to do?");

                foreach (var option in Options)
                {
                    Console.WriteLine(option.Key + ": " + option.Value);
                }

                string choice = Console.ReadLine();

                switch (int.Parse(choice))
                {
                    case 1:
                        createDatabase();
                        break;
                    default:
                        break;
                }
            }
        }

        static void createDatabase()
        {
            Console.WriteLine("Cool! Let's build a database.");

            Console.WriteLine("How many users do you want to make?");

            int numberOfUsers = 0;

            while (true)
            {
                string input = Console.ReadLine();
                if(int.TryParse(input, out numberOfUsers))
                {
                    break;
                }
            }

            IUnitOfWork _unitOfWork = kernel.Get<IUnitOfWork>();
            IWingmanUserRepository _wingmanUserRepository = kernel.Get<IWingmanUserRepository>();

            // topics
            ITopicRepository _topicRepository = kernel.Get<ITopicRepository>();
            Console.Write("Creating topics... ");
            _topicRepository.Add(new Topic { TopicName = "Hookups" });
            _topicRepository.Add(new Topic { TopicName = "Crazies" });
            _topicRepository.Add(new Topic { TopicName = "Family" });
            _topicRepository.Add(new Topic { TopicName = "Friends" });
            _topicRepository.Add(new Topic { TopicName = "Relationship Trouble" });
            _topicRepository.Add(new Topic { TopicName = "Other" });

            _unitOfWork.Commit();
            Console.WriteLine("done!");

            var random = new Random();

            // users
            IAuthorizationRepository _authorizationRepository = kernel.Get<IAuthorizationRepository>();
            Console.Write("Creating users... ");
            for (int i = 0; i < numberOfUsers; i++)
            {
                _authorizationRepository.RegisterUser(new Core.Models.RegistrationModel
                {
                    EmailAddress = $"wingman{i}@gmail.com",
                    Gender = (Gender)random.Next(1, 3),
                    Username = $"wingman{i}",
                    Password = $"wingman{i}",
                    ConfirmPassword = $"wingman{i}"
                });

                Console.WriteLine($"Added wingman{i} to the database");

                Thread.Sleep(250);
            }
            Console.WriteLine("done!");


            // submissions
            ISubmissionRepository _submissionRepository = kernel.Get<ISubmissionRepository>();
            IResponseRepository _responseRepository = kernel.Get<IResponseRepository>();
            Console.Write("Creating submissions... ");

            for (int i = 0; i < numberOfUsers; i++)
            {
                int numberOfSubmissionsForThisUser = random.Next(1, 10);

                for (int j = 0; j < numberOfSubmissionsForThisUser; j++)
                {
                    var openedDate = LoremNET.Lorem.DateTime(DateTime.Now.AddYears(-1));

                    var submission = new Submission
                    {
                        Context = LoremNET.Lorem.Paragraph(random.Next(5, 30), 3),
                        DateOpened = openedDate,
                        DateClosed = random.Next(0, 1) == 1 ? (DateTime?)openedDate.AddDays(random.Next(1, 10)) : null,
                        TextMessage = LoremNET.Lorem.Paragraph(random.Next(5, 30), 3),
                        TopicId = random.Next(1, _topicRepository.Count()),
                        User = _wingmanUserRepository.GetFirstOrDefault(u => u.UserName == "wingman" + i),
                    };

                    _submissionRepository.Add(submission);
                }

                _unitOfWork.Commit();
            }

            Console.Write("Creating responses... ");
            for (int i = 0; i < numberOfUsers; i++)
            {
                int numberOfResponsesForThisUser = random.Next(1, 5);

                for (int j = 0; j < numberOfResponsesForThisUser; j++)
                {
                    var randomSubmissionId = random.Next(1, _submissionRepository.Count());

                    var randomSubmission = _submissionRepository.GetById(randomSubmissionId);

                    var response = new Response
                    {
                        DateSubmitted = randomSubmission.DateOpened.AddDays(random.Next(1, 5)),
                        KeyPrice = random.Next(1, 100),
                        ResponseText = LoremNET.Lorem.Paragraph(random.Next(5, 30), 3),
                        User = _wingmanUserRepository.GetFirstOrDefault(u => u.UserName == "wingman" + i),
                        SubmissionId = randomSubmissionId
                    };

                    int oddsBetween1and10 = random.Next(1, 10);

                    if(oddsBetween1and10 == 5)
                    {
                        response.Picked = true;
                        randomSubmission.DateClosed = response.DateSubmitted.AddDays(random.Next(1, 3));
                        _submissionRepository.Update(randomSubmission);
                    }

                    _responseRepository.Add(response);

                    _unitOfWork.Commit();
                }
            }

            Console.WriteLine("done!");
        }
    }
}
