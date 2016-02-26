using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Wingman.Core.Domain;
using Wingman.Core.Models;

namespace Wingman
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            CreateMaps();

        }
        public static void CreateMaps()
        {
            Mapper.CreateMap<Response, ResponseModel>();
            Mapper.CreateMap<Submission, SubmissionModel>();
            Mapper.CreateMap<Topic, TopicModel>();
        }
    }
}
