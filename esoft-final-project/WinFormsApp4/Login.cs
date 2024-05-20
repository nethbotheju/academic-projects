using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace WinFormsApp4
{
    public partial class Login : Form
    {
        private SqlConnection _connection;

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Username is required. Please enter it before continuing.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Password is required. Please enter it before continuing.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SqlConnection con = new SqlConnection("Data Source=DESKTOP-R86KLLA;Initial Catalog=esoft;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("SELECT * FROM users WHERE username = @username AND password = @password", con);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", textBox2.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        Register register = new Register();
                        register.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("The password or Username you entered do not match. Please try again.", "Invalid Login Details", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){ 
                this.Close();
            };
        }
    }
}
