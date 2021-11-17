using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using Xunit;

namespace MerchandiseService.Domain.Tests.MerchAggregate
{
    public class MerchTypeTests
    {
        [Fact]
        public void CreateMerchType()
        {
            //Arrange
            var id = 10;
            var name = "StarterPack";

            //Act
            var result = new MerchType(id, name);

            //Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(name, result.Name);
        }
        
        [Fact]
        public void ParseMerchType()
        {
            //Arrange
            var type = MerchType.VeteranPack;

            //Act
            MerchType.TryParse(type.Id, out var result);

            //Assert
            Assert.Equal(type.Id, result.Id);
            Assert.Equal(type.Name, result.Name);
        }
        
        [Fact]
        public void ParseNonExistentMerchType()
        {
            //Arrange
            var id = 999;

            //Act
            MerchType.TryParse(id, out var result);

            //Assert
            Assert.Null(result);
        }
        
        [Fact]
        public void ParseMerchTypeWithZeroId()
        {
            //Arrange
            var id = 0;

            //Act

            //Assert
            Assert.Throws<MerchTypeException>(() => MerchType.TryParse(id, out var result));
        }
        
        [Fact]
        public void ParseMerchTypeWithBelowZeroId()
        {
            //Arrange
            var id = -999;

            //Act

            //Assert
            Assert.Throws<MerchTypeException>(() => MerchType.TryParse(id, out var result));
        }
    }
}