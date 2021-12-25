using System.ComponentModel.DataAnnotations;

namespace MVCExample.Models
{
    public class phong_ban
    {
        [Key]
        public int id { set; get; }
        public string ten_phong_ban { set; get; }
    }
}