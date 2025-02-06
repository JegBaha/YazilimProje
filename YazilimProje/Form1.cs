using System;
using System.Data;
using System.Windows.Forms;

namespace YazilimProje
{
    public partial class Form1 : Form
    {
        DatabaseHelper dbHelper = new DatabaseHelper();

        public Form1()
        {
            InitializeComponent();
            CreateDatabaseTables(); // Tablo oluşturmayı burada çağırabilirsiniz.
            LoadCustomers();
        }

        private void CreateDatabaseTables()
        {
            try
            {
                dbHelper.CreateCustomersTable();
                MessageBox.Show("Customers tablosu başarıyla oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tablo oluşturma hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadCustomers()
        {
            dgvCustomers.DataSource = dbHelper.GetCustomers();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                // TextBox'lardan bilgileri al
                string name = txtCustomerName.Text;
                string phone = txtCustomerPhone.Text;
                string email = txtCustomerEmail.Text;
                string address = txtCustomerAddress.Text;

                // DatabaseHelper sınıfından nesne oluştur
                DatabaseHelper dbHelper = new DatabaseHelper();

                // Yeni müşteri ekle
                dbHelper.AddCustomer(name, phone, email, address);

                // Güncellenmiş müşteri listesini yükle
                dgvCustomers.DataSource = dbHelper.GetCustomers();

                // TextBox'ları temizle
                txtCustomerName.Clear();
                txtCustomerPhone.Clear();
                txtCustomerEmail.Clear();
                txtCustomerAddress.Clear();

                MessageBox.Show("Müşteri başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }
    }
}