using MerchandiseService.Domain.AggregationModels.MerchPackAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchPackAggregate;
using Xunit;

namespace MerchandiseService.Domain.Tests.MerchPackAggregate
{
    public class MerchPackItemTests
    {
        [Fact]
        public void CreateMerchPackItem()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);
            var size = Size.XL;

            //Act
            var result = MerchPackItem.Create(1, sku, quantity, size);

            //Assert
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(size, result.Size);
        }
        
        [Fact]
        public void CreateMerchPackItemWithoutSize()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);

            //Act
            var result = MerchPackItem.Create(1,sku, quantity);

            //Assert
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Null(result.Size);
        }
        
        [Fact]
        public void CreateMerchPackItemWithNullSize()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);
            Size size = null;

            //Act
            var result = MerchPackItem.Create(1, sku, quantity, size);

            //Assert
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(size, result.Size);
        }
        
        [Fact]
        public void CreateMerchPackItemWithNullSku()
        {
            //Arrange
            Sku sku = null;
            var quantity = new Quantity(10);
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchPackItemException>(() => MerchPackItem.Create(1, sku, quantity, size));
        }
        
        [Fact]
        public void CreateMerchPackItemWithNullQuantity()
        {
            //Arrange
            var sku = new Sku(123456);
            Quantity quantity = null;
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchPackItemException>(() => MerchPackItem.Create(1, sku, quantity, size));
        }
    }
}