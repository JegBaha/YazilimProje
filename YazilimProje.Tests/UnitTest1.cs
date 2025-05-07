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

            // Assertasasa
            DataTable customers = dbHelper.GetCustomers();
            Assert.IsTrue(customers.Rows.Count > 0);

            DataRow lastRow = customers.Rows[customers.Rows.Count - 1];
            Assert.AreEqual(name, lastRow["Name"]);
            Assert.AreEqual(phone, lastRow["Phone"]);
            Assert.AreEqual(email, lastRow["Email"]);
            Assert.AreEqual(address, lastRow["Address"]);
        }

        [TestMethod]
        public void TestGetCustomers()
        {
            // Arrangee
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

            // Act
            DataTable customers = dbHelper.GetCustomers();

            // Assertaasds
            Assert.AreEqual(2, customers.Rows.Count);

            DataRow firstRow = customers.Rows[0];
            Assert.AreEqual(name1, firstRow["Name"]);
            Assert.AreEqual(phone1, firstRow["Phone"]);
            Assert.AreEqual(email1, firstRow["Email"]);
            Assert.AreEqual(address1, firstRow["Address"]);

            DataRow secondRow = customers.Rows[1];
            Assert.AreEqual(name2, secondRow["Name"]);
            Assert.AreEqual(phone2, secondRow["Phone"]);
            Assert.AreEqual(email2, secondRow["Email"]);
            Assert.AreEqual(address2, secondRow["Address"]);
        }
    }
}