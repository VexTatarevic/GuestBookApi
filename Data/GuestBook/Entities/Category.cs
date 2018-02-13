
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.GuestBook.Entities
{
    [Table("Category", Schema = "GuestBook")]
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }


        //----------- Referencing Entities---------------

        public virtual ICollection<Message> Messages { get; set; }

    }
}
