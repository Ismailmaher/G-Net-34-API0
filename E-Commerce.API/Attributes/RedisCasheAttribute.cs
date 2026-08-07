using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;
using System.Text.Json;

namespace E_Commerce.API.Attributes
{
    public class RedisCasheAttribute:ActionFilterAttribute
    {
        private readonly int _durationInSeconds;
        public RedisCasheAttribute(int durationInSeconds=90)
        {
            _durationInSeconds = durationInSeconds;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<ICasheService>();
            var casheKey = CreateCasheKey(context.HttpContext.Request);
            var cashed= await cacheService.GetAsync(casheKey);

            if (!string.IsNullOrEmpty(cashed))
            {
                context.Result = new ContentResult
                {
                    Content = cashed,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
            }
            var executed = await next.Invoke();
            if (executed.Result is ObjectResult { Value : not null}ok)
            {

                var json = JsonSerializer.Serialize(ok.Value);

                await cacheService.SetAsync(
                    casheKey,
                    json,
                    TimeSpan.FromSeconds(_durationInSeconds));
            }
        }

        private string CreateCasheKey(HttpRequest request)
        {
            var key = new StringBuilder();
            key.Append(request.Path);
            key.Append('?');
            foreach(var (k,v) in request.Query.OrderBy(q => q.Key)) {
                key.Append(k).Append('=').Append(v).Append('&');
            }
            return key.ToString();
        }
    }
}
