using MultiplayerSignalRHubClasses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace OnlineGameServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) //no web api for azure.
        {
            services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 72428800;
            }); //don't use core. core is only bare necessities.
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MultiplayerConnectionHub>("/hubs/gamepackage/messages", options =>
                {
                    options.TransportMaxBufferSize = 72428800;
                    options.ApplicationMaxBufferSize = 72428800;
                });
            });
        }
    }
}