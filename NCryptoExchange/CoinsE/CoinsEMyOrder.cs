using Lostics.NCryptoExchange.Model;
using Newtonsoft.Json.Linq;
using System;

namespace Lostics.NCryptoExchange.CoinsE
{
    public class CoinsEMyOrder : MyOrder
    {
        private CoinsEMyOrder(CoinsEOrderId orderId, OrderType orderType,
            DateTime created, decimal price, decimal quantity, decimal originalQuantity,
            CoinsEMarketId marketId) : base(orderId, orderType, created, price, quantity, originalQuantity, marketId)
        {
        }

        public static CoinsEMyOrder Parse(JObject json)
        {
            DateTime dateTime = CoinsEParsers.ParseTime(json.Value<int>("created"));
            CoinsEMarketId marketId = new CoinsEMarketId(json.Value<string>("pair"));

            return new CoinsEMyOrder(new CoinsEOrderId(marketId, json.Value<string>("id")),
                CoinsEParsers.ParseOrderType(json.Value<string>("order_type")), dateTime,
                json.Value<decimal>("rate"), json.Value<decimal>("quantity_remaining"), json.Value<decimal>("quantity"),
                marketId
            )
            {
                IsOpen = json.Value<bool>("is_open"),
                Status = CoinsEParsers.ParseOrderStatus(json.Value<string>("status"))
            };
        }

        public bool IsOpen { get; private set; }
        public CoinsEOrderStatus Status { get; private set; }
    }
}
