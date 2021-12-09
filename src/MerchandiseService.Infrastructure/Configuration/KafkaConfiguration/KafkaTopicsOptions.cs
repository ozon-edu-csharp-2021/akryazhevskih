namespace MerchandiseService.Infrastructure.Configuration.KafkaConfiguration
{
    /// <summary>
    /// Конфигурация топиков кафки
    /// </summary>
    public class KafkaTopicsOptions
    {
        public string StockReplenishedTopic { get; set; }

        public string EmailNotificationTopic { get; set; }

        public string IssueMerchTopic { get; set; }
    }
}
