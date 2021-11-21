using Microsoft.Extensions.DependencyInjection;
using stock_quote_alert_core.Interfaces;
using stock_quote_alert_core.Models;
using stock_quote_alert_core.Models.Configuracoes;
using stock_quote_alert_core.Models.Tabelas;
using stock_quote_alert_repositorio.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Repositorios
{
    public class ConsultasRepositorio : IConsultaRepositorio
    {
        public readonly ArgsModel _args;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ConsultasRepositorio( ArgsModel args, IServiceScopeFactory serviceScopeFactory)
        {
            _args = args;
            _serviceScopeFactory = serviceScopeFactory;
        }


        public async Task<Consultas> AddAcao(AcaoModel acao)
        {
            try
            {
               

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AcaoContext>();
                    Consultas consulta;

                    if (acao != null && acao.QuoteResponse != null && acao.QuoteResponse.Result.Length > 0)
                    {
                        consulta = new Consultas
                        {
                            DtaExecucao = DateTime.Now,
                            MercadoAberto = acao.QuoteResponse.Result[0].MarketState == "CLOSED" ? false : true,
                            NomeAcao = acao.QuoteResponse.Result[0].Symbol,
                            RetornouResultados = true,
                            ValorApurado = acao.QuoteResponse.Result[0].RegularMarketPrice

                        };
                    }

                    else
                    {
                        consulta = new Consultas
                        {
                            DtaExecucao = DateTime.Now,
                            MercadoAberto = null,
                            NomeAcao = _args.Acao,
                            RetornouResultados = false,
                            ValorApurado = 0
                        };
                    }

                    var result = await dbContext.Consultas.AddAsync(consulta);
                    await dbContext.SaveChangesAsync();

                    return result.Entity;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeletarRegistros()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AcaoContext>();
                    dbContext.Consultas.RemoveRange(dbContext.Consultas);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
