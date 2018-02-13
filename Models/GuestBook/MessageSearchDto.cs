using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.GuestBook
{
    public class MessageSearchDto
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
        ///     Field by which the records are ordered
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        ///    True means Sort in Descending order. False : sort in Ascending order
        /// </summary>
        public bool Desc{ get; set; }


        /// <summary>
        ///  CategoryId
        /// </summary>
        public int? Cat { get; set; }

        /// <summary>
        ///  Sender Name
        /// </summary>
        public string Sender { get; set; }


        /// <summary>
        ///  Sender Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///  Message Subject
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        ///  Has message been read
        /// </summary>
        public bool? Read { get; set; }

        /// <summary>
        ///  Date From - return messages sent from and including this date
        /// </summary>
        public DateTime? From { get; set; }

        /// <summary>
        ///  Date To - return messages sent to and including this date
        /// </summary>
        public DateTime? To { get; set; }


    }
}
