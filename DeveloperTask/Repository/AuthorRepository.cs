using Dapper;
using DeveloperTask.Models;
using System.Data;
using System.Data.SqlClient;

namespace DeveloperTask.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private IDbConnection dbConnection;

        public AuthorRepository(IConfiguration configuration)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public IEnumerable<Author> GetAllAuthors()
        {
            return dbConnection.Query<Author>("Get_All_Authors", commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Author> GetAuthorsByName(string name)
        {
            var p = new DynamicParameters();
            p.Add("name", name);
            return dbConnection.Query<Author>("Get_Authors_By_Name", p, commandType: CommandType.StoredProcedure);
        }
    }
}
