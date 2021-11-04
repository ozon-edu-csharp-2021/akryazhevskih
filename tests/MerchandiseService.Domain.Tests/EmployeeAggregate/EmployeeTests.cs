using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.EmployeeAggregate;
using Xunit;

namespace MerchandiseService.Domain.Tests.EmployeeAggregate
{
    public class EmployeeTests
    {
        [Fact]
        public void CreateEmployee()
        {
            //Arrange
            var id = new Identifier(999);
            var size = Size.XL;
            var email = new Email("test999@test.ru");

            //Act
            var result = new Employee(id, size, email);

            //Assert
            Assert.Equal(id, result.Id);
            Assert.Equal(size, result.Size);
            Assert.Equal(email, result.Email);
        }
        
        [Fact]
        public void CreateEmployeeWithNullId()
        {
            //Arrange
            Identifier id = null;
            var size = Size.XL;
            var email = new Email("test999@test.ru");

            //Act

            //Assert
            Assert.Throws<EmployeeException>(() => new Employee(id, size, email));
        }
        
        [Fact]
        public void CreateEmployeeWithNullSize()
        {
            //Arrange
            var id = new Identifier(999);
            Size size = null;
            var email = new Email("test999@test.ru");

            //Act

            //Assert
            Assert.Throws<EmployeeException>(() => new Employee(id, size, email));
        }
        
        [Fact]
        public void CreateEmployeeWithNullEmail()
        {
            //Arrange
            var id = new Identifier(999);
            var size = Size.XL;
            Email email = null;

            //Act

            //Assert
            Assert.Throws<EmployeeException>(() => new Employee(id, size, email));
        }
    }
}