using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WowGuildManager.Common.GlobalConstants;
using WowGuildManager.Data;
using WowGuildManager.Domain.Identity;
using WowGuildManager.Domain.Logs;

namespace WowGuildManager.Web.Filters.ExceptionFilters
{
    public class LogErorInDatabaseExceptionFilter : ExceptionFilterAttribute
    {
        private readonly WowGuildManagerDbContext context;
        private readonly UserManager<WowGuildManagerUser> userManager;

        public LogErorInDatabaseExceptionFilter(
            WowGuildManagerDbContext context,
            UserManager<WowGuildManagerUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public override void OnException(ExceptionContext context)
        {
            var username = userManager.GetUserName(context.HttpContext.User);

            if (username == null)
            {
                username = WowGuildManagerUserConstants.NullUserWarningMessage;
            }

            var exceptionLog = new ExceptionLog
            {
                ExceptionMessage = context.Exception.Message,
                Username = username
            };

            context.ExceptionHandled = true;

            this.context.ExceptionLogs.Add(exceptionLog);
            this.context.SaveChanges();
        }
    }
}
