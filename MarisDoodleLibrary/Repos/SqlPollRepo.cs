using MarisDoodleLibrary.Contracts.Db;
using MarisDoodleLibrary.Contracts.Repos;
using MarisDoodleLibrary.Db;
using MarisDoodleLibrary.Models;
using System.Linq;
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

        public async Task<int> CreateBasicPollAndReturnId(PollModel poll)
        {
            string sql = "insert into dbo.Polls (PollName, CreatedOn) values (@PollName, @CreatedOn); " +
                "select SCOPE_IDENTITY();";

            var pollId = await _db.Load<int>(sql, new { PollName = poll.PollName, CreatedOn = poll.CreatedOn }, _connectionStringData.SqlConnectionName);

            return pollId.ToList().FirstOrDefault();
        }

        public async Task<PollModel> GetBasicPoll(int id)
        {
            string sql = "select * from dbo.Polls where Id = @Id;";

            var data = await _db.Load<PollModel>(sql, new { Id = id }, _connectionStringData.SqlConnectionName);

            return data.ToList().FirstOrDefault();
        }


    }
}
