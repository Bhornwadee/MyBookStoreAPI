using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBookStoreAPI.Data;
using MyBookStoreAPI.Models;

namespace MyBookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MultipleOrigins")]
    public class BookStoreController : ControllerBase
    {
        private readonly DataContext _context;

        public BookStoreController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<List<OrderDetailModel>>> CreateOrder(BookOrderModel order)
        {
            var orderDetail = new OrderDetailModel();
            orderDetail.Id = await GetId();
            orderDetail.OrderNumber = GenerateOrderNumber(orderDetail.Id);
            orderDetail.Name = order.Name;
            orderDetail.Price = order.Price;
            orderDetail.Store = order.Store;
            orderDetail.TotalPaid = order.Price;
            _context.orderDetailModels.Add(orderDetail);
            await _context.SaveChangesAsync();

            var result = await _context.orderDetailModels.ToListAsync();

            return (result);
        }

        [HttpGet("OrderList")]
        public async Task<ActionResult<List<OrderDetailModel>>> GetOrderDetails()
        {
            var data = await _context.orderDetailModels.ToListAsync();
            return (data);
        }

        string GenerateOrderNumber(int id)
        {
            var random = new Random().Next(5000);
            var ordernumber = $"BT0000{random}_{id}";

            return (ordernumber);
        }

        async Task<int> GetId()
        {
            var data = await _context.orderDetailModels.ToListAsync();
            int id = 1;
            if (data.Any() && data.Count() > 0)
            {
                id = data.Max(x => x.Id) + 1;
            }

            return (id);
        }

    }
}