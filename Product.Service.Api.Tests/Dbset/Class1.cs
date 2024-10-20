﻿using Microsoft.EntityFrameworkCore;
using Moq;


namespace Product.Service.Api.Tests.Dbset
{
    public static class DbSetMockExtensions
    {
        public static Mock<DbSet<T>> CreateDbSetMock<T>(this IEnumerable<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            dbSetMock.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(sourceList.ToList().Add);
            return dbSetMock;
        }
    }
}
