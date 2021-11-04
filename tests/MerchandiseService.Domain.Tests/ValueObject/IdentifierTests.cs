using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests.ValueObject
{
    public class IdentifierTests
    {
        [Fact]
        public void CreateIdentifier()
        {
            //Arrange
            var id = 999;

            //Act
            var result = new Identifier(id);

            //Assert
            Assert.Equal(id, result.Value);
        }
        
        [Fact]
        public void CreateZeroIdentifier()
        {
            //Arrange
            var id = 0;

            //Act

            //Assert
            Assert.Throws<IdentifierException>(() => new Identifier(id));
        }
        
        [Fact]
        public void CreateBelowZeroIdentifier()
        {
            //Arrange
            var id = -999;

            //Act

            //Assert
            Assert.Throws<IdentifierException>(() => new Identifier(id));
        }
    }
}