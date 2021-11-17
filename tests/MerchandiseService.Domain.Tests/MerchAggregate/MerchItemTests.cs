using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests.MerchAggregate
{
    public class MerchItemTests
    {
        [Fact]
        public void CreateMerchItem()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);
            var size = Size.XL;

            //Act
            var result = new MerchItem(sku, quantity, size);

            //Assert
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(size, result.Size);
            Assert.Null(result.IssuedQuantity);
        }
        
        [Fact]
        public void CreateMerchItemWithSupplyAwaitsStatus()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);
            var size = Size.XL;


            //Act
            var result = new MerchItem(sku, quantity, size);

            //Assert
            Assert.Equal(MerchItemStatus.Awaits, result.Status);
        }
        
        [Fact]
        public void CreateMerchItemWithDoneStatus()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(10);
            var size = Size.XL;


            //Act
            var result = new MerchItem(sku, quantity, size);
            result.SetIssuedQuantity(issuedQuantity);

            //Assert
            Assert.Equal(MerchItemStatus.Done, result.Status);
        }
        
        [Fact]
        public void SetMerchItemQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            var quantity = new Quantity(10);

            //Act
            merchItem.SetQuantity(quantity);
            
            //Assert
            Assert.Equal(quantity, merchItem.Quantity);
        }
        
        [Fact]
        public void SetMerchItemNullQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            Quantity quantity = null;

            //Act
            
            //Assert
            Assert.Throws<QuantityException>(() => merchItem.SetQuantity(quantity));
        }
        
        [Fact]
        public void SetMerchItemZeroQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            var quantity = new Quantity(0);

            //Act
            
            //Assert
            Assert.Throws<QuantityException>(() => merchItem.SetQuantity(quantity));
        }
        
        [Fact]
        public void SetMerchItemIssuedQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            var quantity = new Quantity(10);

            //Act
            merchItem.SetIssuedQuantity(quantity);
            
            //Assert
            Assert.Equal(quantity, merchItem.IssuedQuantity);
        }
        
        [Fact]
        public void SetMerchItemBelowNecessaryIssuedQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            var quantity = new Quantity(5);

            //Act
            merchItem.SetIssuedQuantity(quantity);
            
            //Assert
            Assert.Equal(quantity, merchItem.IssuedQuantity);
        }
        
        [Fact]
        public void SetMerchItemNullIssuedQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            Quantity quantity = null;

            //Act
            
            //Assert
            Assert.Throws<QuantityException>(() => merchItem.SetIssuedQuantity(quantity));
        }
        
        [Fact]
        public void SetMerchItemZeroIssuedQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            var quantity = new Quantity(0);

            //Act
            
            //Assert
            Assert.Throws<QuantityException>(() => merchItem.SetIssuedQuantity(quantity));
        }
        
        [Fact]
        public void SetMerchItemAboveNecessaryIssuedQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Sku(123456),
                new Quantity(10),
                Size.XL);
            
            var quantity = new Quantity(15);

            //Act
            
            //Assert
            Assert.Throws<QuantityException>(() => merchItem.SetIssuedQuantity(quantity));
        }

        [Fact]
        public void CreateMerchItemWithoutSize()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);

            //Act
            var result = new MerchItem(sku, quantity);

            //Assert
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Null(result.IssuedQuantity);
            Assert.Null(result.Size);
        }
        
        [Fact]
        public void CreateMerchPackWithNullSize()
        {
            //Arrange
            var sku = new Sku(123456);
            var quantity = new Quantity(10);
            Size size = null;

            //Act
            var result = new MerchItem( sku, quantity, size);

            //Assert
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(size, result.Size);
            Assert.Null(result.IssuedQuantity);
        }
        
        [Fact]
        public void CreateMerchPackWithNullSku()
        {
            //Arrange
            Sku sku = null;
            var quantity = new Quantity(10);
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchItemException>(() => new MerchItem(sku, quantity, size));
        }
        
        [Fact]
        public void CreateMerchPackWithNullQuantity()
        {
            //Arrange
            var sku = new Sku(123456);
            Quantity quantity = null;
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchItemException>(() => new MerchItem(sku, quantity, size));
        }
    }
}