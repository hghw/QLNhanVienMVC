using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCExample.Models
{
    [Table("phong_ban")]
    public class phong_ban
    {
        [Key]
        public int phongban_id { set; get; }
        [StringLength(100)]
        [Column(TypeName = "varchar")]
        public string ten_phong_ban { set; get; }
    }
}