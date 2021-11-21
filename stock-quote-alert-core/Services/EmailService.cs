using FluentEmail.Core;
using Microsoft.Extensions.Options;
using stock_quote_alert_core.Interfaces;
using stock_quote_alert_core.Models;
using stock_quote_alert_core.Models.Configuracoes;
using stock_quote_alert_core.Models.Tabelas;
using System;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly EmailDestino _emailDestino;
        private readonly ArgsModel _args;

        public EmailService(IFluentEmail fluentEmail, IOptions<EmailDestino> emailDestino, ArgsModel args)
        {
            _fluentEmail = fluentEmail;
            _emailDestino = emailDestino.Value;
            _args = args;

        }

        private EmailModel CriaEmail(Consultas consulta)
        {
            return new EmailModel {
                Nome = _emailDestino.Nome,
                MinOuMax = consulta.ValorApurado <= _args.PrecoMinimo ? "mínimo" : "máximo",
                NomeAcao = consulta.NomeAcao,
                Preco = consulta.ValorApurado
            };
        }



        public async Task<bool> SendEmailAsnyc(Consultas consulta)
        {
            try
            {
                var email = await _fluentEmail
                                     .To(_emailDestino.Email)
                                     .Subject(_emailDestino.Assunto)
                                     .UsingTemplateFromFile("../stock-quote-alert-core/Templates/AlertaAcao.cshtml", CriaEmail(consulta))
                                     .SendAsync();

                if (email.Successful)
                    return true;

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

     
    }
}
