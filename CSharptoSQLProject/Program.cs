using System;
using System.Diagnostics;
using CSharptoSQLProjectLibrary;
namespace CSharptoSQLProject {
    class Program {

        void RunProductTest() {
            var conn = new Connection(@"localhost\sqlexpress", "PRS");
            conn.Open();
            Products.Connection = conn;

            var testproduct1 = Products.GetByPk(1);
            Console.WriteLine($"Product {testproduct1.Name} from Vendor {testproduct1.Vendor.Name} is priced at {testproduct1.Price}");

            var Product = Products.GetAll();
            foreach(var p in Product) {
                //Console.WriteLine($"Product{p.Name} from Vendor {p.Vendor.Name} is priced at {p.Price}");


                conn.Close();
            }
        }

        void RunVendorsTest() {
            var conn = new Connection(@"localhost\sqlexpress", "PRS");
            conn.Open();

            Vendors.Connection = conn;

            var vendors = Vendors.GetAll();
            foreach (var v in vendors) {
                Console.WriteLine(v.Name);
            }

            var vendor = Vendors.GetByPk(3);
            Debug.WriteLine(vendor);

            var rip = Vendors.Delete(3);

            var vendorabc = Vendors.GetByPk(1);

            vendorabc.Code = "ABC00";

            

            var success = Vendors.Update(vendorabc);

            var newvendor = new Vendors();
            newvendor.Code = "AHGFT";
            newvendor.Name = "Macys";
            newvendor.Address = "3475 dfdsf st";
            newvendor.City = "Chicago";
            newvendor.State = "Il";
            newvendor.Zip = "55555";
            newvendor.Phone = "234-456-1234";
            newvendor.Email = "wergfiwegif@mail.com";
            success = Vendors.Insert(newvendor);



            conn.Close();
        }
        void RunUsersTest() { 
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
            //pgm.RunVendorsTest();
            pgm.RunProductTest();
        }
    }
}
