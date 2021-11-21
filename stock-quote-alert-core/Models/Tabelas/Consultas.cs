using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Models.Tabelas
{
    public class Consultas
    {
        public long IdConsulta { get; set; }
        public string NomeAcao { get; set; }
        public DateTime DtaExecucao { get; set; }
        public double ValorApurado { get; set; }
        public bool? MercadoAberto { get; set; }
        public bool RetornouResultados { get; set; }
        public EnvioEmail Email { get; set; }
    }
}
