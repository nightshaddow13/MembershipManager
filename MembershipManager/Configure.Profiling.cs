[assembly: HostingStartup(typeof(ConfigureProfiling))]

namespace MembershipManager;

public class ConfigureProfiling : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => {
            if (context.HostingEnvironment.IsDevelopment())
            {
                services.AddPlugin(new RequestLogsFeature
                {
                    EnableResponseTracking = true,
                });

                services.AddPlugin(new ProfilingFeature
                {
                    IncludeStackTrace = true,
                });
            }
        });
}
