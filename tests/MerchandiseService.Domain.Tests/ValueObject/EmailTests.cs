using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests.ValueObject
{
    public class EmailTests
    {
        [Fact]
        public void CreateEmail()
        {
            //Arrange
            var email = "test999@test.ru";

            //Act
            var result = new Email(email);

            //Assert
            Assert.Equal(email, result.Value);
        }
        
        [Fact]
        public void CreateEmptyEmail()
        {
            //Arrange
            var email = string.Empty;

            //Act

            //Assert
            Assert.Throws<EmailException>(() => new Email(email));
        }
        
        [Fact]
        public void CreateNullEmail()
        {
            //Arrange
            string email = null;

            //Act

            //Assert
            Assert.Throws<EmailException>(() => new Email(email));
        }
    }
}