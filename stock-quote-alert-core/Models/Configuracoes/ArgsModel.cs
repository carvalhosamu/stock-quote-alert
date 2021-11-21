using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Models.Configuracoes
{
    public class ArgsModel
    {
        public double PrecoMinimo { get; set; }
        public double PrecoMaximo { get; set; }
        public string Acao { get; set; }
    }
}
