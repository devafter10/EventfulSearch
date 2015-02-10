using System;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;
using Microsoft.Framework.Logging.Console;
using EventfulSearch.Models;
using EventfulSearch.Services;

namespace EventfulSearch
{
	public class Startup
	{
		// This method gets called by the runtime.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add MVC services to the services container.
			services.AddMvc();

			services.AddSingleton<IEventRepository, EventRepository>();

			services.AddSingleton<SearchHelper, SearchHelper>();

			services.AddTransient<IEventfulService, EventfulService>();

			services.AddTransient<IGoogleGeocodeService, GoogleGeocodeService>();

			services.AddTransient<IRestProxy, RestSharpProxy>();

			// Uncomment the following line to add Web API servcies which makes it easier to port Web API 2 controllers.
			// You need to add Microsoft.AspNet.Mvc.WebApiCompatShim package to project.json
			// services.AddWebApiConventions();

		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			// For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940

			// Configure the HTTP request pipeline.
			// Add the console logger.
			loggerFactory.AddConsole();

			// Add the following to the request pipeline only in development environment.
			if (string.Equals(env.EnvironmentName, "Development", StringComparison.OrdinalIgnoreCase))
			{
				app.UseBrowserLink();
				app.UseErrorPage(ErrorPageOptions.ShowAll);
			}
			else
			{
				// Add Error handling middleware which catches all application specific errors and
				// send the request to the following path or controller action.
				app.UseErrorHandler("/Home/Error");
			}

			// Add static files to the request pipeline.
			app.UseStaticFiles();

			// Add MVC to the request pipeline.
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller}/{action}/{id?}",
					defaults: new { controller = "Home", action = "Index" });

				// Uncomment the following line to add a route for porting Web API 2 controllers.
				// routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
			});

			// app.UseWelcomePage();
		}
	}
}
