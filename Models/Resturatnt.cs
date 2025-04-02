using System.ComponentModel.DataAnnotations;

namespace mvcproj.Models
{
    public class Resturatnt
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public int Price { set; get; }
        public string Description { set; get; }

        public string Image { set; get; }
    }

}
