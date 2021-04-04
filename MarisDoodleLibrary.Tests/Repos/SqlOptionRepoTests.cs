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
    public class SqlOptionRepoTests
    {
        private readonly SqlOptionRepo _sut;
        private readonly Mock<IDataAccess> _db = new Mock<IDataAccess>();
        private readonly ConnectionStringData _connectionStringData = new ConnectionStringData();
        
        public SqlOptionRepoTests()
        {
            _connectionStringData.SqlConnectionName = "Development";
            _sut = new SqlOptionRepo(_db.Object, _connectionStringData);
        }
        
        [Fact]
        public async Task AddOptionsToPoll_ShouldAddOneOption()
        {
            // Arrange
            var pollId = 2;
            var option = new PollOptionModel { Id = 1, Option = "testOption", PollId = pollId };
            var listOfOptions = new List<PollOptionModel>() { option };

            string sql = "insert into dbo.PollOptions ([Option], PollId, CreatedOn) " +
                "values (@Option, @PollId, @CreatedOn);";

            _db.Setup(x => x.Save(sql,
                new { Option = option.Option, PollId = option.PollId, CreatedOn = option.CreatedOn },
                    _connectionStringData.SqlConnectionName));

            // Act
            await _sut.AddOptionsToPoll(pollId, listOfOptions);

            // Assert
            _db.Verify(x => x.Save(sql,
                new { Option = option.Option, PollId = option.PollId, CreatedOn = option.CreatedOn },
                    _connectionStringData.SqlConnectionName),
                    Times.Exactly(1));
        }
    }
}
