using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using stock_quote_alert.Contexto;
using stock_quote_alert.Interfaces;
using stock_quote_alert.Models;
using stock_quote_alert.Services;
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
        private readonly IAcaoService _acaoService;
        private readonly IEmailRepositorio _emailRepository;
        private readonly IConsultaRepositorio _consultaRepository;


        public Worker(ILogger<Worker> logger,
                      IAcaoService acaoService,
                      IHostApplicationLifetime lifetime,
                      IEmailRepositorio email,
                      IConsultaRepositorio consultaRepository
                      )
        {
           
            _lifetime = lifetime;
            _logger = logger;
            _acaoService = acaoService;
            _emailRepository = email;
            _consultaRepository = consultaRepository;

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
                    await _acaoService.VerificaAcao();
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Ocorreu um erro: " + ex.Message + "  " + ex.StackTrace);
                    _lifetime.StopApplication();
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
