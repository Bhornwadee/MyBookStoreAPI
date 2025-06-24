using Microsoft.EntityFrameworkCore;
using MyBookStoreAPI.Data;
using MyBookStoreAPI.Models;
using Xunit.Abstractions;

namespace MyBookStoreAPI.UnitTests;

public class BookStoreTests
{
    private readonly ITestOutputHelper _output;

    public BookStoreTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public async Task CanGetAndSaveData()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
                  .UseInMemoryDatabase(databaseName: "MyBookStoreDB")
                  .Options;

        var databaseContext = new DataContext(options);
        databaseContext.Database.EnsureCreated();

        if (await databaseContext.orderDetailModels.CountAsync() <= 0)
        {
            var random = new Random().Next(2000);
            databaseContext.orderDetailModels.Add(new OrderDetailModel { Id = random, OrderNumber = $"BT0000{random}_{random}", Name = "UnitTest001", Price = random, Store = "Greta", TotalPaid = random });
            databaseContext.orderDetailModels.Add(new OrderDetailModel { Id = random + 1, OrderNumber = $"BT0000{random + 1}_{random + 1}", Name = "UnitTest002", Price = random + 1, Store = "Peter", TotalPaid = random + 1 });

            //Test : Create Order
            await databaseContext.SaveChangesAsync();
        }

        //Test : Get Data
        var data = databaseContext.orderDetailModels.ToList();

        //Show all data in databaseContext
        foreach (var item in data)
        {
            _output.WriteLine($"Id = {item.Id}, OrderNumber = {item.OrderNumber}, Name = {item.Name}, Price = {item.Price}, Store = {item.Store}, TotalPaid = {item.TotalPaid}");
        }

        Assert.NotEmpty(data);
        Assert.Equal(2, data.Count());
        Assert.Contains(data, o => o.Name == "UnitTest001");
    }
}