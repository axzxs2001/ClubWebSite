using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ClubWebSite.Model;
using Asp.NetCore_WebPage.Model.Repository;
using UEditorNetCore;
using ClubWebSite.Model.DataModel;
using Microsoft.EntityFrameworkCore;

namespace ClubWebSite
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
          
        }

        public IConfigurationRoot Configuration { get; }
      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            //添加数据操作
            var connection =string.Format( Configuration.GetConnectionString("DefaultConnection"),System.IO.Directory.GetCurrentDirectory());
            Console.WriteLine($"Connecting{connection}");
            //添加数据实体
            services.AddDbContext<ClubWebSiteDbContext>(options =>options.UseSqlite(connection));


            //注入活动仓储类
          services.AddTransient<IActiveResitory, ActiveResitory>();
            //注入用户仓储类
            services.AddTransient<IUserResitory, UserResitory>();

            services.AddUEditorService();
            services.AddMvc();
        }
    
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
           
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
        
            //为验证添加中间件
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                //验证方案名称
                AuthenticationScheme = "loginvalidate",
                //没有权限时导航的登录action
                LoginPath = new Microsoft.AspNetCore.Http.PathString("/login"),
                //访问被拒绝后的acion
                AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/login"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                SlidingExpiration = true
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
