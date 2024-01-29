using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;

namespace Demo.Pl.Mapping_Profiles
{
    public class UserProfiler:Profile
    {
        public UserProfiler()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
        
    }
}
