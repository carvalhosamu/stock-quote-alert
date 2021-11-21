using FluentEmail.Core.Interfaces;
using FluentEmail.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using stock_quote_alert_core.Models.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Configuracoes
{
    public static class EmailConfiguration
    {
        public static void ConfiguraEmail(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection("ConfiguracaoEmail").Get<SMTPConfiguracao>();

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
        }
    }
}
