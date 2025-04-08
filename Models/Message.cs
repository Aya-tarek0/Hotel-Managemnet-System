using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.Models
{
    public class Message
    {
      
            [Key]
            public int Id { get; set; }

        [ForeignKey("SenderId")]

        public ApplicationUser Sender { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }
        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }

        public string Content { get; set; }

            public DateTime Timestamp { get; set; } = DateTime.Now;
        
    }
}
