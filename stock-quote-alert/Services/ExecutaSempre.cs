using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using stock_quote_alert.Interfaces;
using stock_quote_alert.Models.Configuracoes;
using stock_quote_alert.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Services
{
    public class ExecutaSempre : ExecucaoBase<ExecutaSempre>, IExecucao
    {

        public ExecutaSempre(ICotacaoAPIService cotacaoApi,
                             IEmailService emailService,
                             IConsultaRepositorio consultaRepositorio,
                             ArgsModel args,
                             IOptions<ConfiguracaoServico> options,
                             ILogger<ExecutaSempre> logger,
                             IConsultaRepositorio repositorio,
                             IEmailRepositorio emailRepositorio) :
        base(cotacaoApi, emailService, consultaRepositorio, args, options, logger, repositorio, emailRepositorio)
        {

        }

        public override Task<bool> ValidaEnvioEmail(Consultas acao)
        {
            return Task.FromResult(verificaEnvioDeEmail(acao));
        }

   

       
    }
}
