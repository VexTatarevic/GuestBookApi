using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Web.Data.GuestBook.Entities;
using Web.Models.GuestBook;

namespace Web.Data.GuestBook
{
    public interface IRepository
    {

        // Messages            
        Message GetMessage(int id);
        MessageResultsDto SearchMessages(MessageSearchDto s, Expression<Func<Message, bool>> predicate);


        // Categories
        IEnumerable<Category> GetAllCategories();
        Category GetCategory(int id);
        Category GetCategory(int id, bool includeMessages);


        
        // Basic DB Operations
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAllAsync();


    }
}