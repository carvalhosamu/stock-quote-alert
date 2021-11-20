using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_quote_alert.Models
{
    /// <summary>
    ///  Classe que representa o retorno vindo da API do Yahoo 
    /// </summary>
    public class AcaoModel
    {
        [JsonProperty("quoteResponse")]
        public QuoteResponse QuoteResponse { get; set; }
    }

    public class QuoteResponse
    {
        [JsonProperty("result")]
        public Result[] Result { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class Result
    {
        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("quoteType")]
        public string QuoteType { get; set; }

        [JsonProperty("quoteSourceName")]
        public string QuoteSourceName { get; set; }

        [JsonProperty("triggerable")]
        public bool Triggerable { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("longName")]
        public string LongName { get; set; }

        [JsonProperty("messageBoardId")]
        public string MessageBoardId { get; set; }

        [JsonProperty("exchangeTimezoneName")]
        public string ExchangeTimezoneName { get; set; }

        [JsonProperty("exchangeTimezoneShortName")]
        public string ExchangeTimezoneShortName { get; set; }

        [JsonProperty("gmtOffSetMilliseconds")]
        public long GmtOffSetMilliseconds { get; set; }

        [JsonProperty("market")]
        public string Market { get; set; }

        [JsonProperty("marketState")]
        public string MarketState { get; set; }

        [JsonProperty("firstTradeDateMilliseconds")]
        public long FirstTradeDateMilliseconds { get; set; }

        [JsonProperty("earningsTimestamp")]
        public long? EarningsTimestamp { get; set; }

        [JsonProperty("earningsTimestampStart")]
        public long? EarningsTimestampStart { get; set; }

        [JsonProperty("earningsTimestampEnd")]
        public long? EarningsTimestampEnd { get; set; }

        [JsonProperty("trailingAnnualDividendRate")]
        public double? TrailingAnnualDividendRate { get; set; }

        [JsonProperty("trailingPE")]
        public double? TrailingPe { get; set; }

        [JsonProperty("trailingAnnualDividendYield")]
        public double? TrailingAnnualDividendYield { get; set; }

        [JsonProperty("epsTrailingTwelveMonths")]
        public double? EpsTrailingTwelveMonths { get; set; }

        [JsonProperty("epsForward")]
        public double? EpsForward { get; set; }

        [JsonProperty("epsCurrentYear")]
        public double? EpsCurrentYear { get; set; }

        [JsonProperty("priceEpsCurrentYear")]
        public double? PriceEpsCurrentYear { get; set; }

        [JsonProperty("sharesOutstanding")]
        public long? SharesOutstanding { get; set; }

        [JsonProperty("bookValue")]
        public double? BookValue { get; set; }

        [JsonProperty("fiftyDayAverage")]
        public double FiftyDayAverage { get; set; }

        [JsonProperty("fiftyDayAverageChange")]
        public double FiftyDayAverageChange { get; set; }

        [JsonProperty("fiftyDayAverageChangePercent")]
        public double FiftyDayAverageChangePercent { get; set; }

        [JsonProperty("twoHundredDayAverage")]
        public double TwoHundredDayAverage { get; set; }

        [JsonProperty("twoHundredDayAverageChange")]
        public double TwoHundredDayAverageChange { get; set; }

        [JsonProperty("twoHundredDayAverageChangePercent")]
        public double TwoHundredDayAverageChangePercent { get; set; }

        [JsonProperty("marketCap")]
        public long? MarketCap { get; set; }

        [JsonProperty("forwardPE")]
        public double? ForwardPe { get; set; }

        [JsonProperty("priceToBook")]
        public double? PriceToBook { get; set; }

        [JsonProperty("sourceInterval")]
        public long SourceInterval { get; set; }

        [JsonProperty("exchangeDataDelayedBy")]
        public long ExchangeDataDelayedBy { get; set; }

        [JsonProperty("averageAnalystRating")]
        public string AverageAnalystRating { get; set; }

        [JsonProperty("tradeable")]
        public bool Tradeable { get; set; }

        [JsonProperty("esgPopulated")]
        public bool EsgPopulated { get; set; }

        [JsonProperty("priceHint")]
        public long PriceHint { get; set; }

        [JsonProperty("postMarketChangePercent")]
        public double? PostMarketChangePercent { get; set; }

        [JsonProperty("postMarketTime")]
        public long? PostMarketTime { get; set; }

        [JsonProperty("postMarketPrice")]
        public double? PostMarketPrice { get; set; }

        [JsonProperty("postMarketChange")]
        public double? PostMarketChange { get; set; }

        [JsonProperty("regularMarketChange")]
        public double RegularMarketChange { get; set; }

        [JsonProperty("regularMarketChangePercent")]
        public double RegularMarketChangePercent { get; set; }

        [JsonProperty("regularMarketTime")]
        public long RegularMarketTime { get; set; }

        [JsonProperty("regularMarketPrice")]
        public double RegularMarketPrice { get; set; }

        [JsonProperty("regularMarketDayHigh")]
        public double RegularMarketDayHigh { get; set; }

        [JsonProperty("regularMarketDayRange")]
        public string RegularMarketDayRange { get; set; }

        [JsonProperty("regularMarketDayLow")]
        public double RegularMarketDayLow { get; set; }

        [JsonProperty("regularMarketVolume")]
        public long RegularMarketVolume { get; set; }

        [JsonProperty("regularMarketPreviousClose")]
        public double RegularMarketPreviousClose { get; set; }

        [JsonProperty("bid")]
        public double? Bid { get; set; }

        [JsonProperty("ask")]
        public double? Ask { get; set; }

        [JsonProperty("bidSize")]
        public long? BidSize { get; set; }

        [JsonProperty("askSize")]
        public long? AskSize { get; set; }

        [JsonProperty("fullExchangeName")]
        public string FullExchangeName { get; set; }

        [JsonProperty("financialCurrency")]
        public string FinancialCurrency { get; set; }

        [JsonProperty("regularMarketOpen")]
        public double RegularMarketOpen { get; set; }

        [JsonProperty("averageDailyVolume3Month")]
        public long AverageDailyVolume3Month { get; set; }

        [JsonProperty("averageDailyVolume10Day")]
        public long AverageDailyVolume10Day { get; set; }

        [JsonProperty("fiftyTwoWeekLowChange")]
        public double FiftyTwoWeekLowChange { get; set; }

        [JsonProperty("fiftyTwoWeekLowChangePercent")]
        public double FiftyTwoWeekLowChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekRange")]
        public string FiftyTwoWeekRange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChange")]
        public double FiftyTwoWeekHighChange { get; set; }

        [JsonProperty("fiftyTwoWeekHighChangePercent")]
        public double FiftyTwoWeekHighChangePercent { get; set; }

        [JsonProperty("fiftyTwoWeekLow")]
        public double FiftyTwoWeekLow { get; set; }

        [JsonProperty("fiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh { get; set; }

        [JsonProperty("dividendDate")]
        public long? DividendDate { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("circulatingSupply")]
        public long? CirculatingSupply { get; set; }

        [JsonProperty("lastMarket")]
        public string LastMarket { get; set; }

        [JsonProperty("volume24Hr")]
        public long? Volume24Hr { get; set; }

        [JsonProperty("volumeAllCurrencies")]
        public long? VolumeAllCurrencies { get; set; }

        [JsonProperty("fromCurrency")]
        public string FromCurrency { get; set; }

        [JsonProperty("toCurrency")]
        public string ToCurrency { get; set; }

        [JsonProperty("startDate")]
        public long? StartDate { get; set; }

        [JsonProperty("coinImageUrl")]
        public Uri CoinImageUrl { get; set; }
    }
}
