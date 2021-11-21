
using stock_quote_alert_core.Models;
using stock_quote_alert_core.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Interfaces
{
    public interface IConsultaRepositorio
    {
        Task<Consultas> AddAcao(AcaoModel acao);
        Task DeletarRegistros();

    }
}
