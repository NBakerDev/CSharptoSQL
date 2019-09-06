using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace CSharptoSQLProjectLibrary {
   public class Products {

        public static Connection Connection { get; set; }
        #region SQL Statements
        private const string SqlGetAll = "SELECT * From Products; ";
        private const string SqlGetByPk = "SELECT * From Products where Id = @Id; ";
        private const string SqlDelete = "DELETE from Products Where Id = @Id ";
        private const string SqlInsert = "Insert Products  " +
            "PartNbr, Name, Price, Unit, PhotoPath, VendorId) " +
            "VALUES (@PartNbr, @Name, @Price, @Unit, @PhotoPath, @VendorId); ";
        private const string SqlUpdate = "UPDATE Products Set " +
            " PartNbr = @PartNbr, Name = @Name, Price = @Price, Unit = @Unit, " +
            " PhotoPath = @PhotoPath, VendorId = @VendorId " +
            "Where Id =@Id ";
        #endregion

        public static List<Products> GetAll() {
            var sqlcmd = new SqlCommand(SqlGetAll, Connection.sqlConnection);
            var reader = sqlcmd.ExecuteReader();
            var products = new List<Products>();
            Vendors.Connection = Connection;
            while(reader.Read()) {
                var product = new Products();
                products.Add(product);
                LoadProductFromSql(product, reader);
                
            }
            reader.Close();

            Vendors.Connection = Connection;
            foreach (var prod in products) {
                var vendor = Vendors.GetByPk(prod.VendorId);
                prod.Vendor = vendor;
            }
            return products;

            

            }
        public static Products GetByPk(int id) {
            var sqlcmd = new SqlCommand(SqlGetByPk, Connection.sqlConnection);
            sqlcmd.Parameters.AddWithValue("@Id", id);
            var reader = sqlcmd.ExecuteReader();
            if (!reader.HasRows) {
                reader.Close();
                return null;


            }
            reader.Read();
            var product = new Products();

            LoadProductFromSql(product, reader);

            reader.Close();
            Vendors.Connection = Connection;
            var vendor = Vendors.GetByPk(product.VendorId);
            product.Vendor = vendor;

            return product;

        }
        private static void LoadProductFromSql(Products product, SqlDataReader reader) {
            product.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            product.PartNbr = reader["PartNbr"].ToString();
            product.Name = reader["Name"].ToString();
            product.Price = (decimal)reader["Price"];
            product.Unit = reader["Unit"].ToString();
            product.PhotoPath = reader["PhotoPath"]?.ToString();
            product.VendorId = (int)reader["VendorId"];
        }

        #region Instance Properties
        public int Id { get; private set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
        public int VendorId { get; set; }
        public Vendors Vendor { get; private set; }
        #endregion

        public override string ToString() {
            return $"Id={Id}, PartNbr={PartNbr}, Name={Name}, Price={Price}, Unit={Unit}, VendorId={VendorId}, Vendor={Vendor}";
        }
    }
}
