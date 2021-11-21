using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert_core.Interfaces
{
    public interface IAcaoService
    {
        public Task VerificaAcao();
    }
}
