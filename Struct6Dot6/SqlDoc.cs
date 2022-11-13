using Microsoft.Data.SqlClient;
using System.Data;

namespace Struct6Dot6
{
    sealed public class SqlDoc
    {

        static private readonly string _path = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\1iltad\source\repos\Struct6Dot6\Struct6Dot6\StudentsSql.mdf;Integrated Security=True";
        private SqlConnection _connection = new SqlConnection(_path);
        SqlCommand _command;

        public void Add(Student student,double avg,Group group)
        {
            _connection.Open();
            _command = new SqlCommand($"INSERT INTO [StudentSql] (Surname, Name, Patronymic, Average, Groups) VALUES(N'{student.Surname}', N'{student.Name}', N'{student.Patronymic}', '{avg.ToString().Replace(",",".")}', N'{group.Title}')", _connection);
            _command.ExecuteNonQuery();
            _connection.Close();
        }
        public int Count()
        {
            _connection.Open();
            _command = new SqlCommand("SELECT COUNT (id) FROM [StudentSql]",_connection);
            int count = int.Parse(_command.ExecuteScalar().ToString());
            _connection.Close();
            return count;
        }
        public int MoreThanFour()
        {
            _connection.Open();
            _command = new SqlCommand($"SELECT COUNT (Average) FROM [StudentSql] WHERE Average  > 4",_connection);
            int count = int.Parse(_command.ExecuteScalar().ToString());
            _connection.Close();
            return count;
        }
        public int SmallerThanFour()
        {
            return Count() - MoreThanFour();
        }
        public int GoodMoney()
        {
            _connection.Open();
            _command = new SqlCommand($"SELECT COUNT (Average) FROM [StudentSql] WHERE Average  = 5", _connection);
            int count = int.Parse(_command.ExecuteScalar().ToString());
            _connection.Close();
            return count;
        }
        public void Clear()
        {
            _connection.Open();
            _command = new SqlCommand("DELETE [StudentSql]", _connection);
            _command.ExecuteNonQuery();
            _connection.Close();
        }
        public DataView Sort()
        {
            _connection.Open();
            _command = new SqlCommand("SELECT * FROM [StudentSQL] ORDER BY [Groups]",connection: _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(_command);
            DataTable dt = new DataTable("Students");
            adapter.Fill(dt);
            _connection.Close();
            return dt.DefaultView;
        }
        public DataView GoodMarkups()
        {
            _connection.Open();
            _command = new SqlCommand("SELECT * FROM [StudentSQL] WHERE [AVERAGE] > 4", connection: _connection);
            SqlDataAdapter adapter = new SqlDataAdapter(_command);
            DataTable dt = new DataTable("Students");
            adapter.Fill(dt);
            _connection.Close();
            return dt.DefaultView;
        }
    }

}
