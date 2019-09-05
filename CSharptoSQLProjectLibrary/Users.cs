using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace CSharptoSQLProjectLibrary {
   public class Users {

        public static Connection Connection { get; set; }

        public static bool Update(Users user) {
            var sql = "UPDATE Users Set " +
              " Username = @Username, " +
              " Password = @Password, " +
              " FirstName = @Firstname, " +
              " LastName = @Lastname, " +
              " Phone = @Phone, " +
              " Email = @Email, " +
              " IsAdmin = @IsAdmin, " +
              " IsReviewer = @IsReviewer " +
              " Where Id = @Id";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            sqlcmd.Parameters.AddWithValue("@Username", user.Username);
            sqlcmd.Parameters.AddWithValue("@Password", user.Password);
            sqlcmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            sqlcmd.Parameters.AddWithValue("@Lastname", user.LastName);
            sqlcmd.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
            sqlcmd.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
            sqlcmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            sqlcmd.Parameters.AddWithValue("@IsReviewer", user.IsReviewer);
            sqlcmd.Parameters.AddWithValue("@Id", user.Id); //Refactor this
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }

        public static bool Insert(Users user) {
            var sql = "Insert into Users " +
               "(Username, Password, FirstName, LastName, Phone, Email, IsAdmin, IsReviewer) " +
               " VALUES " +
              "(@Username, @Password, @FirstName, @LastName, @Phone, @Email, @IsAdmin, @IsReviewer)";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            sqlcmd.Parameters.AddWithValue("@Username", user.Username);
            sqlcmd.Parameters.AddWithValue("@Password", user.Password);
            sqlcmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            sqlcmd.Parameters.AddWithValue("@Lastname", user.LastName);
            sqlcmd.Parameters.AddWithValue("@Phone", (object)user.Phone ?? DBNull.Value);
            sqlcmd.Parameters.AddWithValue("@Email", (object)user.Email ?? DBNull.Value);
            sqlcmd.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            sqlcmd.Parameters.AddWithValue("@IsReviewer", user.IsReviewer); //Refactor this 
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);

            

            

        }

        public static bool Delete(int id) {
            var sql = "DELETE from Users where Id = @Id;";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            var rowsAffected = sqlcmd.ExecuteNonQuery();
            return (rowsAffected == 1);
            
        }

        public static Users GetByPk(int id) {
            var sql = "SELECT * from Users where Id = @Id";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                return null;


            }
            reader.Read();
            var user = new Users();

            LoadUserFromSql(user, reader);

            reader.Close();
            return user;
        }
        
        public static List<Users> GetAll() {
            var sql = "SELECT * from Users;";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            var reader = sqlcmd.ExecuteReader();
            var users = new List<Users>();
            while(reader.Read()) {
                var user = new Users();
                users.Add(user);

                LoadUserFromSql(user, reader);

                
                
            }
            reader.Close();
            return users;
        }

        public static Users Login(string username, string password) {
            var sql = "SELECT * from Users where Username = @Username and Password = @Password";
            var sqlcmd = new SqlCommand(sql, Connection._Connection);
            sqlcmd.Parameters.AddWithValue("Username", username);
            sqlcmd.Parameters.AddWithValue("Password", password);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                return null;

            }
            reader.Read();
            var user = new Users();

            LoadUserFromSql(user, reader);

            reader.Close();
            return user;



            
        }

    

        private static void LoadUserFromSql(Users user, SqlDataReader reader) {
            user.Id = (int)reader["Id"];
            user.Username = reader["Username"].ToString();
            user.Password = (string)reader["Password"];
            user.FirstName = (string)reader["Firstname"];
            user.LastName = (string)reader["Lastname"];
            user.Phone = reader["Phone"]?.ToString();
            user.Email = reader["Email"]?.ToString();
            user.IsAdmin = (bool)reader["IsAdmin"];
            user.IsReviewer = (bool)reader["IsReviewer"];

        }

        public int Id { get; private set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsReviewer { get; set; }

        public Users() {

        }

        public override string ToString() {
            return $"Id={Id}, Username={Username}, Password={Password}, " +
                $"Name={FirstName} {LastName}, Admin?={IsAdmin}, Reviewer?={IsReviewer}";
        }
    }
}
