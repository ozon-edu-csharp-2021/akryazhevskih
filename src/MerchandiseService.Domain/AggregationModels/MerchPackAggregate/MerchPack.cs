using System.Collections.Generic;
using System.Linq;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using MerchandiseService.Domain.Models;

namespace MerchandiseService.Domain.AggregationModels.MerchPackAggregate
{
    /// <summary>
    /// Набор товаров
    /// </summary>
    public class MerchPack : ValueObject
    {
        public MerchPack(
            MerchType type,
            List<MerchPackItem> items,
            Size size = null)
        {
            if (type is null)
            {
                throw new MerchPackException("Sku cannot be null");
            }
            
            if (items is null || !items.Any())
            {
                throw new MerchPackException("Items cannot be null");
            }
            
            Type = type;
            Items = items;
            Size = size;
        }
        
        /// <summary>
        /// Тип набора
        /// </summary>
        public MerchType Type  { get; }
        
        /// <summary>
        /// Размер
        /// </summary>
        public Size? Size { get; }
        
        /// <summary>
        /// Список товаров
        /// </summary>
        private List<MerchPackItem> Items { get; }

        public List<MerchPackItem> GetMerchPackItems()
        {
            return new List<MerchPackItem>(Items);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Items;
        }
    }
}