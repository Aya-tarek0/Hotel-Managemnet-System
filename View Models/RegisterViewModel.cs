using System.ComponentModel.DataAnnotations;

namespace mvcproj.View_Models
{
    public class RegisterViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Address { set; get; }
        public string Email { get; set; }
        public string Phone { set; get; }
    }
}
