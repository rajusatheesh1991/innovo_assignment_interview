using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  Hangfire;
using Hangfire.Dashboard;

namespace InnovoAssignment.Web.Filters
{
    public class HangfireAuthorizationFilter: IDashboardAuthorizationFilter
    {

        public bool Authorize(DashboardContext context)
        {
            // var httpContext = context.GetHttpContext();

            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            // handled through middleware
            return true; // httpContext.User.Identity.IsAuthenticated;
        }

    }
}
