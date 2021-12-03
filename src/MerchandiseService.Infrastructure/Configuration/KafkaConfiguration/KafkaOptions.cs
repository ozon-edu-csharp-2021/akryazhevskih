namespace MerchandiseService.Infrastructure.Configuration.KafkaConfiguration
{
    /// <summary>
    /// Конфигурация кафки
    /// </summary>
    public class KafkaOptions
    {
        /// <summary>
        /// Идентификатор ConsumerGroup
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// Конфигурация топиков кафки
        /// </summary>
        public KafkaTopicsOptions Topics { get; set; }

        /// <summary>
        /// Адрес сервера кафки
        /// </summary>
        public string BootstrapServers { get; set; }
    }
}
