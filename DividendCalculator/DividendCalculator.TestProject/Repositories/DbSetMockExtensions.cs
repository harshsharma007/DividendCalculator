using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace DividendCalculator.TestProject.Repositories;

public static class DbSetMockExtensions
{
    public static Mock<DbSet<T>> GetMockDbSet<T>(this IEnumerable<T> data) where T : class
    {
        var queryableData = data.AsQueryable();
        var mockSet = new Mock<DbSet<T>>();

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

        return mockSet;
    }
}
