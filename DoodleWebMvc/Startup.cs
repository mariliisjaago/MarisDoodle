using DoodleWebMvc.Utils;
using DoodleWebMvc.Utils.Contracts;
using MarisDoodleLibrary.Contracts.Db;
using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Contracts.Routines;
using MarisDoodleLibrary.Db;
using MarisDoodleLibrary.Repos;
using MarisDoodleLibrary.Routines;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DoodleWebMvc
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

            services.AddHttpContextAccessor();

            services.AddSession();
            services.AddMemoryCache();

            services.AddSingleton(new ConnectionStringData
            {
                SqlConnectionName = "Production"
            });

            services.AddScoped<IDataAccess, SqlDataAccess>();

            services.AddScoped<IPollRepo, SqlPollRepo>();
            services.AddScoped<IOptionRepo, SqlOptionRepo>();
            services.AddScoped<IVoteRepo, SqlVoteRepo>();

            services.AddScoped<IPollRoutine, PollRoutine>();
            services.AddScoped<IVotingRoutine, VotingRoutine>();
            services.AddScoped<IResultRoutine, ResultRoutine>();

            services.AddScoped<IUrlGenerator, UrlGenerator>();
            services.AddScoped<IModelPopulator, ModelPopulator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
