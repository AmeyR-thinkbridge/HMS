#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class DishCategoryViewModel
    {
        [Required(ErrorMessage ="Enter the CategoryCode")]
        public string CategoryCode { get; set; }
        [Required(ErrorMessage = "Enter the Description")]
        [MaxLength()]
        public string Description { get; set; }
    }
}
