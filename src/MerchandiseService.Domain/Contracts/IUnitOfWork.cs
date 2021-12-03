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
        Task StartTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Завершить транзакцию.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохранить изменения.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены.</param>
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}