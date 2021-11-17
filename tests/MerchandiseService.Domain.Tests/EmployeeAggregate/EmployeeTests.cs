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
            var id = 999;
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
        public void CreateEmployeeWithZeroId()
        {
            //Arrange
            long id = 0;
            var size = Size.XL;
            var email = new Email("test999@test.ru");

            //Act

            //Assert
            Assert.Throws<EmployeeException>(() => new Employee(id, size, email));
        }
        
        [Fact]
        public void CreateEmployeeWithBelowZeroId()
        {
            //Arrange
            long id = -999;
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
            var id = 999;
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
            var id = 999;
            var size = Size.XL;
            Email email = null;

            //Act

            //Assert
            Assert.Throws<EmployeeException>(() => new Employee(id, size, email));
        }
    }
}