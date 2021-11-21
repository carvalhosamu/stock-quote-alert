using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Models.Tabelas
{
    public class EnvioEmail
    {
        public long IdEmail { get; set; }
        public long IdConsulta { get; set; }
        public string EmailEnviado { get; set; }
        public string Nome { get; set; }
        public string TipoEnvio { get; set; }
        public DateTime DtaEnvio { get; set; }
        public bool StatusEnviado { get; set; }
        public Consultas Consulta { get; set; }

    }
}
