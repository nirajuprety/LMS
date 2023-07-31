﻿using IdentityServer4.EntityFramework.Options;
using Library.Infrastructure.Repository;
using Library.UnitTest.Application.DTO.Request;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Test.Infrastructure.Mapper;
using System;

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

            databaseContext.SaveChanges();

            mockDbContext = databaseContext;
            //allows other parts of the test code to access the in-memory database context.
        }

        public void Dispose()
        {
            MapperHelper._isUnitTest = false;
            mockDbContext.Database.EnsureDeleted();
            // clean up test data from the database
        }
    }
}
