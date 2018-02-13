
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.GuestBook.Entities
{

    [Table("Message", Schema = "GuestBook")]
    public class Message
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public string Subject { get; set; }
        
        public string Text { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime? DateRead { get; set; }


        public int CategoryId { get; set; }



        //----------- Referenced Entities -----------

        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }



    }
}
