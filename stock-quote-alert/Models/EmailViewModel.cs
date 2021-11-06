using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Models
{
    public class EmailViewModel
    {
        public string EmailEnvio { get; set; }
        public string NomeEnvio { get; set; }
        public string Host { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Path { get; set; }



    }
}
