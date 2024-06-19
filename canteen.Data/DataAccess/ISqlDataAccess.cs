using System.Collections.Generic;
using System.Threading.Tasks;

namespace canteen.Data.DataAccess
{
    public interface ISqlDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters, string connectionId = "conn");
        Task SaveData<T>(string spName, T parameters, string connectionId = "conn");

        // Define the LoadData method for retrieving data as needed
        Task<IEnumerable<T>> LoadData<T, U>(string sql, U parameters, string connectionId = "conn");
        Task<T> LoadData<T>(string sql, object parameters);


    }
}