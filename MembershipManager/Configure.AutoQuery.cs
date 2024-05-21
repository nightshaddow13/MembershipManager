[assembly: HostingStartup(typeof(ConfigureAutoQuery))]

namespace MembershipManager;

public class ConfigureAutoQuery : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            services.AddPlugin(new AutoQueryFeature {
                MaxLimit = 1000,
                IncludeTotal = true,
            });
        });
}
