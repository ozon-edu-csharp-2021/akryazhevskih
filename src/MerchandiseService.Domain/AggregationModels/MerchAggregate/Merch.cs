using System;
using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Events;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Мерч
    /// </summary>
    public class Merch : Entity
    {
        public Merch(
            long id,
            Employee employe,
            Manager manager,
            MerchStatus status,
            MerchType type,
            Size size,
            DateTime createdAt,
            DateTime? issuedAt)
        {
            Id = id;
            Employee = employe;
            Manager = manager;
            Status = status;
            Type = type;
            Size = size;
            CreatedAt = createdAt;
            IssuedAt = issuedAt;

            Items = new List<MerchItem>();
        }

        private Merch(
            Employee employee,
            Manager manager,
            MerchType type,
            Size size)
        {
            if (employee is null)
            {
                throw new MerchException("Employee cannot be null");
            }

            if (manager is null)
            {
                throw new MerchException("Manager cannot be null");
            }

            if (type is null)
            {
                throw new MerchException("Merch type cannot be null");
            }

            if (size is null)
            {
                throw new MerchException("Size type cannot be null");
            }

            Employee = employee;
            Manager = manager;
            Type = type;
            Size = size;

            Items = new List<MerchItem>();
            Status = MerchStatus.New;
            CreatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Сотрудник
        /// </summary>
        public Employee Employee { get; }

        /// <summary>
        /// Статус
        /// </summary>
        public MerchStatus Status { get; private set; }

        /// <summary>
        /// Размер
        /// </summary>
        public Size Size { get; set; }

        /// <summary>
        /// Тип набора
        /// </summary>
        public MerchType Type { get; }

        /// <summary>
        /// Дата создания запроса
        /// </summary>
        public DateTime CreatedAt { get; }

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? IssuedAt { get; private set; }

        /// <summary>
        /// Менеджер, ответственный за выдачу
        /// </summary>
        public Manager Manager { get; }

        /// <summary>
        /// Список товаров
        /// </summary>
        private List<MerchItem> Items { get; set; }

        public IEnumerable<MerchItem> GetItems()
        {
            return new List<MerchItem>(Items);
        }

        public void SetItems(IEnumerable<MerchItem> items)
        {
            Items = new List<MerchItem>(items);
        }

        public static Merch Create(Employee employee, Manager manager, MerchType type, Size size)
        {
            return new Merch(employee, manager, type, size);
        }

        /// <summary>
        /// Добавление товара в список
        /// </summary>
        /// <param name="item"></param>
        public bool TryAddMerchItem(MerchItem item, out string reason)
        {
            reason = string.Empty;

            if (item is null)
            {
                reason = "MerchItem cannot be null";
                return false;
            }

            if (Items == null)
            {
                Items = new List<MerchItem>();
            }

            if (Items.Exists(x => x.Sku.Equals(item.Sku)))
            {
                reason = $"MerchItem with sku {item.Sku.Code} already exists";
                return false;
            }

            Items.Add(item);

            return true;
        }

        public void SetStatusInWork()
        {
            Status = MerchStatus.InWork;

            var merchStatusChangedToInWorkDomainEvent = new MerchStatusChangedToInWorkDomainEvent(this);

            AddDomainEvent(merchStatusChangedToInWorkDomainEvent);
        }

        public void SetStatusSupplyAwaits()
        {
            Status = MerchStatus.SupplyAwaits;

            var merchStatusChangedToSupplyAwaitsDomainEvent = new MerchStatusChangedToSupplyAwaitsDomainEvent(this);

            AddDomainEvent(merchStatusChangedToSupplyAwaitsDomainEvent);
        }

        public void SetStatusDone()
        {
            Status = MerchStatus.Done;
            IssuedAt = DateTime.UtcNow;

            var merchStatusChangedToDoneDomainEvent = new MerchStatusChangedToDoneDomainEvent(this);

            AddDomainEvent(merchStatusChangedToDoneDomainEvent);
        }
    }
}