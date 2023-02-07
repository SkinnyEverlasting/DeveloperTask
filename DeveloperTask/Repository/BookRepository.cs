using Dapper;
using DeveloperTask.Models;
using System.Data;
using System.Data.SqlClient;

namespace DeveloperTask.Repository
{
    public class BookRepository : IBookRepository
    {

        private IDbConnection dbConnection;

        public BookRepository(IConfiguration configuration)
        {
            this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public bool AddBookInformation(string title, string description, string imagePath, float rating, DateTime releaseDate, List<AuthorModel> authors)
        {
            var authorsTable = new DataTable();
            authorsTable.Columns.Add("ID", typeof(int));
            authorsTable.Columns.Add("FirstName", typeof(string));
            authorsTable.Columns.Add("LastName", typeof(string));
            authorsTable.Columns.Add("BirthDate", typeof(DateTime));
            for(int i = 0; i < authors.Count; i++)
            {
                authorsTable.Rows.Add(i + 1, authors[i].FirstName, authors[i].LastName, authors[i].BirthDate);
            }

            var p = new DynamicParameters();
            p.Add("title", title);
            p.Add("description", description);
            p.Add("imagePath", imagePath);
            p.Add("rating", rating);
            p.Add("releaseDate", releaseDate);
            p.Add("authorsList", authorsTable.AsTableValuedParameter("UT_Authors"));
            p.Add("returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            dbConnection.Execute("Add_Book_Information", p, commandType: CommandType.StoredProcedure);
            var success = p.Get<int>("returnvalue");
            return success == 0;
        }

        public ResultStatus RequestToTakeBook(int bookId)
        {
            var p = new DynamicParameters();
            p.Add("bookId", bookId);
            p.Add("returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            dbConnection.Execute("Take_Book", p, commandType: CommandType.StoredProcedure);
            var status = (ResultStatus)p.Get<int>("returnvalue");
            return status;
        }

        public ResultStatus ReturnBackBook(int bookId)
        {
            var p = new DynamicParameters();
            p.Add("bookId", bookId);
            p.Add("returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            dbConnection.Execute("Return_Book", p, commandType: CommandType.StoredProcedure);
            var status = (ResultStatus)p.Get<int>("returnvalue");
            return status;
        }

        public bool UpdateBookInformation(int bookId, string title, string description, string imagePath, float rating, DateTime releaseDate, List<AuthorModel> authors)
        {
            var authorsTable = new DataTable();
            authorsTable.Columns.Add("ID", typeof(int));
            authorsTable.Columns.Add("FirstName", typeof(string));
            authorsTable.Columns.Add("LastName", typeof(string));
            authorsTable.Columns.Add("BirthDate", typeof(DateTime));
            for (int i = 0; i < authors.Count; i++)
            {
                authorsTable.Rows.Add(i + 1, authors[i].FirstName, authors[i].LastName, authors[i].BirthDate);
            }

            var p = new DynamicParameters();
            p.Add("bookId", bookId);
            p.Add("title", title);
            p.Add("description", description);
            p.Add("imagePath", imagePath);
            p.Add("rating", rating);
            p.Add("releaseDate", releaseDate);
            p.Add("authorsList", authorsTable.AsTableValuedParameter("UT_Authors"));
            p.Add("returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            dbConnection.Execute("Update_Book_Information", p, commandType: CommandType.StoredProcedure);
            var success = p.Get<int>("returnvalue");
            return success == 0;
        }

        public bool CheckIfBookExists(int bookId)
        {
            var p = new DynamicParameters();
            p.Add("bookId", bookId);
            p.Add("returnvalue", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
            dbConnection.Execute("Check_If_Book_Exists", p, commandType: CommandType.StoredProcedure);
            var success = p.Get<int>("returnvalue");
            return success == 0;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return dbConnection.Query<Book>("Get_All_Books", commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Book> GetBooksByTitle(string title)
        {
            var p = new DynamicParameters();
            p.Add("title", title);
            return dbConnection.Query<Book>("Get_Books_By_Title", p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<BookModel> GetAllBooksInformation()
        {
            return dbConnection.Query<BookModel>("Get_All_Books_Information", commandType: CommandType.StoredProcedure);
        }

        public BookDetails GetBookDetails(int bookId)
        {
            var p = new DynamicParameters();
            p.Add("bookId", bookId);
            var result = dbConnection.Query<BookDetails>("Get_Book_Details", p, commandType: CommandType.StoredProcedure).FirstOrDefault(); ;
            return result;
        }
    }
}
