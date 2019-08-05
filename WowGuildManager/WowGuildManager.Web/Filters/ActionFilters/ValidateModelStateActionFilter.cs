namespace WowGuildManager.Web.Filters.ActionFilters
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc.Filters;

    using WowGuildManager.Data;
    using WowGuildManager.Domain.Logs;
    using WowGuildManager.Domain.Identity;

    public class ValidateModelStateActionFilter : IAsyncActionFilter
    {
        private readonly UserManager<WowGuildManagerUser> userManager;
        private readonly WowGuildManagerDbContext context;

        public ValidateModelStateActionFilter(
            UserManager<WowGuildManagerUser> userManager,
            WowGuildManagerDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var errors = new List<Error>();
            var userId = this.userManager.GetUserId(context.HttpContext.User);

            if (context.ModelState.IsValid == false)
            {
                foreach (var exception in context.ModelState.Values)
                {
                    foreach (var error in exception.Errors)
                    {
                        var exceptionLog = new Error
                        {
                            Message = $"Warning: {error.ErrorMessage}",
                            UserId = userId,
                            DateTime = DateTime.UtcNow
                        };

                        errors.Add(exceptionLog);
                    }
                }

                await this.context.AddRangeAsync(errors);
                await this.context.SaveChangesAsync();
            }

            await next();
        }
    }
}
