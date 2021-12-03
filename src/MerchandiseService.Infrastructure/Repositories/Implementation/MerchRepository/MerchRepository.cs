using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Infrastructure.Repositories.Infrastructure.Interfaces;
using MerchandiseService.Infrastructure.Repositories.Models;
using Npgsql;

namespace MerchandiseService.Infrastructure.Repositories.Implementation.MerchRepository
{
    public class MerchRepository : IMerchRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _connectionFactory;
        private readonly IChangeTracker _changeTracker;

        public MerchRepository(
            IDbConnectionFactory<NpgsqlConnection> connectionFactory,
            IChangeTracker changeTracker)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory), "Cannot be null");
            _changeTracker = changeTracker ?? throw new ArgumentNullException(nameof(changeTracker), "Cannot be null");
        }

        public async Task<Merch> CreateAsync(Merch merch, CancellationToken cancellationToken = default)
        {
            var query = @"
                INSERT INTO merches (employee_name, employee_email, manager_name, manager_email, status, type, size, created_at)
                VALUES (@employee_name, @employee_email, @manager_name, @manager_email, @status, @type, @size, @created_at)
                RETURNING id, 
                          employee_name as EmployeeName,
                          employee_email as EmployeeEmail,
                          manager_name as ManagerName,
                          manager_email as ManagerEmail,
                          status,
                          type,
                          size,
                          created_at as CreatedAt,
                          issued_at as IssuedAt;";

            var parameters = new
            {
                employee_name = merch.Employee.Person.FullName,
                employee_email = merch.Employee.Email.Value,
                manager_name = merch.Manager.Person.FullName,
                manager_email = merch.Manager.Email.Value,
                status = merch.Status.Id,
                type = merch.Type.Id,
                size = merch.Size.Id,
                created_at = merch.CreatedAt
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QuerySingleAsync<MerchDb>(query, parameters);

            var createdMerch = new Merch(
                result.Id,
                new Employee
                {
                    Person = new Person(result.EmployeeName),
                    Email = new Email(result.EmployeeEmail)
                },
                new Manager
                {
                    Person = new Person(result.ManagerName),
                    Email = new Email(result.ManagerEmail)
                },
                MerchStatus.Parse(result.Status),
                MerchType.Parse(result.Type),
                Size.Parse(result.Size),
                result.CreatedAt,
                result.IssuedAt);

            _changeTracker.Track(merch);

            return createdMerch;
        }

        public async Task UpdateAsync(Merch merch, CancellationToken cancellationToken = default)
        {
            var query = @"
                UPDATE merches
                SET status = @status,
                    issued_at = @issued_at
                WHERE id = @id;";

            var parameters = new
            {
                id = merch.Id,
                status = merch.Status.Id,
                issued_at = merch.IssuedAt
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            await connection.ExecuteAsync(query, parameters);

            _changeTracker.Track(merch);
        }

        public async Task<Merch> GetAsync(long id, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT id, status, type, size, created_at as CreatedAt, issued_at as IssuedAt,
                       employee_name as EmployeeName, employee_email as EmployeeEmail,
                       manager_name as ManagerName, manager_email as ManagerEmail
                FROM merches
                WHERE id = @id;";

            var parameters = new
            {
                id = id
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<MerchDb>(query, parameters);

            if (result is null)
            {
                return null;
            }

            var merch = new Merch(
                result.Id,
                new Employee
                {
                    Person = new Person(result.EmployeeName),
                    Email = new Email(result.EmployeeEmail)
                },
                new Manager
                {
                    Person = new Person(result.ManagerName),
                    Email = new Email(result.ManagerEmail)
                },
                MerchStatus.Parse(result.Status),
                MerchType.Parse(result.Type),
                Size.Parse(result.Size),
                result.CreatedAt,
                result.IssuedAt);

            var merchItems = await GetMerchItems(merch.Id, cancellationToken);

            merch.SetItems(merchItems);

            _changeTracker.Track(merch);

            return merch;
        }

        public async Task<Merch> GetAsync(string employeeEmail, MerchType type, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT id, status, type, size, created_at as CreatedAt, issued_at as IssuedAt,
                       employee_name as EmployeeName, employee_email as EmployeeEmail,
                       manager_name as ManagerName, manager_email as ManagerEmail
                FROM merches
                WHERE employee_email = @employee_email
                AND type = @type;";

            var parameters = new
            {
                employee_email = employeeEmail,
                type = type.Id
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QueryFirstOrDefaultAsync<MerchDb>(query, parameters);

            if (result is null)
            {
                return null;
            }

            var merch = new Merch(
                result.Id,
                new Employee
                {
                    Person = new Person(result.EmployeeName),
                    Email = new Email(result.EmployeeEmail)
                },
                new Manager
                {
                    Person = new Person(result.ManagerName),
                    Email = new Email(result.ManagerEmail)
                },
                MerchStatus.Parse(result.Status),
                MerchType.Parse(result.Type),
                Size.Parse(result.Size),
                result.CreatedAt,
                result.IssuedAt);

            var merchItems = await GetMerchItems(merch.Id, cancellationToken);

            merch.SetItems(merchItems);

            _changeTracker.Track(merch);

            return merch;
        }

        public async Task<IEnumerable<Merch>> GetSupplyAwaitsMerches(long sku, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT m.id, m.status, m.type, m.size, m.created_at as CreatedAt, m.issued_at as IssuedAt,
                       m.employee_name as EmployeeName, m.employee_email as EmployeeEmail,
                       m.manager_name as ManagerName, m.manager_email as ManagerEmail
                FROM merches m
                JOIN merch_items mi on mi.merch_id = m.id
                WHERE mi.sku = @sku
                AND mi.status = @status;";

            var parameters = new
            {
                sku = sku,
                status = MerchItemStatus.Awaits.Id
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QueryAsync<MerchDb>(query, parameters);

            var merches = result.Select(x => new Merch(
                x.Id,
                new Employee
                {
                    Person = new Person(x.EmployeeName),
                    Email = new Email(x.EmployeeEmail)
                },
                new Manager
                {
                    Person = new Person(x.ManagerName),
                    Email = new Email(x.ManagerEmail)
                },
                MerchStatus.Parse(x.Status),
                MerchType.Parse(x.Type),
                Size.Parse(x.Size),
                x.CreatedAt,
                x.IssuedAt));

            foreach (var merch in merches)
            {
                _changeTracker.Track(merch);
            }

            return merches;
        }

        public async Task<IEnumerable<MerchItem>> GetMerchItems(long merchId, CancellationToken cancellationToken = default)
        {
            var query = @"
                SELECT id, merch_id as MerchId, sku, quantity, issued_quantity as IssuedQuantity, size, status
                FROM merch_items
                WHERE merch_id = @merch_id;";

            var parameters = new
            {
                merch_id = merchId,
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QueryAsync<MerchItemDb>(query, parameters);

            var merchItems = result.Select(x => new MerchItem(
                x.Id,
                x.MerchId,
                new Sku(x.Sku),
                new Quantity(x.Quantity),
                new Quantity(x.IssuedQuantity),
                x.Size.HasValue ? Size.Parse(x.Size.Value) : null));

            foreach (var merchItem in merchItems)
            {
                _changeTracker.Track(merchItem);
            }

            return merchItems;
        }

        public async Task<MerchItem> CreateAsync(MerchItem merchItem, CancellationToken cancellationToken = default)
        {
            var query = @"
                INSERT INTO merch_items (merch_id, sku, quantity, issued_quantity, size, status)
                VALUES (@merch_id, @sku, @quantity, @issued_quantity, @size, @status)
                RETURNING id, merch_id as MerchId, sku, quantity, issued_quantity as IssuedQuantity, size, status;";

            var parameters = new
            {
                merch_id = merchItem.MerchId,
                sku = merchItem.Sku.Code,
                quantity = merchItem.Quantity.Value,
                issued_quantity = merchItem.IssuedQuantity?.Value,
                size = merchItem.Size?.Id,
                status = merchItem.Status.Id
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            var result = await connection.QuerySingleAsync<MerchItemDb>(query, parameters);

            var createdMerchItem = new MerchItem(
                result.Id,
                result.MerchId,
                new Sku(result.Sku),
                new Quantity(result.Quantity),
                new Quantity(result.IssuedQuantity),
                result.Size.HasValue ? Size.Parse(result.Size.Value) : null);

            _changeTracker.Track(createdMerchItem);

            return createdMerchItem;
        }

        public async Task UpdateAsync(MerchItem merchItem, CancellationToken cancellationToken = default)
        {
            var query = @"
                UPDATE merch_items
                SET quantity = @quantity,
                    issued_quantity = @issued_quantity,
                    status = @status
                WHERE id = @id;";

            var parameters = new
            {
                id = merchItem.Id,
                quantity = merchItem.Quantity.Value,
                issued_quantity = merchItem.IssuedQuantity?.Value,
                status = merchItem.Status.Id
            };

            var connection = await _connectionFactory.CreateConnection(cancellationToken);

            await connection.ExecuteAsync(query, parameters);

            _changeTracker.Track(merchItem);
        }
    }
}