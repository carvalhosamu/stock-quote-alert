using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using stock_quote_alert.Interfaces;
using stock_quote_alert.Models;
using stock_quote_alert.Services;
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
using stock_quote_alert.Contexto;
using Microsoft.EntityFrameworkCore;
using stock_quote_alert.Models.Configuracoes;

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
                    #region Injeção das configurações 
                    services.Configure<ApiConfiguration>(hostContext.Configuration.GetSection("ApiConfiguration"));
                    services.Configure<SMTPConfiguracao>(hostContext.Configuration.GetSection("ConfiguracaoEmail"));
                    services.Configure<EmailDestino>(hostContext.Configuration.GetSection("EmailDestino"));
                    services.Configure<ConfiguracaoServico>(hostContext.Configuration.GetSection("ConfiguracaoServico"));
                    #endregion


                    services.AddDbContext<AcaoContext>(options =>
                        options.UseSqlite(hostContext.Configuration.GetValue<string>("connectionString")));

                    #region Injeção de dependência 

                    string teste = $"stock_quote_alert.Services.Executa{hostContext.Configuration.GetSection("ConfiguracaoServico").Get<ConfiguracaoServico>().TipoEnvio}";

                    Type elementType = Type.GetType(teste);

                    services.AddSingleton<ICotacaoAPIService, CotacaoAPIService>();
                    services.AddSingleton<IEmailService, EmailService>();

                    services.AddSingleton(typeof(IExecucao), elementType);

                    services.AddSingleton<IConsultaRepositorio, ConsultasRepositorio>();
                    services.AddSingleton<IEmailRepositorio, EnvioEmailRepositorio>();
                    #endregion

                    #region Configuracao Email

                    var emailSettings = hostContext.Configuration.GetSection("ConfiguracaoEmail").Get<SMTPConfiguracao>();

                    services.AddFluentEmail(emailSettings.EmailEnvio, emailSettings.NomeEnvio)
                            .AddRazorRenderer();
                        //      .AddSmtpSender(emailSettings.Host, emailSettings.Porta, emailSettings.Usuario, emailSettings.Senha);

                    services.AddSingleton<ISender>(x => new SmtpSender(new SmtpClient
                    {
                        Host = emailSettings.Host,
                        Port = emailSettings.Porta,
                        Credentials = new NetworkCredential(emailSettings.Usuario, emailSettings.Senha),
                        EnableSsl = true
                    }));


                    #endregion

                    #region Argumentos

                    var argsViewModel = new ArgsModel();

                    try
                    {
                        if (args.Length >= 3)
                        {
                            argsViewModel = new ArgsModel
                            {
                                Acao = args[0],
                                PrecoMinimo = double.Parse(args[1], CultureInfo.InvariantCulture),
                                PrecoMaximo = double.Parse(args[2], CultureInfo.InvariantCulture)
                            };
                        }
                        else
                        {
                            argsViewModel = new ArgsModel
                            {
                                Acao = hostContext.Configuration.GetValue<string>("NomeAcao"),
                                PrecoMinimo = hostContext.Configuration.GetValue<double>("PrecoMinimo"),
                                PrecoMaximo = hostContext.Configuration.GetValue<double>("PrecoMaximo"),
                            };
                        }


                    }
                    catch (Exception)
                    {

                       argsViewModel = new ArgsModel
                        {
                            Acao = hostContext.Configuration.GetValue<string>("NomeAcao"),
                            PrecoMinimo = hostContext.Configuration.GetValue<double>("PrecoMinimo"),
                            PrecoMaximo = hostContext.Configuration.GetValue<double>("PrecoMaximo"),
                        };
                    }

                    services.AddSingleton(argsViewModel);

                    #endregion

                    services.AddHostedService<Worker>();

                   
                });
    }
}
