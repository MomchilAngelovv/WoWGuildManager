namespace WowGuildManager.Web.Filters.ActionFilters
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc.Filters;

    using WowGuildManager.Common.GlobalConstants;
    using WowGuildManager.Data;
    using WowGuildManager.Domain.Logs;

    public class ValidateModelStateActionFilter : IAsyncActionFilter
    {
        private readonly WowGuildManagerDbContext context;

        public ValidateModelStateActionFilter(
            WowGuildManagerDbContext context)
        {
            this.context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var errors = new List<Error>();

            if (context.ModelState.IsValid == false)
            {
                foreach (var exception in context.ModelState.Values)
                {
                    foreach (var error in exception.Errors)
                    {
                        var exceptionLog = new Error
                        {
                            Message = $"Warning: {error.ErrorMessage}",
                            UserId = context.HttpContext.User.Identity.Name,
                            DateTime = DateTime.UtcNow,
                        };

                        errors.Add(exceptionLog);
                    }
                }

                await this.context.Errors.AddRangeAsync(errors);
                await this.context.SaveChangesAsync();

                throw new ArgumentException(ErrorConstants.InvalidDataProvided);
            }

            await next();
        }
    }
}
