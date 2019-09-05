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
            var success = Users.Delete(3);
            var user3 = Users.GetByPk(3);
            Debug.WriteLine(user3);
            var userpef = Users.Login("testuser1", "testpass1");
            Console.WriteLine(userpef);
            var userfail = Users.Login("asdasd", "asada");
            Console.WriteLine(userfail?.ToString() ?? "Not Founderino");

            var newuser = new Users();
            newuser.Username = "ABC44554";
            newuser.Password = "XYZ";
            newuser.FirstName = "Normal";
            newuser.LastName = "Usersdfsdf";
            newuser.IsAdmin = false;
            newuser.Phone = "555-123-1234";
            newuser.Email = "dabfhsdfadofjodisjforf@gmail.com";
            newuser.IsReviewer = true;
            success = Users.Insert(newuser);

            var newuser1 = Users.GetByPk(2);
            newuser.Username = "ABCdfgd444";
            newuser.Password = "XYZ44";
            newuser.FirstName = "Normal";
            newuser.LastName = "Usersdfsdf";
            newuser.IsAdmin = false;
            newuser.Phone = "555-123-1234";
            newuser.Email = "dabfhsdfadofjodisjforf@gmail.com";
            newuser.IsReviewer = true;
           
            success = Users.Update(newuser1);

            conn.Close();


           



        }

        


        static void Main(string[] args) {
            var pgm = new Program();
            pgm.Run();
        }
    }
}
