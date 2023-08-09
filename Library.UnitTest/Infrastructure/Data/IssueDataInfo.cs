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
    public class IssueDataInfo
    {
        public static void Init()
        {
            IssueRequestData = new IssueRequest()
            {
                BookId = 1,
               Id = 1,
                IssuedDate = DateTime.Now.ToUniversalTime(),
                ReturnDate = DateTime.Now.ToUniversalTime(),
                IssuedStatus = Domain.Enum.IssuedStatus.Issued,
                MemberId = 1
            };

            IssueTableData = new EIssueTable()
            {
                BookId = 1,
                Id = 1,
                IssuedDate = DateTime.Now.ToUniversalTime(),
                ReturnDate = DateTime.Now.ToUniversalTime(),
                IssuedStatus = Domain.Enum.IssuedStatus.Issued,
                StaffId = 1,
                MemberId = 1
            };

            IssueTableDataAdd = new EIssueTable()
            {
                BookId = 1,
                //Id = 1,
                IssuedDate = DateTime.Now.ToUniversalTime(),
                ReturnDate = DateTime.Now.ToUniversalTime(),
                IssuedStatus = Domain.Enum.IssuedStatus.Issued,
                StaffId = 1,
                MemberId = 1
            };
            IssueTableDataAddFalse = null;

            IssueResponseData = new IssueResponse()
            {
                BookId = 1,
                Id = 1,
                IssuedDate = DateTime.Now.ToUniversalTime(),
                ReturnDate = DateTime.Now.ToUniversalTime(),
                IssuedStatus = 1,
                StaffId = 1,
                MemberId = 1
            };
           

            IssueResponseDataList = new List<IssueResponse>()
            {
                new IssueResponse()
                {
                    BookId = 1,
                    Id = 1,
                    IssuedDate = DateTime.Now.ToUniversalTime(),
                    ReturnDate = DateTime.Now.ToUniversalTime(),
                    IssuedStatus = 1,
                    StaffId = 1,
                    MemberId = 1
                },
                
            };

            IssueTableResponseList = new List<EIssueTable>()
            {
                new EIssueTable()
                {
                     BookId = 1,
                    Id = 1,
                    IssuedDate = DateTime.Now.ToUniversalTime(),
                    ReturnDate = DateTime.Now.ToUniversalTime(),
                    IssuedStatus = Domain.Enum.IssuedStatus.Issued,
                    StaffId = 1,
                    MemberId = 1
                },
            };
        }
        public static IssueRequest IssueRequestData{ get; private set; } = new IssueRequest();
        public static EIssueTable IssueTableData { get; private set; } = new EIssueTable();
        public static EIssueTable IssueTableDataAdd { get; private set; } = new EIssueTable();
        public static EIssueTable IssueTableDataAddFalse { get; private set; } = new EIssueTable();
        public static IssueResponse IssueResponseData { get; private set; } = new IssueResponse();
        public static List<IssueResponse> IssueResponseDataList { get; private set; } = new List<IssueResponse>();
        public static List<EIssueTable> IssueTableResponseList { get; private set; } = new List<EIssueTable>();
    }
}
