using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using stock_quote_alert_core.Interfaces;
using stock_quote_alert_core.Models.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace stock_quote_alert
{
    public class Worker : BackgroundService
    {

        private readonly IHostApplicationLifetime _lifetime;
        private readonly ILogger<Worker> _logger;
        private readonly IExecucao _execucao;
        private readonly IEmailRepositorio _emailRepository;
        private readonly IConsultaRepositorio _consultaRepository;
        private readonly ConfiguracaoServico _configuracao;


        public Worker(ILogger<Worker> logger,
                      IExecucao acaoService,
                      IHostApplicationLifetime lifetime,
                      IEmailRepositorio email,
                      IConsultaRepositorio consultaRepository,
                      IOptions<ConfiguracaoServico> configuracao
                      )
        {
           
            _lifetime = lifetime;
            _logger = logger;
            _execucao = acaoService;
            _emailRepository = email;
            _consultaRepository = consultaRepository;
            _configuracao = configuracao.Value;

        }

        public override async Task StartAsync(CancellationToken cancellationToken) 
        {
            await _emailRepository.DeletaRegistros();
            await _consultaRepository.DeletarRegistros();
            await base.StartAsync(cancellationToken);

            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _execucao.Exececutar();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Ocorreu um erro: " + ex.Message + "  " + ex.StackTrace);
                    _lifetime.StopApplication();
                }

                await Task.Delay(_configuracao.DelayPooling, stoppingToken);
            }
        }
    }
}
