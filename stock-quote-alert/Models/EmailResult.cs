using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Models
{
    public class EmailModel
    {
        public string Nome { get; set; }
        public string NomeAcao { get; set; }
        public double Preco { get; set; }
        public string MinOuMax { get; set; }

    }
}
