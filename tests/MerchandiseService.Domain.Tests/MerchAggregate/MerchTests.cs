using System.Collections.Generic;
using System.Linq;
using MerchandiseService.Domain.AggregationModels.EmployeeAggregate;
using MerchandiseService.Domain.AggregationModels.MerchAggregate;
using MerchandiseService.Domain.AggregationModels.ValueObjects;
using MerchandiseService.Domain.Exceptions.MerchAggregate;
using Xunit;

namespace MerchandiseService.Domain.Tests.MerchAggregate
{
    public class MerchTests
    {
         [Fact]
         public void CreateMerch()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;
             
             var items = new List<MerchItem>();

             //Act
             var result = new Merch(employee, type);

             //Assert
             Assert.Equal(employee, result.Employee);
             Assert.Equal(type, result.Type);
             Assert.Equal(items, result.GetMerchItems());
             Assert.Equal(MerchStatus.New, result.Status);
         }
         
         [Fact]
         public void CreateMerchWithInWorkStatus()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;
             
             var result = new Merch(employee, type);
             var merchItem = new MerchItem(new Sku(123456), new Quantity(10), Size.XL);
             result.TryAddMerchItem(merchItem, out var reason);

             //Act
             result.SetStatusInWork();

             //Assert
             Assert.Equal(MerchStatus.InWork, result.Status);
         }
         
         [Fact]
         public void CreateMerchWithSupplyAwaitsStatus()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;

             var result = new Merch(employee, type);
             var merchItem = new MerchItem(new Sku(123456), new Quantity(10), Size.XL);
             merchItem.SetIssuedQuantity(new Quantity(10));

             //Act
             result.SetStatusSupplyAwaits();

             //Assert
             Assert.Equal(MerchStatus.SupplyAwaits, result.Status);
         }
//         
         [Fact]
         public void CreateMerchWithDoneStatus()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;

             var result = new Merch(employee, type);
             var merchItem = new MerchItem(new Sku(123456), new Quantity(10), Size.XL);
             merchItem.SetIssuedQuantity(new Quantity(10));

             //Act
             result.SetStatusDone();

             //Assert
             Assert.Equal(MerchStatus.Done, result.Status);
         }
         
         [Fact]
         public void AddItemToMerch()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;
             
             var result = new Merch(employee, type);
             
             var item = new MerchItem(new Sku(123456), new Quantity(10), Size.XL);
             var items = new List<MerchItem>
             {
                 item
             };

             //Act
             result.TryAddMerchItem(item, out _);

             //Assert
             Assert.Equal(items, result.GetMerchItems());
         }
         
         [Fact]
         public void AddExistingItemToMerch()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;
             
             var result = new Merch(employee, type);
             
             var item = new MerchItem(new Sku(123456), new Quantity(10), Size.XL);
             var items = new List<MerchItem>
             {
                 item
             };

             //Act
             result.TryAddMerchItem(item, out _);
             result.TryAddMerchItem(item, out _);

             //Assert
             Assert.Equal(items, result.GetMerchItems());
         }
         
         [Fact]
         public void AddNullItemToMerch()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;
             
             var result = new Merch(employee, type);

             MerchItem item = null;
             var items = new List<MerchItem>();

             //Act
             result.TryAddMerchItem(item, out _);

             //Assert
             Assert.Equal(items, result.GetMerchItems());
         }
         
         [Fact]
         public void UpdateItemInMerch()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             var type = MerchType.VeteranPack;
             
             var result = new Merch(employee, type);
             
             var item = new MerchItem(new Sku(123456), new Quantity(10), Size.XL);
             
             result.TryAddMerchItem(item, out _);
             
             item.SetIssuedQuantity(new Quantity(10));
             var items = new List<MerchItem>
             {
                 item
             };

             //Act
             result.GetMerchItems().FirstOrDefault().SetIssuedQuantity(new Quantity(10));

             //Assert
             Assert.Equal(items, result.GetMerchItems());
         }
         
         [Fact]
         public void CreateMerchWithNullEmployee()
         {
             //Arrange
             Employee employee = null;
             var type = MerchType.VeteranPack;

             //Act

             //Assert
             Assert.Throws<MerchException>(() => new Merch(employee, type));
         }
         
         [Fact]
         public void CreateMerchWithNullType()
         {
             //Arrange
             var employee = new Employee(999, Size.XL, new Email("test999@test.ru"));
             MerchType type = null;

             //Act

             //Assert
             Assert.Throws<MerchException>(() => new Merch(employee, type));
         }
    }
}