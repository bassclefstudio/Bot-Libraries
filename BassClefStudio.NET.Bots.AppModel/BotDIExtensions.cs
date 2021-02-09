using Autofac;
using Autofac.Extensions.DependencyInjection;
using BassClefStudio.NET.Bots.Commands;
using BassClefStudio.NET.Bots.Inline;
using BassClefStudio.NET.Bots.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BassClefStudio.NET.Bots.AppModel
{
    /// <summary>
    /// Provides extension methods for the <see cref="ContainerBuilder"/> when creating a bot client.
    /// </summary>
    public static class BotDIExtensions
    {
        /// <summary>
        /// Registers to an Autofac <see cref="ContainerBuilder"/> a static <see cref="Bot"/> with the provided type of <see cref="IBotService"/>, as well as all known <see cref="IBotCommand"/>s, etc. in the app's assemblies.
        /// </summary>
        /// <typeparam name="TService">The type of the <see cref="IBotService"/> to register.</typeparam>
        /// <param name="builder">The Autofac <see cref="ContainerBuilder"/> in question.</param>
        /// <param name="commandAssemblies">A collection of <see cref="Assembly"/> references from which to pull <see cref="IBotCommand"/>s and other bot handlers.</param>
        public static void RegisterBotConfiguration<TService>(this ContainerBuilder builder, Assembly[] commandAssemblies)
            where TService : IBotService
        {
            //// Bot registered types.
            builder.RegisterAssemblyTypes(commandAssemblies)
                .AssignableTo<IBotCommand>()
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(commandAssemblies)
                .AssignableTo<IInlineHandler>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<Bot>()
                .SingleInstance();
            builder.RegisterType<TService>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }

        /// <summary>
        /// Registers to an Autofac <see cref="ContainerBuilder"/> a static <see cref="Bot"/> with the provided type of <see cref="IBotService"/>, as well as all known <see cref="IBotCommand"/>s, etc. in the app's assemblies and API configuration using the <see cref="IOptions{TOptions}"/> interface.
        /// </summary>
        /// <typeparam name="TService">The type of the <see cref="IBotService"/> to register.</typeparam>
        /// <typeparam name="TOptions">The (reference) type of the required API configuration needed to initialize a <typeparamref name="TService"/> <see cref="IBotService"/>.</typeparam>
        /// <param name="builder">The Autofac <see cref="ContainerBuilder"/> in question.</param>
        /// <param name="commandAssemblies">A collection of <see cref="Assembly"/> references from which to pull <see cref="IBotCommand"/>s and other bot handlers.</param>
        /// <param name="configName">The relative path to the JSON config file for the application (or one which will simply contain bot API configuration).</param>
        /// <param name="sectionName">The section in that JSON file where the API config - properties for <typeparamref name="TOptions"/> - can be found.</param>
        public static void RegisterBotConfiguration<TService, TOptions>(this ContainerBuilder builder, Assembly[] commandAssemblies, string configName = "appsettings.json", string sectionName = "BotApi")
            where TService : IBotService where TOptions : class
        {
            builder.RegisterBotConfiguration<TService>(commandAssemblies);

            //// Secrets - appsettings.json
            var configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile(configName);
            IConfiguration config = configBuilder.Build();

            IServiceCollection services = new ServiceCollection();
            services.Configure<TOptions>(config.GetSection(sectionName));
            builder.Populate(services);

            //// Quick-and-dirty setup for BotService constructor.
            builder.RegisterAdapter<IOptions<TOptions>, TOptions>(o => o.Value);
        }
    }
}
