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
            var merchId = new Identifier(999);
            var sku = new Sku(123456, "Socks");
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(10);
            var size = Size.XL;

            //Act
            var result = new MerchItem(merchId, sku, quantity, issuedQuantity, size);

            //Assert
            Assert.Equal(merchId, result.MerchId);
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(issuedQuantity, result.IssuedQuantity);
            Assert.Equal(size, result.Size);
        }
        
        [Fact]
        public void CreateMerchItemWithSupplyAwaitsStatus()
        {
            //Arrange
            var merchId = new Identifier(999);
            var sku = new Sku(123456, "Socks");
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(5);
            var size = Size.XL;


            //Act
            var result = new MerchItem(merchId, sku, quantity, issuedQuantity, size);

            //Assert
            Assert.Equal(MerchItemStatus.SupplyAwaits, result.Status);
        }
        
        [Fact]
        public void CreateMerchItemWithDoneStatus()
        {
            //Arrange
            var merchId = new Identifier(999);
            var sku = new Sku(123456, "Socks");
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(10);
            var size = Size.XL;


            //Act
            var result = new MerchItem(merchId, sku, quantity, issuedQuantity, size);

            //Assert
            Assert.Equal(MerchItemStatus.Done, result.Status);
        }
        
        [Fact]
        public void SetMerchItemdQuantity()
        {
            //Arrange
            var merchItem = new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(0),
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
            var merchId = new Identifier(999);
            var sku = new Sku(123456, "Socks");
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(10);

            //Act
            var result = new MerchItem(merchId, sku, quantity, issuedQuantity);

            //Assert
            Assert.Equal(merchId, result.MerchId);
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(issuedQuantity, result.IssuedQuantity);
            Assert.Null(result.Size);
        }
        
        [Fact]
        public void CreateMerchPackWithNullSize()
        {
            //Arrange
            var merchId = new Identifier(999);
            var sku = new Sku(123456, "Socks");
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(10);
            Size size = null;

            //Act
            var result = new MerchItem(merchId, sku, quantity, issuedQuantity, size);

            //Assert
            Assert.Equal(merchId, result.MerchId);
            Assert.Equal(sku, result.Sku);
            Assert.Equal(quantity, result.Quantity);
            Assert.Equal(issuedQuantity, result.IssuedQuantity);
            Assert.Equal(size, result.Size);
        }
        
        [Fact]
        public void CreateMerchPackWithNullMerchId()
        {
            //Arrange
            Identifier merchId = null;
            var sku = new Sku(123456, "Socks");
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(10);
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchItemException>(() => new MerchItem(merchId, sku, quantity, issuedQuantity, size));
        }
        
        [Fact]
        public void CreateMerchPackWithNullSku()
        {
            //Arrange
            var merchId = new Identifier(999);
            Sku sku = null;
            var quantity = new Quantity(10);
            var issuedQuantity = new Quantity(10);
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchItemException>(() => new MerchItem(merchId, sku, quantity, issuedQuantity, size));
        }
        
        [Fact]
        public void CreateMerchPackWithNullQuantity()
        {
            //Arrange
            var merchId = new Identifier(999);
            var sku = new Sku(123456, "Socks");
            Quantity quantity = null;
            var issuedQuantity = new Quantity(10);
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchItemException>(() => new MerchItem(merchId, sku, quantity, issuedQuantity, size));
        }
        
        [Fact]
        public void CreateMerchPackWithNullIssuedQuantity()
        {
            //Arrange
            var merchId = new Identifier(999);
            var sku = new Sku(123456, "Socks");
            var quantity = new Quantity(10);
            Quantity issuedQuantity = null;
            var size = Size.XL;

            //Act

            //Assert
            Assert.Throws<MerchItemException>(() => new MerchItem(merchId, sku, quantity, issuedQuantity, size));
        }
    }
}