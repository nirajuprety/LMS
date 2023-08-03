using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UnitTest.Infrastructure.Data
{
    public static class StudentSettingDataInfo
    {
        public static void init()
        {
            eStudent = new EStudent
            {
                Email = "m@g.com",
                Faculty = "BSC Hons in Computing",
                FullName = "Manish Nagarkoti",
                RollNo = 1,
                StudentCode = 1234,
                UpdatedBy = "StaffID",
            };
            eStudentWithID = new EStudent
            {
                Id = 1,
                Email = "m@g.com",
                Faculty = "BSC Hons in Computing",
                FullName = "Manish Nagarkoti",
                RollNo = 1,
                StudentCode = 1234,
                UpdatedBy = "StaffID",
            };
            studentRequest = new StudentRequest()
            {
                Email = "m@g.com",
                Faculty = "BSC Hons in Computing",
                FullName = "Manish Nagarkoti",
                RollNo = 1,
                StudentCode = 1234,
                UpdatedBy = "StaffID",
            };
            eStudentList = new List<EStudent> { new EStudent() {
                    Id = 1,
                    Email = "m@g.com",
                    Faculty = "BSC Hons in Computing",
                    FullName = "Manish Nagarkoti",
                    RollNo = 1,
                    StudentCode = 1234,
                    UpdatedBy = "StaffID",
            } };
            studentResponseList = new List<StudentResponse>()
            {
                new StudentResponse()
                {
                    Id = 1,
                    Email = "m@g.com",
                    Faculty = "BSC Hons in Computing",
                    FullName = "Manish Nagarkoti",
                    RollNo = 1,
                    StudentCode = 1234
                }
            };
            studentResponse = new StudentResponse()
            {
                Id = 1,
                Email = "m@g.com",
                Faculty = "BSC Hons in Computing",
                FullName = "Manish Nagarkoti",
                RollNo = 1,
                StudentCode = 1234
            };
            eMember = new EMember()
            {
                Id = 1,
                FullName = "Manish Nagarkoti",
                Email = "m@g.com",
                MemberType = Domain.Enum.MemberType.Staff,
                MemberCode = 1,
                ReferenceId = 1
            };
        }

        public static EStudent eStudent { get; set; } = new EStudent();
        public static EStudent eStudentWithID { get; set; } = new EStudent();
        public static StudentRequest studentRequest { get; set; } = new StudentRequest();
        public static List<EStudent> eStudentList { get; set; } = new List<EStudent>();
        public static List<StudentResponse> studentResponseList { get; set; } = new List<StudentResponse> { new StudentResponse() { } };
        public static StudentResponse studentResponse { get; set; } = new StudentResponse();
        public static EMember eMember { get; set; } = new EMember();

        
    }
}
