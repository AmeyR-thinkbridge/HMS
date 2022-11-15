using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class DishViewModel
    {
        [Required(ErrorMessage = "Please enter name of the Dish"), MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter MRP of the Dish")]
        public double MRP { get; set; }
        public int? DishCategroyId { get; set; }
    }
}
