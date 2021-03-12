using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarisDoodleLibrary.Contracts.Db
{
    public interface IDataAccess
    {
        Task<List<T>> Load<T>(string sqlStatement, object parameters, string connectionStringName);
        Task<int> Save(string sqlStatement, object parameters, string connectionStringName);
    }
}