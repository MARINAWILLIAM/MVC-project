using AutoMapper;
using Demo.DAL.Models;
using Demo.Pl.ViewModels;
using Microsoft.AspNetCore.Identity;
using System;

namespace Demo.Pl.Mapping_Profiles
{
    public class RoleProfiler:Profile 
    {
        public RoleProfiler()
        {
                 CreateMap<RoleViewModel,IdentityRole>()
                .ForMember(d=>d.Name,s=>s.MapFrom(s=>s.RoleName))
                .ReverseMap();
        }

        
    }
}
