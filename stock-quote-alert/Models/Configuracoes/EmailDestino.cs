using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Models.Configuracoes
{
    public class EmailDestino
    {
        public string Email { get; set; }
        public string Assunto { get; set; }
        public string Nome { get; set; }
    }
}
