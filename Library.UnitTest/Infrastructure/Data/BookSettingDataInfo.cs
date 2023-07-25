using Library.Application.DTO.Request;
using Library.Application.DTO.Response;
using Library.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.UnitTest.Application.DTO.Request
{
    public static class BookSettingDataInfo
    {
        public static void Init()
        {
            BookSetting = new List<EBook>()
            {
                new EBook()
                {
                    Id = 1,
                    Title = "Sample Book",
                    Author = "John Doe",
                    ISBN = "1234567890",
                    PublicationDate = DateTime.Now,
                    UpdatedBy = "User123",
                    IsActive = true,
                },
            };

            SuccessBookSetting = new EBook()
            {
                Title = "Sample Book",
                Author = "John Doe",
                ISBN = "1234567890",
                PublicationDate = DateTime.Now,
                UpdatedBy = "User123",
                IsActive = true,
            };

            SuccessBookSettings = new EBook()
            {
                Id = 1,
                Title = "Sample Book",
                Author = "John Doe",
                ISBN = "1234567890",
                PublicationDate = DateTime.Now,
                UpdatedBy = "User123",
                IsActive = true,
            };

            BookResponse = new List<BookResponse>()
            {
                new BookResponse()
                {
                    // Add properties for BookResponse, if needed.
                }
            };

            BookRequest = new BookRequest()
            {
                Title = "Sample Book",
                Author = "John Doe",
                ISBN = "1234567890",
                PublicationDate = DateTime.Now,
                UpdatedBy = "User123",
                
                
            };

            BookRequests = new BookRequest()
            {
                // Add properties for BookRequests, if needed.
            };
        }

        public static List<EBook> BookSetting { get; private set; } = new List<EBook>();
        public static EBook SuccessBookSetting { get; private set; } = new EBook();
        public static EBook SuccessBookSettings { get; private set; } = new EBook();
        public static List<BookResponse> BookResponse { get; private set; } = new List<BookResponse>();
        public static BookRequest BookRequest { get; private set; } = new BookRequest();
        public static BookRequest BookRequests { get; private set; } = new BookRequest();
    }
}
