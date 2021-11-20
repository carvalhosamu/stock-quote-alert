using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using stock_quote_alert.Contexto;
using stock_quote_alert.Interfaces;
using stock_quote_alert.Models;
using stock_quote_alert.Models.Configuracoes;
using stock_quote_alert.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Repositorios
{
    public class EnvioEmailRepositorio : IEmailRepositorio
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IOptions<EmailDestino> _emailDestino;
        private readonly ArgsModel _args;

        public EnvioEmailRepositorio( IServiceScopeFactory serviceScopeFactory, IOptions<EmailDestino> emailDestino, ArgsModel args)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _emailDestino = emailDestino;
            _args = args;
        }

        public async Task DeletaRegistros()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AcaoContext>();
                    dbContext.EnvioEmail.RemoveRange(dbContext.EnvioEmail);
                    await dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
           
        }


        public async Task<EnvioEmail> BuscaUltimoEmail()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AcaoContext>();
                    var result = await dbContext.EnvioEmail
                                                .Where(c => c.StatusEnviado == true)
                                                .OrderByDescending(c => c.DtaEnvio)
                                                .Include(c => c.Consulta)
                                                .FirstOrDefaultAsync();

                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
         
        }

       
        public async Task<EnvioEmail> SalvaEnvio(Consultas consulta, bool statusEnviado)
        {
            try
            {
                var emailEnnviado = new EnvioEmail
                {
                    DtaEnvio = DateTime.Now,
                    EmailEnviado = _emailDestino.Value.Email,
                    IdConsulta = consulta.IdConsulta,
                    Nome = _emailDestino.Value.Nome,
                    StatusEnviado = statusEnviado,
                    TipoEnvio = consulta.ValorApurado <= _args.PrecoMinimo ? "B" : "T"
                };

                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AcaoContext>();
                    var Result = await dbContext.AddAsync(emailEnnviado);
                    await dbContext.SaveChangesAsync();
                    return Result.Entity;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
