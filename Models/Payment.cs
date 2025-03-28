using System.ComponentModel.DataAnnotations.Schema;

namespace mvcproj.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }

        [ForeignKey("Booking")]
        public int BookingID { get; set; }

        public int Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentMethod { get; set; }

        public Booking? Booking { get; set; }
    }
}
