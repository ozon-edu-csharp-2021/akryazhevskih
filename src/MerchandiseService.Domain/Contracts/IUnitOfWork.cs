using System;
using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Domain.Contracts
{
    /// <summary>
    /// Единица работы.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Начать транзакцию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task StartTransaction(CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохранить изменения и завершить транзакцию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}