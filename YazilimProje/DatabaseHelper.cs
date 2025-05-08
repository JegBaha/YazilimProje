using System.Data;
using System.Data.SqlClient;
/// </summary>ğ
public class DatabaseHelper
{
    private string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";

    public void CreateCustomersTable()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = @"
                IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Customers' AND xtype='U')
                CREATE TABLE Customers (
                    Id INT PRIMARY KEY IDENTITY,
                    Name NVARCHAR(100),
                    Phone NVARCHAR(20),
                    Email NVARCHAR(100),
                    Address NVARCHAR(255)
                )";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

    public void AddCustomer(string name, string phone, string email, string address)
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = "INSERT INTO Customers (Name, Phone, Email, Address) VALUES (@name, @phone, @email, @address)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.ExecuteNonQuery();
            }
        }
    }

    

    public void ClearCustomersTable()
    {
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            string query = "DELETE FROM Customers";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }
}