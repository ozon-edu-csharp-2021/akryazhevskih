using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests.ValueObject
{
    public class QuantityTests
    {
        [Fact]
        public void CreateQuantity()
        {
            //Arrange
            var quantity = 999;

            //Act
            var result = new Quantity(quantity);

            //Assert
            Assert.Equal(quantity, result.Value);
        }
        
        [Fact]
        public void CreateZeroQuantity()
        {
            //Arrange
            var quantity = 0;

            //Act
            var result = new Quantity(quantity);

            //Assert
            Assert.Equal(quantity, result.Value);
        }
        
        [Fact]
        public void CreateBelowZeroQuantity()
        {
            //Arrange
            var quantity = -999;

            //Act

            //Assert
            Assert.Throws<QuantityException>(() => new Quantity(quantity));
        }
    }
}