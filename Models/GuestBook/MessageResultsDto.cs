using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.GuestBook.Entities;

namespace Web.Models.GuestBook
{
    public class MessageResultsDto
    {

        /// <summary>
        ///     Current page number
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        ///     Number of records per page
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        ///     Total number of records
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        ///  Records returned for the given search
        /// </summary>
        public IEnumerable<Message> Records { get; set; }

    }
}
