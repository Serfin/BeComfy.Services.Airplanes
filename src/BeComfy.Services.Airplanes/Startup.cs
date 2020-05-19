using System;
using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeComfy.Common.Consul;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.EFCore;
using BeComfy.Common.Jaeger;
using BeComfy.Common.Mongo;
using BeComfy.Common.RabbitMq;
//using BeComfy.MessageBroker.RabbitMQ;
using BeComfy.Services.Airplanes.Domain;
using BeComfy.Services.Airplanes.EF;
using BeComfy.Services.Airplanes.Messages.Commands;
using BeComfy.Services.Airplanes.Messages.Events;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeComfy.Services.Airplanes
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddJaeger();
            services.AddOpenTracing();
            services.AddConsul();
            services.AddMongo();
            //services.AddRabbitMq();
            services.AddMongoRepository<Airplane>("Airplanes");
            services.AddEFCoreContext<AirplanesContext>();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .AsImplementedInterfaces();

            builder.AddRabbitMq();
            builder.Populate(services);
            builder.AddHandlers();
            builder.AddDispatcher();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            IHostApplicationLifetime applicationLifetime, IConsulClient consulClient)
        {
            app.UseRouting();

            app.UseRabbitMq()
                .SubscribeCommand<CreateAirplane>();
                //.ConsumeMessage<CreateAirplane>();
            
            app.UseEndpoints(endpoints => 
            {
                endpoints.MapControllers();
            });

            var consulServiceId = app.UseConsul();
            applicationLifetime.ApplicationStopped.Register(
                () => consulClient.Agent.ServiceDeregister(consulServiceId)
            );
        }
    }
}