using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Hangfire.SqlServer;
using Hangfire;
using Hangfire.Dashboard;
using Owin;
using Microsoft.Owin;
using IMChatApp.App_Start;

[assembly: OwinStartup(typeof(IMChatApp.Startup))]

namespace IMChatApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
          //  app.UseHangfireServer();
            //var options = new BackgroundJobServerOptions();
            //JobStorage.Current = new SqlServerStorage("ConnectionStringName", options);
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(@"Server=SQL5018.Smarterasp.net;Database=DB_9E52D6_w424;User Id=DB_9E52D6_w424_admin;Password=qwerty@1234;")
                .UseMsmqQueues(@".\Private$\hangfire{0}", "default", "critical")
                .UseDashboardMetric(SqlServerStorage.ActiveConnections)
                .UseDashboardMetric(SqlServerStorage.TotalConnections)
                .UseDashboardMetric(DashboardMetrics.FailedCount);
            app.UseHangfireServer(); //
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
           // ConfigureAuth(app);
                // Make long polling connections wait a maximum of 110 seconds for a
            // response. When that time expires, trigger a timeout command and
            // make the client reconnect. 110-30-10
           // GlobalHost.Configuration.ConnectionTimeout = TimeSpan.FromSeconds(50);

            // Wait a maximum of 2 seconds after a transport connection is lost
            // before raising the Disconnected event to terminate the SignalR connection.
           // GlobalHost.Configuration.DisconnectTimeout = TimeSpan.FromSeconds(6);

            // For transports other than long polling, send a keepalive packet every
            // 10 seconds. 
            // This value must be no more than 1/3 of the DisconnectTimeout value.
           // GlobalHost.Configuration.KeepAlive = TimeSpan.FromSeconds(2);
            // Any connection or hub wire up and configuration should go here
            GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule()); 
            app.MapSignalR();
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
