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
    public abstract class ExecucaoBase<T> 
    {
        protected readonly ICotacaoAPIService _cotacaoApi;
        protected readonly IEmailService _emailService;
        protected readonly IConsultaRepositorio _consultaRepositorio;
        protected readonly IEmailRepositorio _emailRepositorio;
        protected readonly ArgsModel _args;
        protected ConfiguracaoServico _config;
        protected readonly ILogger<T> _logger;
        protected readonly IConsultaRepositorio _repositorio;

        public ExecucaoBase(ICotacaoAPIService cotacaoApi,
                           IEmailService emailService,
                           IConsultaRepositorio consultaRepositorio,
                           ArgsModel args,
                           IOptions<ConfiguracaoServico> options,
                           ILogger<T> logger,
                           IConsultaRepositorio repositorio,
                           IEmailRepositorio emailRepositorio)
        {
            _cotacaoApi = cotacaoApi;
            _args = args;
            _emailService = emailService;
            _consultaRepositorio = consultaRepositorio;
            _logger = logger;
            _repositorio = repositorio;
            _config = options.Value;
            _emailRepositorio = emailRepositorio;
        }

        public bool verificaEnvioDeEmail(Consultas acao)
        {
            if (acao.ValorApurado > _args.PrecoMaximo ||
                acao.ValorApurado < _args.PrecoMinimo)
            {
                return true;
            }

            return false;
        }

        public abstract Task<bool> ValidaEnvioEmail(Consultas acao);

        public async Task EnviaEmail(Consultas acao)
        {
            var r = await _emailService.SendEmailAsnyc(acao);
            await _emailRepositorio.SalvaEnvio(acao, r);
        }

        public async Task<Consultas> getAcoes()
        {
            var result = await _cotacaoApi.GetActions(_args.Acao);
            var entidade = await _repositorio.AddAcao(result);

            return entidade;
        }

        public async Task Exececutar()
        {
            try
            {
                var result = await getAcoes();

                if (result.RetornouResultados)
                {
                    if (await ValidaEnvioEmail(result))
                    {
                        await EnviaEmail(result);
                    }
                }
                else
                {
                    _logger.LogError("Ocorreu um erro verifique nos arquivos de log");
                    throw new Exception("O Resultado da api foi nulo");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
