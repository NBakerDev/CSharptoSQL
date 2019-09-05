using System;
using System.Diagnostics;
using CSharptoSQLProjectLibrary;
namespace CSharptoSQLProject {
    class Program {
        void Run() {

            var conn = new Connection(@"localhost\sqlexpress", "PRS");

            conn.Open();

            Users.Connection = conn;
            var users = Users.GetAll();
            foreach(var usr in users) {
                Console.WriteLine(usr);
            }
            var user = Users.GetByPk(2);
            Debug.WriteLine(user);
            var usernf = Users.GetByPk(3487);
            var userpef = Users.Login("testuser1", "testpass1");
            Console.WriteLine(userpef);
            var userfail = Users.Login("asdasd", "asada");
            Console.WriteLine(userfail?.ToString() ?? "Not Founderino");
            conn.Close();






        }


        static void Main(string[] args) {
            var pgm = new Program();
            pgm.Run();
        }
    }
}
