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

namespace WindowsFormsApp1
{
    public partial class Products : Form
    {
        public Products()
        {
            InitializeComponent();
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Products_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            this.loadData();
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            //insert logic
            con.Open();
            bool status = (comboBox1.SelectedIndex==0) ? true : false;

            var sqlQuery = "";
            if (IfProductsExists(con, textBox1.Text)){

                sqlQuery = @"UPDATE [Products] SET [ProductName] = '" + textBox2.Text + "', [ProductStatus] = '" + status + "' WHERE [ProductCode] = '"+ textBox1.Text +"'";

            }
            else {
                sqlQuery = @"INSERT INTO [Stock].[dbo].[Products] ([ProductCode],[ProductName],[ProductStatus]) VALUES
                ('" + textBox1.Text + "','" + textBox2.Text + "','" + status + "')";

            }

            SqlCommand cmd = new SqlCommand(sqlQuery, con);

            cmd.ExecuteNonQuery();
            con.Close();
            loadData();
            
        }

        private bool IfProductsExists(SqlConnection con, String productCode)
        {
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT 1 FROM [Stock].[dbo].[Products] WHERE [ProductCode]= '" + productCode + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            return (dt.Rows.Count > 0) ? true : false; 
        }

        public void loadData(){

            //read data
            SqlConnection con = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Stock].[dbo].[Products]", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            dataGridView1.Rows.Clear();

            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item["ProductCode"].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item["ProductName"].ToString();
                dataGridView1.Rows[n].Cells[2].Value = (bool)item["ProductStatus"] ? "Active" : "Deactive";
            }

        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //textBox1.Text + "','" + textBox2.Text
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            comboBox1.SelectedIndex = dataGridView1.SelectedRows[0].Cells[2].Value.ToString()=="Active"?0:1;

        }

        private void DelBtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
          
            var sqlQuery = "";
            if (IfProductsExists(con, textBox1.Text))
            {
                con.Open();
                sqlQuery = @"DELETE FROM [Products] WHERE [ProductCode] = '" + textBox1.Text + "'";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.ExecuteNonQuery();
                con.Close();

            } else
            {
                MessageBox.Show("Product non exist....");

            }

            loadData();
        }
    }
}
