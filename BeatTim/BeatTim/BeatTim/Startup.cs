using BeatTim.Extensions;
using BeatTim.Models;
using BeatTim.Repositories;
using BeatTim.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeatTim
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddDbContext<ApplicationContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IClientRepository, ClientRepository>()
                .AddScoped<IUserProfileRepository, UserProfileRepository>()
                .AddScoped<ILoginDetailRepository, LoginDetailRepository>()
                .AddScoped<IAccessTokenRepository, AccessTokenRepository>()
                .AddScoped<IContactRepository, ContactRepository>()
                .AddScoped<ICityRepository, CityRepository>()
                .AddScoped<IBeatRepository, BeatRepository>()
                .AddScoped<IUserCommentRepository, UserCommentRepository>()
                .AddScoped<IGenreRepository, GenreRepository>()
                .AddScoped<IFollowerRepository, FollowerRepository>();
            services.AddScoped<AuthorizationService>()
                .AddScoped<AccountService>()
                .AddScoped<BeatService>()
                .AddScoped<CityService>()
                .AddScoped<GenreService>()
                .AddScoped<ImageService>()
                .AddScoped<AudioService>()
                .AddScoped<SubscriptionService>()
                .AddScoped<ClientService>();
            services.AddSession();

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseUserToken();
            app.UseAdmin();

            app.UseEndpoints(endpoints => { endpoints.MapRazorPages(); });
        }
    }
}