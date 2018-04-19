using System;
using Autofac;
using Host.Api.IoC;
using Host.Api.Middlewares;
using Host.Api.Requests.Commands;
using Infrastructure.Domain.IoC.Events;
using Infrastructure.Domain.IoC.Validatiors;
using Infrastructure.IoC.AutoFac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Api
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            var co = ApplicationContainer;
            var x = services.AddAutoFac(out co, RegisterTypes);
            ApplicationContainer = co;
            return x;
        }

        private void RegisterTypes(ContainerBuilder builder)
        {
            builder.RegisterDomainValidators();
            builder.RegisterDomainEvents();
            builder.RegisterRepositories();
            builder.RegisterServices();
            builder.RegisterApiCommands();
            builder.RegisterApiBus(Configuration);

            //builder.RegisterType<MyType>().As<IMyType>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseUnhandledErrorMiddlewareExtension();
            app.UseMvc();

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}
