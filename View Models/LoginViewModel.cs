using System.ComponentModel.DataAnnotations;

namespace mvcproj.View_Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { set; get; }

    }
}
