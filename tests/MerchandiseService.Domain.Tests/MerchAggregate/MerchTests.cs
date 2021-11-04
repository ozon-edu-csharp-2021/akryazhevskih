using System.Collections.Generic;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using Xunit;

namespace MerchandiseService.Domain.Tests.MerchAggregate
{
    public class MerchTests
    {
        [Fact]
        public void CreateMerch()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;
            var items = new List<MerchItem>();

            //Act
            var result = new Merch(employee, type);

            //Assert
            Assert.Equal(employee, result.Employee);
            Assert.Equal(type, result.Type);
            Assert.Equal(items, result.Items);
        }
        
        [Fact]
        public void CreateMerchWithNewStatus()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;

            //Act
            var result = new Merch(employee, type);

            //Assert
            Assert.Equal(MerchStatus.New, result.Status);
        }
        
        [Fact]
        public void CreateMerchWithSupplyAwaitsStatus()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;

            //Act
            var result = new Merch(employee, type);
            result.TryAddMerchItem(new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(5),
                Size.XL));

            //Assert
            Assert.Equal(MerchStatus.SupplyAwaits, result.Status);
        }
        
        [Fact]
        public void CreateMerchWithDoneStatus()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;

            //Act
            var result = new Merch(employee, type);
            result.TryAddMerchItem(new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(10),
                Size.XL));

            //Assert
            Assert.Equal(MerchStatus.Done, result.Status);
        }
        
        [Fact]
        public void AddItemToMerch()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;
            
            var item = new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(10),
                Size.XL);
            
            var items = new List<MerchItem>
            {
                item
            };

            //Act
            var result = new Merch(employee, type);
            result.TryAddMerchItem(item);

            //Assert
            Assert.Equal(items, result.Items);
        }
        
        [Fact]
        public void AddExistingItemToMerch()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;
            
            var item = new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(10),
                Size.XL);
            
            var items = new List<MerchItem>
            {
                item
            };

            //Act
            var result = new Merch(employee, type);
            result.TryAddMerchItem(item);
            result.TryAddMerchItem(item);

            //Assert
            Assert.Equal(items, result.Items);
        }
        
        [Fact]
        public void AddNullItemToMerch()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;

            MerchItem item = null;

            var items = new List<MerchItem>();

            //Act
            var result = new Merch(employee, type);
            result.TryAddMerchItem(item);

            //Assert
            Assert.Equal(items, result.Items);
        }
        
        [Fact]
        public void UpdateItemInMerch()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;
            
            var item = new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(10),
                Size.XL);
            
            var updateItem = new MerchItem(
                item.MerchId,
                item.Sku,
                new Quantity(20),
                new Quantity(20),
                item.Size);
            
            var items = new List<MerchItem>
            {
                updateItem
            };

            //Act
            var result = new Merch(employee, type);
            result.TryAddMerchItem(item);
            result.TryUpdateMerchItem(updateItem);

            //Assert
            Assert.Equal(items, result.Items);
        }
        
        [Fact]
        public void UpdateNotExistingItemInMerch()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;
            
            var item = new MerchItem(
                new Identifier(999),
                new Sku(123456, "Socks"),
                new Quantity(10),
                new Quantity(10),
                Size.XL);

            var items = new List<MerchItem>();

            //Act
            var result = new Merch(employee, type);
            result.TryUpdateMerchItem(item);

            //Assert
            Assert.Equal(items, result.Items);
        }
        
        [Fact]
        public void UpdateNullItemInMerch()
        {
            //Arrange
            var employee = new Employee(
                new Identifier(999),
                Size.XL,
                new Email("test999@test.ru"));
            var type = MerchType.VeteranPack;

            MerchItem item = null;

            var items = new List<MerchItem>();

            //Act
            var result = new Merch(employee, type);
            result.TryUpdateMerchItem(item);

            //Assert
            Assert.Equal(items, result.Items);
        }
    }
}