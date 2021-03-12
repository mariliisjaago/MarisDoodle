using MarisDoodleLibrary.Contracts.Db;
using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Db;
using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Repos
{
    public class SqlPollRepo : IPollRepo
    {
        private readonly IDataAccess _db;
        private readonly ConnectionStringData _connectionStringData;

        public SqlPollRepo(IDataAccess db, ConnectionStringData connectionStringData)
        {
            _db = db;
            _connectionStringData = connectionStringData;
        }

        public Task<int> CreateBasicPollAndReturnId(PollModel poll)
        {
            string sql = "insert into dbo.Polls (PollName, CreatedOn) values (@PollName, @CreatedOn) " +
                "select SCOPE_IDENTITY();";

            return _db.Save(sql, new { PollName = poll.PollName, CreatedOn = poll.CreatedOn }, _connectionStringData.SqlConnectionName);
        }

        public async Task AddOptionsToPoll(int pollId, List<PollOptionModel> pollOptions)
        {
            string sql = "insert into dbo.PollOptions ([Option], PollId, CreatedOn) " +
                "values (@Option, @PollId, @CreatedOn);";

            foreach (var option in pollOptions)
            {
                await _db.Save(sql,
                    new { Option = option.Option, PollId = pollId, CreatedOn = option.CreatedOn },
                    _connectionStringData.SqlConnectionName);
            }
        }
    }
}
