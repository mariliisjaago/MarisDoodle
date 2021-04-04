using MarisDoodleLibrary.Contracts.Db;
using MarisDoodleLibrary.Db;
using MarisDoodleLibrary.Models;
using MarisDoodleLibrary.Repos;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MarisDoodleLibrary.Tests.Repos
{
    public class SqlPollRepoTests
    {
        private readonly SqlPollRepo _sut;
        private readonly Mock<IDataAccess> _db = new Mock<IDataAccess>();
        private readonly ConnectionStringData _connectionStringData = new ConnectionStringData(); 
        
        public SqlPollRepoTests()
        {
            _connectionStringData.SqlConnectionName = "Default";
            _sut = new SqlPollRepo(_db.Object, _connectionStringData);
        }
        
        [Fact]
        public async Task CreateBasicPollAndReturnId_ShouldWork()
        {
            // Arrange
            int expected = 1;

            PollModel poll = new PollModel { PollName = "testPollName", CreatedOn = DateTime.Parse("2021-03-23 00:00:00") };
            
            string sql = "insert into dbo.Polls (PollName, CreatedOn) values (@PollName, @CreatedOn); " +
                "select SCOPE_IDENTITY();";

            _db.Setup(x => x.Load<int>(sql,
                new { PollName = poll.PollName, CreatedOn = poll.CreatedOn },
                _connectionStringData.SqlConnectionName))
                .Returns(Task.FromResult(new List<int> { 1 }));

            // Act
            await _sut.CreateBasicPollAndReturnId(poll);

            // Assert
            _db.Verify(x => x.Load<int>(sql,
                new { PollName = poll.PollName, CreatedOn = poll.CreatedOn },
                _connectionStringData.SqlConnectionName), Times.Exactly(1));
        }
    }
}
