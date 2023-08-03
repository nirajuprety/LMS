using AutoMapper;
using Library.Application.DTO.Response;
using Library.Domain.Entities;
using Library.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UnitTest.Infrastructure.Data
{
    public class MemberEntityData
    {
        public static void Init()
        {

            MemberList = new List<EMember>
            {
                new EMember()
                {
                    Id=1, ReferenceId=1,Email="test@gmail.com",FullName="ram ram",MemberCode=123,MemberType=MemberType.Staff
                },
                new EMember()
                {
                    Id=2, ReferenceId=1,Email="test1@gmail.com",FullName="test ram",MemberCode=113,MemberType=MemberType.Student
                }
            };
            MemberResponse = new EMember()
            {
                Id = 1,
                ReferenceId = 1,
                Email = "test@gmail.com",
                FullName = "ram ram",
                MemberCode = 123,
                MemberType = MemberType.Staff
            };
            ListOfMembers = new List<MemberResponse>()
            {
                new MemberResponse()
            {
                    Id=1,
                    FullName = "Name",
                    MemberCode = 11011,
                    MemberType = 0,
                    ReferenceId = 1,

                },
                new MemberResponse()
        {
                    Id=2,
                    FullName = "Name Name",
                    MemberCode = 11012,
                    MemberType = 1,
                    ReferenceId = 2
                }
    };
            MemberInfo = new MemberResponse()
            {
                Id = 1,
                Email = "test@gmail.com",
                FullName = "ram ram",
                MemberCode = 123,
                MemberType = 0,
                ReferenceId = 1
            };
        }
        public static List<EMember> MemberList { get; set; }
        public static EMember MemberResponse { get; set; }
        public static List<MemberResponse> ListOfMembers { get; set; }
        public static MemberResponse MemberInfo { get; set; }
    }
}
