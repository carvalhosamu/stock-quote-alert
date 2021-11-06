using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Models
{
    public class ConfiguracaoServico
    {
        public string TipoDelay { get; set; }
        public long DelayPooling { get; set; }
        public double ValorDiferencaEnvio { get; set; }
        public string TipoEnvio { get; set; }
    }
}
