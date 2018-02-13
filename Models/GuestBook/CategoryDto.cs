

using System.Collections.Generic;

namespace Web.Models.GuestBook
{
    public class CategoryDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public ICollection<MessageDto> Messages { get; set; }

    }
}
