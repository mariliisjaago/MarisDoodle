using MarisDoodleLibrary.Contracts.Db;
using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Db;
using MarisDoodleLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Repos
{
    public class SqlOptionRepo : IOptionRepo
    {
        private readonly IDataAccess _db;
        private readonly ConnectionStringData _connectionStringData;

        public SqlOptionRepo(IDataAccess db, ConnectionStringData connectionStringData)
        {
            _db = db;
            _connectionStringData = connectionStringData;
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

        public async Task<PollOptionModel> GetOptionById(int optionId)
        {
            string sql = "select * from dbo.PollOptions where Id = @Id;";

            var option = await _db.Load<PollOptionModel>(sql, new { Id = optionId }, _connectionStringData.SqlConnectionName);

            return option.FirstOrDefault();
        }

        public Task<List<PollOptionModel>> GetPollOptionsForDisplay(int pollId)
        {
            string sql = "select * from dbo.PollOptions where PollId = @PollId;";

            return _db.Load<PollOptionModel>(sql, new { PollId = pollId }, _connectionStringData.SqlConnectionName);
        }

        public Task<List<PollOptionVotingModel>> GetPollOptionsForVoting(int pollId)
        {
            string sql = "select [Id], [Option], [PollId] from dbo.PollOptions where PollId = @PollId;";

            return _db.Load<PollOptionVotingModel>(sql, new { PollId = pollId }, _connectionStringData.SqlConnectionName);
        }

        public Task DeleteOptionFromPoll(int optionId)
        {
            string sql = "delete from dbo.PollOptions where Id = @Id;";

            return _db.Save(sql, new { Id = optionId }, _connectionStringData.SqlConnectionName);
        }


    }
}
