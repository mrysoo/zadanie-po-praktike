using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlowerShop
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=WIN-UTO8MRF8D69;Initial Catalog=FlowerShop;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadFlowers();
        }

        private void LoadFlowers()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM Цветы", connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void AddFlower()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("INSERT INTO Цветы (Name, Price, Quantity) VALUES (@Name, @Price, @Quantity)", connection);
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Price", decimal.Parse(textBox2.Text));
                command.Parameters.AddWithValue("@Quantity", int.Parse(textBox3.Text));
                command.ExecuteNonQuery();
                LoadFlowers();
            }
        }

        private void DeleteFlower()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE FROM Цветы WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                command.ExecuteNonQuery();
                LoadFlowers();
            }
        }

        private void EditFlower()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE Цветы SET Name = @Name, Price = @Price, Quantity = @Quantity WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Name", textBox1.Text);
                command.Parameters.AddWithValue("@Price", decimal.Parse(textBox2.Text));
                command.Parameters.AddWithValue("@Quantity", int.Parse(textBox3.Text));
                command.Parameters.AddWithValue("@Id", int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                command.ExecuteNonQuery();
                LoadFlowers();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddFlower();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteFlower();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            EditFlower();
        }


    }
}
