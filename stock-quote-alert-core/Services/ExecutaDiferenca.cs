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
    public class ExecutaDiferenca : ExecucaoBase<ExecutaDiferenca>, IExecucao
    {
        public ExecutaDiferenca(ICotacaoAPIService cotacaoApi,
                           IEmailService emailService,
                           IConsultaRepositorio consultaRepositorio,
                           ArgsModel args,
                           IOptions<ConfiguracaoServico> options,
                           ILogger<ExecutaDiferenca> logger,
      
                           IEmailRepositorio emailRepositorio) :
      base(cotacaoApi, emailService, consultaRepositorio, args, options, logger, emailRepositorio)
        {

        }

        public override async Task<bool> ValidaEnvioEmail(Consultas acao)
        {
            try
            {
                var result = await _emailRepositorio.BuscaUltimoEmail();

                if (result == null)
                {
                    return verificaEnvioDeEmail(acao);
                }

                else if (verificaEnvioDeEmail(acao) &&
                        (acao.ValorApurado > result.Consulta.ValorApurado + result.Consulta.ValorApurado || acao.ValorApurado < result.Consulta.ValorApurado - result.Consulta.ValorApurado))
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
