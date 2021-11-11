
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

            //Act
            var result = new Sku(code);

            //Assert
            Assert.Equal(code, result.Code);
        }
        
        [Fact]
        public void CreateSkuWithZeroCode()
        {
            //Arrange
            var code = 0;

            //Act

            //Assert
            Assert.Throws<SkuException>(() => new Sku(code));
        }
        
        [Fact]
        public void CreateSkuWithBelowZeroCode()
        {
            //Arrange
            var code = -999;

            //Act

            //Assert
            Assert.Throws<SkuException>(() => new Sku(code));
        }
    }
}