using AutoMapper;
using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<StudentRequest, EStudent>();
            CreateMap<StaffRequest, EStaff>();
            CreateMap<EStudent, StudentResponse>();
            CreateMap<EBook, BookResponse>();
            CreateMap<BookRequest, EBook>();
            CreateMap<StaffUpdateRequest, EStaff>();
            CreateMap<StaffUpdateRequest, EMember>()
                .ForMember(dest => dest.MemberCode, opt => opt.MapFrom(src => src.StaffCode))
                .ForMember(dest => dest.ReferenceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name)).ReverseMap();
            CreateMap<StaffUpdateRequest, EStaff>(); //
            CreateMap<StaffUpdateRequest, EMember>();
            CreateMap<StaffUpdateRequest, ELogin>();
            CreateMap<EMember, MemberResponse>().ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();


            CreateMap<IssueRequest, EIssueTable>();
            CreateMap<EIssueTable, IssueResponse>();

            CreateMap<IssueProducerRequest, EIssueTable>();
        }
    }
}
