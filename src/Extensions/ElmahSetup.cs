using System;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.WebApi.Extensions
{
    public static class ElmahSetup
    {
        public static void AddElmahCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddElmah<SqlErrorLog>(options =>
            {
                options.ConnectionString = configuration.GetSection($"ConnectionStrings:ElmahConnection").Value;
                options.Path = "/elmah";
            });
        }

        public static void UseElmahCore(this IApplicationBuilder app)
        {
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/elmah", StringComparison.OrdinalIgnoreCase), appBuilder =>
            {
                appBuilder.Use(next =>
                {
                    return async ctx =>
                    {
                        ctx.Features.Get<IHttpBodyControlFeature>().AllowSynchronousIO = true;
                        await next(ctx);
                    };
                });
            });

            app.UseElmah();
        }
    }
}