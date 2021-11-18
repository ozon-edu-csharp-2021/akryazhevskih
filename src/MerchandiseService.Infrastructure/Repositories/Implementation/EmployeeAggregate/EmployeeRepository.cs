using Dapper;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Contracts;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using MerchandiseService.Infrastructure.Repositories.Models;
using Npgsql;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MerchandiseService.Infrastructure.Repositories.Implementation.EmployeeAggregate
{
    internal class EmployeeRepository : IEmployeeRepository
    {
        private readonly IUnitOfWork _context;
        private readonly IDbConnectionFactory<NpgsqlConnection> _connectionFactory;
        private readonly IChangeTracker _changeTracker;

        public EmployeeRepository(
            IUnitOfWork context,
            IDbConnectionFactory<NpgsqlConnection> connectionFactory,
            IChangeTracker changeTracker)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Cannot be null");
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory), "Cannot be null");
            _changeTracker = changeTracker ?? throw new ArgumentNullException(nameof(changeTracker), "Cannot be null");
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<Employee> CreateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var query = @"
                INSERT INTO employees (id, size, email)
                VALUES (@id, @size, @email);";

            var parameters = new
            {
                id = employee.Id,
                size = employee.Size.Id,
                email = employee.Email.Value
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            await connection.ExecuteAsync(query, parameters);

            _changeTracker.Track(employee);

            return employee;
        }

        public async Task<Employee> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT id, size, email
                FROM employees
                WHERE id = @id;";

            var parameters = new
            {
                id = id,
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<EmployeeDb>(query, parameters);

            if (result is null)
            {
                return null;
            }

            var employee = new Employee(result.Id, Size.Parse(result.Size), new Email(result.Email));

            _changeTracker.Track(employee);

            return employee;
        }

        public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            var query = @"
                UPDATE employees
                SET size = @size,
                    email = @email
                WHERE id = @id;";

            var parameters = new
            {
                id = employee.Id,
                size = employee.Size.Id,
                email = employee.Email.Value
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            await connection.ExecuteAsync(query, parameters);

            _changeTracker.Track(employee);
        }
    }
}
