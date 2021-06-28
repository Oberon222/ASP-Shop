using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        [DisplayName("Show Orders")]
        [Range(1, int.MaxValue, ErrorMessage = "Show orders nust be nore then 0")]
        public int ShowOrder { get; set; }
    }
}
