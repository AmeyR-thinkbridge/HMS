using AutoMapper;
using Hms.Models;
using Hms.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Service.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Table, TableViewModel>().ReverseMap();
        }
    }
}
