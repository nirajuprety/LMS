using AutoMapper;
using Library.Application.DTO.Request;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentRequest, EStudent>();
            CreateMap<StaffRequest, EStaff>();
        }
    }
}
