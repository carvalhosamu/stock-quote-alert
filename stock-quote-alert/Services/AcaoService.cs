using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using stock_quote_alert.Interfaces;
using stock_quote_alert.Models;
using stock_quote_alert.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Services
{
    public class AcaoService : IAcaoService
    {

        private readonly ICotacaoAPIService _cotacaoApi;
        private readonly IEmailService _emailService;
        private readonly IConsultaRepositorio _consultaRepositorio;
        private readonly IEmailRepositorio _emailRepositorio;
        private readonly ArgsViewModel _args;
        private ConfiguracaoServico _config;
        private readonly ILogger<AcaoService> _logger;
        private readonly IConsultaRepositorio _repositorio;

        public Dictionary<string, Func<Task>> acoes = new();

        public void CriaAcoes()
        {
            acoes.Add("SEMPRE", Todos);
            acoes.Add("DIFERENCAVALOR", EnvioDiferenca);
            acoes.Add("UMAVEZ", UmaVez);
        }

        public AcaoService(ICotacaoAPIService cotacaoApi,
                           IEmailService emailService,
                           IConsultaRepositorio consultaRepositorio,
                           ArgsViewModel args,
                           IOptions<ConfiguracaoServico> options,
                           ILogger<AcaoService> logger,
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

            CriaAcoes();
        }

        public async Task VerificaAcao()
        {
            // _config.ValorDiferencaEnvio = _config.TipoEnvio == "DIFERENCAVALOR" ? _config.ValorDiferencaEnvio : 0;
            await acoes[_config.TipoEnvio].Invoke();
        }

        public bool verificaEnvioDeEmail(Consultas acao)
        {
            if (acao.ValorApurado > _args.PrecoMaximo ||
                acao.ValorApurado < _args.PrecoMinimo )
            {
                return true;
            }

            return false;
        }

        public async Task<bool> verificaEnvioDeEmailIntervalo(Consultas acao, double diferenca)
        {
            try
            {
                var result = await _emailRepositorio.BuscaUltimoEmail();

                if (result == null)
                {
                    return verificaEnvioDeEmail(acao);
                }

                else if (acao.ValorApurado > result.Consulta.ValorApurado + diferenca ||
                         acao.ValorApurado < result.Consulta.ValorApurado - diferenca )
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

        public async Task<bool> VerificaEmailUmaVez(Consultas acao, double diferenca)
        {
            try
            {
                var result = await _emailRepositorio.BuscaUltimoEmail();

                if (result == null)
                {
                    return verificaEnvioDeEmail(acao);
                }
                else if (result.TipoEnvio == "T" && acao.ValorApurado <= _args.PrecoMinimo ||
                         result.TipoEnvio == "B" && acao.ValorApurado >= _args.PrecoMaximo)
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


        public async Task EnvioDiferenca()
        {
            try
            {
                var result = await getAcoes();
                
                if (result.RetornouResultados)
                {
                    if (await verificaEnvioDeEmailIntervalo(result, _config.ValorDiferencaEnvio))
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

        public async Task Todos()
        {
            try
            {
                var result = await getAcoes();

                if (result.RetornouResultados)
                {
                    if (verificaEnvioDeEmail(result))
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


        public async Task UmaVez()
        {
            try
            {
                var result = await getAcoes();

                if (result.RetornouResultados)
                {
                    if (verificaEnvioDeEmail(result))
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
