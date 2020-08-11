using api.Services;
using Api.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api
{
    public class Startup
    {
        public IHostEnvironment HostingEnvironment { get; private set; }
        public IConfiguration Configuration { get; }


        public Startup(
            IConfiguration configuration,
            IHostEnvironment env
        )
        {
            HostingEnvironment = env;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Mvc
            services.AddMvc();

            // configure strongly typed settings objects
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddSingleton(Configuration);
            var confFile = Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>();
            var confConn = Configuration.GetSection(nameof(ConnectionStrings)).Get<ConnectionStrings>();

            // Database config
            // SqLite
            services.AddDbContext<DataContext>(options => options.UseSqlite(confConn.AppConnSqLite));

            // Add CORS policy
            services.AddCors(options =>
            {
                options.AddPolicy("cors",
                builder =>
                {
                    // Not a permanent solution, but just trying to isolate the problem
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.AddControllers();

            // configure DI for application services
            services.AddScoped<IVideoService, VideoService>();
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseRouting();

            app.UseCors("cors"); // global cors policy

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
