using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Models.Configuracoes
{
    public class ConfiguracaoServico
    {
        public int DelayPooling { get; set; }
        public double ValorDiferencaEnvio { get; set; }
        public string TipoEnvio { get; set; }
    }
}
