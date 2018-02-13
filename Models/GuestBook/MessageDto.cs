using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Models.GuestBook
{
    public class MessageDto
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [MaxLength(250, ErrorMessage = "Message too long")]
        public string Text { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime? DateRead { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


    }
}
