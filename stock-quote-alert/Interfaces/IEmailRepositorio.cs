﻿using stock_quote_alert.Models;
using stock_quote_alert.Models.Tabelas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Interfaces
{
    public interface IEmailRepositorio
    {
        public Task<EnvioEmail> SalvaEnvio(Consultas consulta, bool statusEnviado);

        public Task<EnvioEmail> BuscaUltimoEmail();

        public Task DeletaRegistros();
    }
}
