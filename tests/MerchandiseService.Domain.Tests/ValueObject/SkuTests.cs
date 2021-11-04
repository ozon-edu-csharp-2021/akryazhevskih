
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests.ValueObject
{
    public class SkuTests
    {
        [Fact]
        public void CreateSku()
        {
            //Arrange
            var code = 999;
            var name = "Socks";

            //Act
            var result = new Sku(code, name);

            //Assert
            Assert.Equal(code, result.Code);
            Assert.Equal(name, result.Name);
        }
        
        [Fact]
        public void CreateSkuWithEmptyName()
        {
            //Arrange
            var code = 999;
            var name = string.Empty;

            //Act

            //Assert
            Assert.Throws<SkuException>(() => new Sku(code, name));
        }
        
        [Fact]
        public void CreateSkuWithNullName()
        {
            //Arrange
            var code = 999;
            string name = null;

            //Act

            //Assert
            Assert.Throws<SkuException>(() => new Sku(code, name));
        }
        
        [Fact]
        public void CreateSkuWithZeroCode()
        {
            //Arrange
            var code = 0;
            var name = "Socks";

            //Act

            //Assert
            Assert.Throws<SkuException>(() => new Sku(code, name));
        }
        
        [Fact]
        public void CreateSkuWithBelowZeroCode()
        {
            //Arrange
            var code = -999;
            var name = "Socks";

            //Act

            //Assert
            Assert.Throws<SkuException>(() => new Sku(code, name));
        }
    }
}