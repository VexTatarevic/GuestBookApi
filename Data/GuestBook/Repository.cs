

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Web.Data.GuestBook.Entities;
using Web.Models.GuestBook;

namespace Web.Data.GuestBook
{
    public class Repository : IRepository
    {

        private readonly DBContext _ctx;

        public Repository(DBContext ctx)
        {
            _ctx = ctx;
        }

        //-------------------
        //      Messages
        //--------------------
        

        public Message GetMessage(int id)
        {
            return _ctx.Messages
                    .Include(m => m.Category)
                    .Where(m => m.Id == id)
                    .FirstOrDefault();
        }

        public MessageResultsDto SearchMessages(MessageSearchDto s, Expression<Func<Message, bool>> predicate)
        {         
            // if search criteria provided then search by it, otherwise get all records without criteria
            var query = predicate == null ?
                _ctx.Messages :
                _ctx.Messages.Where(predicate);

            // if sort field provided then implement OrderBy
            if(!string.IsNullOrEmpty(s.SortBy))
            {
                query = query
                    .OrderBy(s.SortBy, s.Desc)
                    .ThenBy("Id");
            }

            // total number of records for the given search
            var total = query.Count();

            // Number of records to skip for this paging result
            var skip = (s.Page - 1) * s.Size;

            // Only select records on the current page using Take and Skip
            var records = query
               .Skip(skip)
               .Take(s.Size)
               .ToList();

            // Return paging result dto
            return new MessageResultsDto() {
                Page = s.Page,
                Size = s.Size,
                Total = total,
                Records = records
            };
        }
        


        //----------------------------
        //      Message Categories
        //-----------------------------

        public IEnumerable<Category> GetAllCategories()
        {
            return _ctx.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return GetCategory(id, false);
        }
        public Category GetCategory(int id, bool includeMessages)
        {
            var query = _ctx.Categories.Where(c => c.Id == id);

            var cat = includeMessages ?
            query.Include(c => c.Messages).FirstOrDefault() :
            query.FirstOrDefault();

            return cat;
        }


        //----------------------------
        //    Basic DB Operations
        //-----------------------------

        public void Add<T>(T entity) where T : class
        {
            _ctx.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _ctx.Remove(entity);
        }

        public async Task<bool> SaveAllAsync()
        {
            return (await _ctx.SaveChangesAsync()) > 0;
        }

    }
}
