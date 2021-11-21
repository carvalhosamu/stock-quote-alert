using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Smtp;
using System.Net.Mail;
using System.Net;
using stock_quote_alert.Repositorios;
using Microsoft.EntityFrameworkCore;
using stock_quote_alert_repositorio.Contexto;
using stock_quote_alert_core.Interfaces;
using stock_quote_alert_core.Services;
using stock_quote_alert_core.Models.Configuracoes;
using System.IO;
using stock_quote_alert.Configuracoes;

namespace stock_quote_alert
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config
                   .SetBasePath(Environment.CurrentDirectory)
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                   .AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", optional: true);
                    
                    config.AddEnvironmentVariables();
                })

                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfiguraParametros(hostContext.Configuration);

                    services.ConfigureDatabase(hostContext.Configuration);

                    services.ConfiguraRegras(hostContext.Configuration);

                    services.ConfiguraInjecao();

                    services.ConfiguraEmail(hostContext.Configuration);

                    services.ConfiguraArgs(hostContext.Configuration, args);

                    services.AddHostedService<Worker>();

                   
                });
    }
}
