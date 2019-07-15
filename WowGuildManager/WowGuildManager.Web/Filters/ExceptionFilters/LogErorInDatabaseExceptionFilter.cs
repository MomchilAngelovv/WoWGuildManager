namespace WowGuildManager.Web.Filters.ExceptionFilters
{
    using System;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Domain.Identity;
    using WowGuildManager.Common.GlobalConstants;

    public class LogErorInDatabaseExceptionFilter : ExceptionFilterAttribute
    {
        private readonly UserManager<WowGuildManagerUser> userManager;

        private readonly WowGuildManagerDbContext context;

        public LogErorInDatabaseExceptionFilter(
            UserManager<WowGuildManagerUser> userManager,
            WowGuildManagerDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
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
                Username = username,
                ExceptionTime = DateTime.UtcNow
            };

            context.ExceptionHandled = true;

            this.context.ExceptionLogs.Add(exceptionLog);
            this.context.SaveChanges();
        }
    }
}
