namespace Praga.Paisakanaku.Web.Middleware
{
    public class AuthenticationMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // authentication logic goes here
            // Call the next middleware in the pipeline
            //context.Response.Redirect("/login");
            // Handle authorization failure, e.g., return 403 Forbidden
            //context.Response.StatusCode = 403;
            //return;

            await next.Invoke(context);
        }
    }
}
