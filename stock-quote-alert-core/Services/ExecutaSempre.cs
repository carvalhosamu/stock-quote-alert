using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using stock_quote_alert_core.Interfaces;
using stock_quote_alert_core.Models.Configuracoes;
using stock_quote_alert_core.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Services
{
    public class ExecutaSempre : ExecucaoBase<ExecutaSempre>, IExecucao
    {

        public ExecutaSempre(ICotacaoAPIService cotacaoApi,
                             IEmailService emailService,
                             IConsultaRepositorio consultaRepositorio,
                             ArgsModel args,
                             IOptions<ConfiguracaoServico> options,
                             ILogger<ExecutaSempre> logger,
                             IEmailRepositorio emailRepositorio) :
        base(cotacaoApi, emailService, consultaRepositorio, args, options, logger, emailRepositorio)
        {

        }

        public override Task<bool> ValidaEnvioEmail(Consultas acao)
        {
            return Task.FromResult(verificaEnvioDeEmail(acao));
        }

   

       
    }
}
