using Microsoft.EntityFrameworkCore;
using MyBookStoreAPI.Models;

namespace MyBookStoreAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public virtual DbSet<OrderDetailModel> orderDetailModels { get; set; }
    }
}
