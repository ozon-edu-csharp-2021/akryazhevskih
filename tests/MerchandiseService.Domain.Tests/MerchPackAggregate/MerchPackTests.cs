using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace MerchandiseService.Domain.Tests.MerchPackAggregate
{
    public class MerchPackTests
    {
        [Fact]
        public void CreateMerchPack()
        {
            //Arrange
            var type = MerchType.VeteranPack;
            var items = new List<MerchPackItem>
            {
                new MerchPackItem(
                    new Sku(123456, "Socks"),
                    new Quantity(10),
                    Size.XL)
            };
            var size = Size.XL;

            //Act
            var result = new MerchPack(type, items, size);

            //Assert
            Assert.Equal(type, result.Type);
            Assert.Equal(items, result.Items);
            Assert.Equal(size, result.Size);
        }
        
        [Fact]
        public void CreateMerchPackWithoutSize()
        {
            //Arrange
            var type = MerchType.VeteranPack;
            var items = new List<MerchPackItem>
            {
                new MerchPackItem(
                    new Sku(123456, "Socks"),
                    new Quantity(10),
                    Size.XL)
            };

            //Act
            var result = new MerchPack(type, items);

            //Assert
            Assert.Equal(type, result.Type);
            Assert.Equal(items, result.Items);
            Assert.Null(result.Size);
        }
        
        [Fact]
        public void CreateMerchPackWithNullSize()
        {
            //Arrange
            var type = MerchType.VeteranPack;
            var items = new List<MerchPackItem>
            {
                new MerchPackItem(
                    new Sku(123456, "Socks"),
                    new Quantity(10),
                    Size.XL)
            };
            Size size = null;

            //Act
            var result = new MerchPack(type, items, size);

            //Assert
            Assert.Equal(type, result.Type);
            Assert.Equal(items, result.Items);
            Assert.Equal(size, result.Size);
        }
        
        [Fact]
        public void CreateMerchPackWithNullType()
        {
            //Arrange
            MerchType type = null;
            var items = new List<MerchPackItem>
            {
                new MerchPackItem(
                    new Sku(123456, "Socks"),
                    new Quantity(10),
                    Size.XL)
            };
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchPackException>(() => new MerchPack(type, items, size));
        }
        
        [Fact]
        public void CreateMerchPackWithNullItems()
        {
            //Arrange
            var type = MerchType.VeteranPack;
            List<MerchPackItem> items = null;
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchPackException>(() => new MerchPack(type, items, size));
        }
        
        [Fact]
        public void CreateMerchPackWithEmptyItems()
        {
            //Arrange
            var type = MerchType.VeteranPack;
            var items = new List<MerchPackItem>();
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchPackException>(() => new MerchPack(type, items, size));
        }
    }
}