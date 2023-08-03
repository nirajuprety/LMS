
using Library.Infrastructure.Repository;
using Library.UnitTest.Application.DTO.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Infrastructure.Mapper;
using System;
using Library.UnitTest.Infrastructure.Data;
using Library.Domain.Entities;

namespace Library.UnitTest
{
    public class DatabaseFixture : IDisposable
    {
        public DatabaseContext mockDbContext;

        public DatabaseFixture()
        {
            MapperHelper._isUnitTest = true;

            // ConfigurationStoreOptions storeOptions = new ConfigurationStoreOptions();
            // ^ We don't need this line since it's not used.

            var serviceCollection = new ServiceCollection();
            // Add any other services or dependencies you need for your tests to this service collection.

            var builder = new DbContextOptionsBuilder<DatabaseContext>();
            builder.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            builder.UseApplicationServiceProvider(serviceCollection.BuildServiceProvider());

            var databaseContext = new DatabaseContext(builder.Options);
            databaseContext.Database.EnsureCreated();

            #region Feed BookInfo Data
            BookSettingDataInfo.Init();
            var bookSettings = BookSettingDataInfo.BookSetting;
            databaseContext.Books.AddRange(bookSettings);
            #endregion
            #region Feed MemberInfo Data
            MemberEntityData.Init();
            var member = MemberEntityData.MemberList;
            databaseContext.Members.AddRange(member);
            #endregion

            #region Feed LoginInfo Data
            LoginSettingDataInfo.Init();
            var LoginUser= LoginSettingDataInfo.LoginData;
            databaseContext.Logins.AddRange(LoginUser);
            #endregion 
            
            #region Feed LoginInfo Data
            LoginSettingDataInfo.Init();
            var LoginStaffUser= LoginSettingDataInfo.LoginStaffData;
            databaseContext.Logins.AddRange(LoginStaffUser);
            #endregion

            //Feed StaffInfo Data
            StaffDataInfo.Init();
            var eStaff = StaffDataInfo.eStaffList;
            databaseContext.Staffs.AddRange(eStaff);
            var eLogin = StaffDataInfo.eLogin;
           databaseContext.Logins.AddRange(eLogin);

            //feed StudentInfo Datalist
            StudentSettingDataInfo.init();
            var eStudentList = StudentSettingDataInfo.eStudentList;
            databaseContext.Students.AddRange(eStudentList);
                       
            databaseContext.SaveChanges();

            mockDbContext = databaseContext;
        }

        public void Dispose()
        {
            MapperHelper._isUnitTest = false;
            mockDbContext.Database.EnsureDeleted();
            // clean up test data from the database
        }
    }
}