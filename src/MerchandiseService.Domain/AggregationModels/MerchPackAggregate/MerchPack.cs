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
    public class MerchPack : Entity
    {
        public MerchPack(
            long id,
            MerchType type,
            string description,
            Size? size = null)
        {
            Id = id;
            Type = type;
            Size = size;
            Description = description;
        }

        private MerchPack(MerchType type, string description, Size? size = null)
        {
            if (type is null)
            {
                throw new MerchPackException("Type cannot be null");
            }

            Type = type;
            Size = size;
            Description = description;
        }

        public MerchPack(
            MerchType type,
            IEnumerable<MerchPackItem> items,
            Size? size = null)
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
        public MerchType Type { get; }

        /// <summary>
        /// Размер
        /// </summary>
        public Size? Size { get; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Список товаров
        /// </summary>
        private IEnumerable<MerchPackItem>? Items { get; set; }

        public IEnumerable<MerchPackItem> GetItems()
        {
            return new List<MerchPackItem>(Items);
        }

        public void SetItems(IEnumerable<MerchPackItem> items)
        {
            Items = new List<MerchPackItem>(items);
        }

        public MerchPack Create(MerchType type, string description, Size? size = null)
        {
            return new MerchPack(type, description, size);
        }
    }
}