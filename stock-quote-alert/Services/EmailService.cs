using FluentEmail.Core;
using Microsoft.Extensions.Options;
using stock_quote_alert.Interfaces;
using stock_quote_alert.Models;
using stock_quote_alert.Models.Configuracoes;
using stock_quote_alert.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Services
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
                                     .UsingTemplateFromFile("./Templates/AlertaAcao.cshtml", CriaEmail(consulta))
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
