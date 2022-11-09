using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hms.Models
{
    [Index("Name", IsUnique = true)]
    public class Dish
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DishId { get; set; }
        public string? Name { get; set; }
        public double MRP { get; set; }
        [ForeignKey("DishCategroy")]
        public int? DishCategroyId { get; set; }
        public virtual DishCategroy? DishCategroy { get; set; }

        
    }
}
