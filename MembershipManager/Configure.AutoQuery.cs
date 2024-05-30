[assembly: HostingStartup(typeof(ConfigureAutoQuery))]

namespace MembershipManager;

public class ConfigureAutoQuery : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services =>
        {
            services.AddPlugin(new AutoQueryDataFeature
            {
                MaxLimit = 1000,
                //IncludeTotal = true,
            });
            services.AddPlugin(new AutoQueryFeature
            {
                MaxLimit = 1000,
                //IncludeTotal = true,
            });
        });
}