using ServiceStack.Auth;
using MembershipManager.Data;
using ServiceStack.Data;
using ServiceStack.Configuration;

[assembly: HostingStartup(typeof(ConfigureAuth))]

namespace MembershipManager;

public class ConfigureAuth : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => 
        {
            services.AddPlugin(new AuthFeature(IdentityAuth.For<ApplicationUser>(options => {
                options.SessionFactory = () => new CustomUserSession();
                options.CredentialsAuth();
                options.AdminUsersFeature();
            })));

            //services.AddSingleton<IStartupFilter>(new StartupFilter(context.Configuration));
        });

    //public class StartupFilter : IStartupFilter
    //{
    //    private readonly IConfiguration _configuration;

    //    public StartupFilter(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
    //    {
    //        return async builder =>
    //        {
    //            using (var serviceScope = builder.ApplicationServices.CreateScope())
    //            {
    //                await UserManagerHelper.CreateDefaultAdminUser(serviceScope.ServiceProvider, _configuration);
    //            }
    //            next(builder);
    //        };
    //    }
    //}
}