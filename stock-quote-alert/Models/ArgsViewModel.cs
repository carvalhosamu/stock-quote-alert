﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Models
{
    public class ArgsViewModel
    {
        public double PrecoMinimo { get; set; }
        public double PrecoMaximo { get; set; }
        public string Acao { get; set; }
    }
}
