namespace MerchandiseService.Infrastructure.Configuration
{
    /// <summary>
    /// Настройки подключения к БД.
    /// </summary>
    public class DatabaseConnectionOptions
    {
        /// <summary>
        /// Строка подключения.
        /// </summary>
        /// <returns>string</returns>
        public string ConnectionString { get; set; }
    }
}
