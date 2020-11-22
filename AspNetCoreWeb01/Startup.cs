using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreWeb01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStatusCodePages();
            app.UseStaticFiles();

            app.UseRouting();
            #region 中间件的使用练习

            //这是一个中断式中间件，会停止后面的管道处理流程
            //app.Run(ctx => ctx.Response.WriteAsync("this is a terminal middleware" + Environment.NewLine));

            //app.Use(next =>
            //{
            //    Console.WriteLine("这是第1个中间件！");
            //    return new RequestDelegate(async (context) =>
            //    {
            //        await next(context);
            //        await context.Response.WriteAsync("middleware start..."+Environment.NewLine);
            //        await context.Response.WriteAsync("middleware end." + Environment.NewLine);
            //    });
            //});

            #endregion
            app.UseAuthorization();
            app.UseSession();
            loggerFactory.AddLog4Net();//添加Log4net中间件
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=First}/{action=Index}/{id?}");
            });
        }
    }
}
