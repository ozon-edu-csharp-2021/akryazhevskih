using System;
using System.Collections.Generic;
using System.Linq;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchAggregate
{
    /// <summary>
    /// Запрос на выдачу мерча
    /// </summary>
    public class Merch : Entity
    {
        public Merch(
            Employee employee,
            MerchType type
        )
        {
            if (employee is null)
            {
                throw new MerchException("Employee cannot be null");
            }
            
            if (type is null)
            {
                throw new MerchException("Merch type cannot be null");
            }
            
            Employee = employee;
            Type = type;
            
            Items = new List<MerchItem>();
            CreatedAt = DateTime.UtcNow;
        }
        
        /// <summary>
        /// ID
        /// </summary>
        public new Identifier Id { get; private set; } 
        
        /// <summary>
        /// Сотрудник
        /// </summary>
        public Employee Employee { get; }
        
        /// <summary>
        /// Статус
        /// </summary>
        public MerchStatus Status
        {
            get 
            {
                if (!Items.Any())
                {
                    return MerchStatus.New;
                }

                if (Items.Exists(x => x.Status.Equals(MerchItemStatus.SupplyAwaits)))
                {
                    return MerchStatus.SupplyAwaits;
                }

                return MerchStatus.Done;
            }
        }

        /// <summary>
        /// Набор товаров
        /// </summary>
        public MerchType Type  { get; }
        
        /// <summary>
        /// Список товаров
        /// </summary>
        public List<MerchItem> Items { get; }
        
        /// <summary>
        /// Дата создания запроса
        /// </summary>
        public DateTime CreatedAt  { get; }
        
        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? IssuedAt  { get; }
        
        /// <summary>
        /// Добавление товара в список
        /// </summary>
        /// <param name="item"></param>
        public bool TryAddMerchItem(MerchItem item)
        {
            if (item is null)
            {
                return false;
            }
            
            if (Items.Exists(x => x.Sku.Equals(item.Sku)))
            {
                return false;
            }
            
            Items.Add(item);

            return true;
        }

        /// <summary>
        /// Обновление товара в списке
        /// </summary>
        /// <param name="item"></param>
        public bool TryUpdateMerchItem(MerchItem item)
        {
            if (item is null)
            {
                return false;
            }
            
            var merchItem = Items.FirstOrDefault(x => x.Sku.Equals(item.Sku));
            if (merchItem is null)
            {
                return false;
            }

            merchItem.SetQuantity(item.Quantity);
            merchItem.SetIssuedQuantity(item.IssuedQuantity);
            
            return true;
        }
    }
}