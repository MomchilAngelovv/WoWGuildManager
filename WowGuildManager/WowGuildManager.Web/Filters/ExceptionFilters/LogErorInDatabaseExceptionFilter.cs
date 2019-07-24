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
            var userId = userManager.GetUserId(context.HttpContext.User);

            if (userId == null)
            {
                userId = WowGuildManagerUserConstants.NullUserWarningMessage;
            }

            var exceptionLog = new Error
            {
                Message = $"Exception: {context.Exception.Message}",
                UserId = userId,
                DateTime = DateTime.UtcNow,
            };

            this.context.Errors.AddAsync(exceptionLog);
            this.context.SaveChanges();
        }
    }
}
