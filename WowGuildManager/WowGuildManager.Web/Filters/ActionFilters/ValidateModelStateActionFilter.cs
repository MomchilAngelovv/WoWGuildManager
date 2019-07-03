namespace WowGuildManager.Web.Filters.ActionFilters
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;
    using WowGuildManager.Common.GlobalConstants;

    public class ValidateModelStateActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid == false)
            {
                throw new ArgumentException(ErrorConstants.InvalidDataProvided);
            }

            await next();
        }
    }
}
