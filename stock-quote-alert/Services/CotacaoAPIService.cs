using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using stock_quote_alert.Interfaces;
using stock_quote_alert.Models;
using stock_quote_alert.Models.Configuracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Services
{
    public class CotacaoAPIService : ICotacaoAPIService
    {
        private readonly ILogger<CotacaoAPIService> _logger;
        public readonly ApiConfiguration _apiConfiguration;
        
        
        public CotacaoAPIService(ILogger<CotacaoAPIService> logger, IOptions<ApiConfiguration> apiConfiguration)
        {
            _logger = logger;
             _apiConfiguration = apiConfiguration.Value;
        }

        private RestRequest ConfiguraHeaders(RestRequest request)
        {
            request.AddHeader("acept", "application/json");
            request.AddHeader("X-API-KEY", _apiConfiguration.ApiKey);
            return request;
        }

        public async Task<AcaoModel> GetActions(string actionName)
        {
            try
            {
                var client = new RestClient($"{_apiConfiguration.ApiUrl}/quote?region=US&lang=en&symbols={actionName}.SA")
                                            .UseNewtonsoftJson();
                
                var request = new RestRequest(Method.GET);
                request = ConfiguraHeaders(request);

                IRestResponse<AcaoModel> response = await client.ExecuteAsync<AcaoModel>(request);

                return response.Data;

            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}   {ex.InnerException}   {ex.StackTrace}");
                throw;
            }
        }
    }
}
