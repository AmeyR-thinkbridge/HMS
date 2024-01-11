using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Models.ViewModels
{
    public class DishSearchViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? DishCategoryId { get; set; }
        public bool OrderByAscending { get; set; } = true;
        public int? PageNumber { get; set; }
        public int? DishNumber { get; set; }
        public bool? IsExcel { get; set; }
    }
}
