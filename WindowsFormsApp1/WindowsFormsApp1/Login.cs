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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Username.Text = "";
            Password.Clear();
            Username.Focus();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // TO-DO: Check Login username & Password
            SqlConnection con = new SqlConnection("Data Source=LENOVO-PC\\SQLEXPRESS;Initial Catalog=Stock;Integrated Security=True");
            SqlDataAdapter sda = new SqlDataAdapter(@"SELECT * FROM [Stock].[dbo].[Login] Where UserName = '"+ Username.Text + "' and Password = '"+ Password.Text+ "'",con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                this.Hide();
                StockMain main = new StockMain();
                main.Show();
            }
            else
            {
                MessageBox.Show("Invalid UserName & Password","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                button1_Click(sender, e);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Username_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
