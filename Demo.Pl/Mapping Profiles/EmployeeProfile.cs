using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;

namespace Demo.Pl.Mapping_Profiles
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
