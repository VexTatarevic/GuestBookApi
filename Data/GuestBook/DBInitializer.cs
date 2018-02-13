using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Data.GuestBook.Entities;

namespace Web.Data.GuestBook
{
    public class DBInitializer
    {
        private readonly DBContext _ctx;

        public DBInitializer(DBContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        ///  Create sample data
        /// </summary>
        public async Task Seed()
        {

            _ctx.Database.EnsureCreated();

            // Message Categories
            if (!_ctx.Categories.Any())
            {
                _ctx.Categories.AddRange(new Category[]{
                    new Category() { Name = "Greeting"}
                    , new Category() { Name = "Question" }
                    , new Category() { Name = "Feedback" }
                    , new Category() { Name = "Idea" }
                    , new Category() { Name = "Bug" }
                 });
            }
            // Messages
            if (!_ctx.Messages.Any())
            {
                _ctx.Messages.AddRange(new Message[]{
                    new Message() { Name = "John", Email = "john@gmail.com", Subject = "John - Greeting", Text = "Hello from John. Have a nice day.", DateSent = DateTime.UtcNow, CategoryId=1 }
                    , new Message() { Name = "Bill", Email = "bill@hotmail.com", Subject = "Bill - Question", Text = "Hello from Bill. What is your favourit color.", DateSent = DateTime.UtcNow, CategoryId=2 }
                    , new Message() { Name = "Harry", Email = "harry@yahoo.com", Subject = "Harry - Feedback", Text = "Hello from Harry. I like your website.", DateSent = DateTime.UtcNow, CategoryId=3 }
                    , new Message() { Name = "Tom", Email = "tom@gmail.com", Subject = "Tom - Idea", Text = "Hello from Tom. You should build a social networking app for people in IT.", DateSent = DateTime.UtcNow, CategoryId=4 }
                    , new Message() { Name = "Will", Email = "will@gmail.com", Subject = "Will - Bug", Text = "Hello from Will. I'd like to report a bug on your Crypto app. When I go to detail page it is not showing  the current rate for that currency.", DateSent = DateTime.UtcNow, CategoryId=5 }

                 });
            }

            await _ctx.SaveChangesAsync();


        }


    }
}
