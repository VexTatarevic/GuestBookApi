using Microsoft.EntityFrameworkCore;

using Web.Data.GuestBook.Entities;

namespace Web.Data.GuestBook
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }


        public DbSet<Message> Messages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public object MessageCategories { get; internal set; }
    }
}
