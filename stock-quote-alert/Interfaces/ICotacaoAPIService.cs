using stock_quote_alert.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Interfaces
{
    public interface ICotacaoAPIService
    {
        public Task<AcaoViewModel> GetActions(string quoteName);
    }
}
