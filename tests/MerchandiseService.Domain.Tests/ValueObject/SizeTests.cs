using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests.ValueObject
{
    public class SizeTests
    {
        [Fact]
        public void CreateSize()
        {
            //Arrange
            var size = 10;
            var name = "XXXL";

            //Act
            var result = new Size(size, name);

            //Assert
            Assert.Equal(size, result.Id);
            Assert.Equal(name, result.Name);
        }
        
        [Fact]
        public void ParseSize()
        {
            //Arrange
            var size = Size.XL;

            //Act
            Size.TryParse(size.Id, out var result);

            //Assert
            Assert.Equal(size.Id, result.Id);
            Assert.Equal(size.Name, result.Name);
        }
        
        [Fact]
        public void ParseNonExistentSize()
        {
            //Arrange
            var size = 999;

            //Act
            Size.TryParse(size, out var result);

            //Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void ParseSizeWithZeroId()
        {
            //Arrange
            var id = 0;

            //Act

            //Assert
            Assert.Throws<SizeException>(() => Size.TryParse(id, out var result));
        }
        
        [Fact]
        public void ParseSizeWithBelowZeroId()
        {
            //Arrange
            var id = -999;

            //Act

            //Assert
            Assert.Throws<SizeException>(() => Size.TryParse(id, out var result));
        }
    }
}