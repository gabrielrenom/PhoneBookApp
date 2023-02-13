using Microsoft.EntityFrameworkCore;
using Moq;
using PhoneBookApp.DAL.Context;
using PhoneBookApp.DAL.Entities;
using PhoneBookApp.DAL.Repository;

namespace PhoneBookApp.Test
{
    public class ContactsRepositoryTests
    {     
        [Fact]
        public async Task TestCreateContact()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<PhoneBookDbContext>()
                                .UseInMemoryDatabase(databaseName: "TestDb")
                                .Options;

            var mockSet = new Mock<DbSet<Contact>>();
            var mockContext = new Mock<PhoneBookDbContext>(options);
            mockContext.Setup(m => m.Contacts).Returns(mockSet.Object);

            var mockGenericRepository = new Mock<IGenericRepository<Contact>>();
            mockGenericRepository.Setup(x => x.Entities).Returns(mockSet.Object);

            var repository = new ContactsRepository(mockContext.Object);
            repository.SetDbSet(mockSet.Object);


            // Set up the mock IGenericRepository
            mockGenericRepository.Setup(x => x.Insert(It.IsAny<Contact>()))
                .ReturnsAsync(new Contact { Id = 1, Name = "Steve", Surname = "Jobs" });

            // Act
            var result = await repository.CreateContact(new Contact { Name = "Steve", Surname = "Jobs" });

            //// Assert
            Assert.NotNull(result);
            Assert.Equal("Steve", result.Name);
            Assert.Equal("Jobs", result.Surname);
        }
    }
}