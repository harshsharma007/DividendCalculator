using AutoFixture;
using AutoFixture.Xunit2;
using DividendCalculator.Models;
using DividendCalculator.Repositories;
using Moq;

namespace DividendCalculator.TestProject.Repositories;

public class PopulateDatabaseTests
{
    [Theory, AutoMoqData]
    public async Task ExecuteAsync_AddsNewTransactionsToDatabase([Frozen] Mock<IDividendCalculatorDbContext> dbContextMock, PopulateDatabase populateDatabase, List<Transaction> transactions)
    {
        // Arrange
        var fixture = new Fixture();
        var mockDbSet = transactions.GetMockDbSet();
        var newTransactions = new List<Transaction>();
        dbContextMock.Setup(x => x.Transactions).Returns(mockDbSet.Object);
        newTransactions.AddRange(transactions);
        newTransactions.Add(fixture.Create<Transaction>());

        // Act
        await populateDatabase.ExecuteAsync(newTransactions);

        // Assert
        mockDbSet.Verify(x => x.AddRange(It.IsAny<Transaction>()), Times.Once);
        dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory, AutoMoqData]
    public async Task ExecuteAsync_DoesNotAddDuplicateTransactions([Frozen] Mock<IDividendCalculatorDbContext> dbContextMock, PopulateDatabase populateDatabase, List<Transaction> transactions)
    {
        // Arrange
        var fixture = new Fixture();
        var mockDbSet = transactions.GetMockDbSet();
        dbContextMock.Setup(x => x.Transactions).Returns(mockDbSet.Object);

        // Create a new list with existing transactions and a new transaction
        var newTransactions = new List<Transaction>();
        newTransactions.AddRange(transactions);
        var newTransaction = fixture.Create<Transaction>();
        newTransactions.Add(newTransaction);

        // Act
        await populateDatabase.ExecuteAsync(newTransactions);

        // Asset
        mockDbSet.Verify(x => x.AddRange(It.IsAny<IEnumerable<Transaction>>()), Times.Never);
        dbContextMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    [Theory, AutoMoqData]
    public async Task ExecuteAsync_ThrowsException([Frozen] Mock<IDividendCalculatorDbContext> dbContextMock, PopulateDatabase populateDatabase, List<Transaction> transactions)
    {
        dbContextMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception());

        await Assert.ThrowsAsync<NotSupportedException>(() => populateDatabase.ExecuteAsync(transactions));
    }
}
