using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using MeetingManagementSystem.Models;

namespace MeetingManagementSystem
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<DiscussionVM, Discussion>().ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId));
                //cfg.CreateMap<Discussion, DiscussionVM>().ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src =>src.EmployeeId));

                cfg.CreateMap<DiscussionVM, Discussion>();
                cfg.CreateMap<Discussion, DiscussionVM>();
            });



            //SqlDependency.Start(ConfigurationManager.ConnectionStrings["MeetingManagementDbContext"].ConnectionString);
        }

        //protected void Application_BeginRequest(Object sender, EventArgs e)
        //{
        //    CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        //    newCulture.DateTimeFormat.ShortDatePattern = "dd-MMM-yyyy";
        //    newCulture.DateTimeFormat.DateSeparator = "-";
        //    Thread.CurrentThread.CurrentCulture = newCulture;
        //}

        protected void Application_End()
        {
            SqlDependency.Stop(ConfigurationManager.ConnectionStrings["MeetingManagementDbContext"].ConnectionString);
        }
    }
}
