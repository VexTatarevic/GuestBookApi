

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

using AutoMapper;

using Web.Services;
using Microsoft.Extensions.Logging;

namespace Web
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // DB Connections
            services.AddDbContext<Data.GuestBook.DBContext>(cfg=>{
                cfg.UseSqlServer(_config.GetConnectionString("LocalConnectionString"));
            },ServiceLifetime.Scoped);

            // Third-Party Libs
            services.AddAutoMapper();

            // Services
            services.AddTransient<IMailService,NullMailService>(); // TODO: Suport for real mail service
            
            // Repositories
            services.AddScoped<Data.GuestBook.IRepository, Data.GuestBook.Repository>();

            // DB Seeders
            services.AddTransient<Data.GuestBook.DBInitializer>();

            // MVC
            services.AddMvc() // prevent circular redundancy
                .AddJsonOptions(opt=>opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            ILoggerFactory loggerFactory,
            IHostingEnvironment env,
            Data.GuestBook.DBInitializer seeder)
        {
            // Configure Logging
            loggerFactory.AddConsole(_config.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Send User to error page if this is production environment
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseExceptionHandler("/error");

            // Allow serving static files
            app.UseStaticFiles();

            // Set up routes
            app.UseMvc(cfg =>
            {
                cfg.MapRoute("Default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Home", Action = "Index" }
                    );
            });

            // Seed the database
            //using (var scope = app.ApplicationServices.CreateScope())
            //{
                seeder.Seed().Wait();
            //}
        }
    }
}
