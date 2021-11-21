﻿using Microsoft.Extensions.Logging;
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
    public class ExecutaUmaVez : ExecucaoBase<ExecutaUmaVez>, IExecucao
    {
        public ExecutaUmaVez(ICotacaoAPIService cotacaoApi,
                             IEmailService emailService,
                             IConsultaRepositorio consultaRepositorio,
                             ArgsModel args,
                             IOptions<ConfiguracaoServico> options,
                             ILogger<ExecutaUmaVez> logger,
                             IConsultaRepositorio repositorio,
                             IEmailRepositorio emailRepositorio) :
        base(cotacaoApi, emailService, consultaRepositorio, args, options, logger, repositorio, emailRepositorio)
        {

        }

        public async override Task<bool> ValidaEnvioEmail(Consultas acao)
        {
            try
            {
                var result = await _emailRepositorio.BuscaUltimoEmail();

                if (result == null)
                {
                    return verificaEnvioDeEmail(acao);
                }
                else if (verificaEnvioDeEmail(acao) && (   result.TipoEnvio == "T" && acao.ValorApurado <= _args.PrecoMinimo ||
                         result.TipoEnvio == "B" && acao.ValorApurado >= _args.PrecoMaximo))
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
