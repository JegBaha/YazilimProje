using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using YazilimProje; // DatabaseHelper sınıfının bulunduğu namespace 

namespace YazilimProje.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private DatabaseHelper dbHelper;

        [TestInitialize]
        public void Setup()
        {
            dbHelper = new DatabaseHelper();
            dbHelper.CreateCustomersTable();
            dbHelper.ClearCustomersTable();
        }

        [TestMethod]
        public void TestAddCustomer()
        {
            // Arrange
            string name = "Test Customer";
            string phone = "1234567890";
            string email = "test@example.com";
            string address = "Test Address";

            // Act
            dbHelper.AddCustomer(name, phone, email, address);

            // Assert
            DataTable customers = dbHelper.GetCustomers();
            Assert.AreEqual(0, customers.Rows.Count, "Expected no rows, but found some."); // Deliberately wrong: expects 0, actual should be 1
        }

        [TestMethod]
        public void TestGetCustomers()
        {
            // Arrange
            string name1 = "Customer 11";
            string phone1 = "1234567890";
            string email1 = "customer1@example.com";
            string address1 = "Address 1";

            string name2 = "Customer 22";
            string phone2 = "0987654321";
            string email2 = "customer2@example.com";
            string address2 = "Address 2";

            dbHelper.AddCustomer(name1, phone1, email1, address1);
            dbHelper.AddCustomer(name2, phone2, email2, address2);

            // Assert
            Assert.AreEqual(1, 2, "Deliberate failure for demonstration."); // Hardcoded failure
        }

        [TestMethod]
        public void TestAlwaysFails()
        {
            Assert.IsTrue(false, "This test is designed to fail for Jenkins demonstration.");
        }
    }
}