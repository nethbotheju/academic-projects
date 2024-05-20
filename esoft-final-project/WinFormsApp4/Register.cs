using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.ConstrainedExecution;

namespace WinFormsApp4
{
    public partial class Register : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-R86KLLA;Initial Catalog=esoft;Integrated Security=True");
        SqlCommand cmd;
        SqlDataReader dr;

        public Register()
        {
            InitializeComponent();
            comboBox1.TextChanged += comboBox1_TextChanged;
            textBox5.TextChanged += textBox5_TextChanged;
            textBox6.TextChanged += textBox6_TextChanged;
            textBox9.TextChanged += textBox9_TextChanged;

        }

        private void Register_Load(object sender, EventArgs e)
        {
            string sql = "SELECT reg_no FROM registration";
            SqlCommand cmd = new SqlCommand(sql, connection);
            connection.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["reg_no"]);
            }
            connection.Close();

            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            button1.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)

        {
            try
            {
                cmd = new SqlCommand("SELECT * FROM registration WHERE reg_no = @registerNo", connection);
                cmd.Parameters.AddWithValue("@registerNo", Convert.ToInt32(comboBox1.Text));
                connection.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string firstname = dr["first_name"] as string;
                    string lastname = dr["last_name"] as string;
                    DateTime dateOfBirth = Convert.ToDateTime(dr["date_of_birth"]);
                    string gender = dr["gender"] as string;
                    string address = dr["address"] as string;
                    string email = dr["email"] as string;
                    int mobile_phone = Convert.ToInt32(dr["mobile_phone"]);
                    int home_phone = Convert.ToInt32(dr["home_phone"]);
                    string parent_name = dr["parent_name"] as string;
                    string nic = dr["nic"] as string;
                    int parent_contact_number = Convert.ToInt32(dr["parent_contact_number"]);

                    textBox1.Text = firstname;
                    textBox2.Text = lastname;
                    dateTimePicker1.Value = dateOfBirth;
                    if (gender == "Male")
                    {
                        radioButton1.Checked = true;
                    }
                    else if (gender == "Female")
                    {
                        radioButton2.Checked = true;
                    }
                    textBox3.Text = address;
                    textBox4.Text = email;
                    if (mobile_phone == 0)
                    {
                        textBox5.Text = "";
                    }
                    else
                    {
                        textBox5.Text = mobile_phone.ToString();
                    }

                    if (home_phone == 0)
                    {
                        textBox6.Text = "";
                    }
                    else
                    {
                        textBox6.Text = home_phone.ToString();
                    }

                    textBox7.Text = parent_name;
                    textBox8.Text = nic;

                    if (parent_contact_number == 0)
                    {
                        textBox9.Text = "";
                    }
                    else
                    {
                        textBox9.Text = parent_contact_number.ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            finally
            {
                connection.Close();
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = false;
            }
            else if (!int.TryParse(comboBox1.Text, out int value))
            {
                MessageBox.Show("Please enter only whole numbers (integers) for the registration number.", "Invalid Input", MessageBoxButtons.OK,MessageBoxIcon.Error);
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled = true;
                button4.Enabled = false;
            }
            else
            {
                try
                {
                    cmd = new SqlCommand("SELECT COUNT(*) FROM registration WHERE reg_no = @registerNo", connection);
                    cmd.Parameters.AddWithValue("@registerNo", Convert.ToInt32(comboBox1.Text));
                    connection.Open();
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                    if (count > 0)
                    {
                        try
                        {
                            cmd = new SqlCommand("SELECT * FROM registration WHERE reg_no = @registerNo", connection);
                            cmd.Parameters.AddWithValue("@registerNo", Convert.ToInt32(comboBox1.Text));
                            connection.Open();
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                string firstname = dr["first_name"] as string;
                                string lastname = dr["last_name"] as string;
                                DateTime dateOfBirth = Convert.ToDateTime(dr["date_of_birth"]);
                                string gender = dr["gender"] as string;
                                string address = dr["address"] as string;
                                string email = dr["email"] as string;
                                int mobile_phone = Convert.ToInt32(dr["mobile_phone"]);
                                int home_phone = Convert.ToInt32(dr["home_phone"]);
                                string parent_name = dr["parent_name"] as string;
                                string nic = dr["nic"] as string;
                                int parent_contact_number = Convert.ToInt32(dr["parent_contact_number"]);

                                textBox1.Text = firstname;
                                textBox2.Text = lastname;
                                dateTimePicker1.Value = dateOfBirth;
                                if (gender == "Male")
                                {
                                    radioButton1.Checked = true;
                                }
                                else if (gender == "Female")
                                {
                                    radioButton2.Checked = true;
                                }
                                textBox3.Text = address;
                                textBox4.Text = email;
                                if (mobile_phone == 0)
                                {
                                    textBox5.Text = "";
                                }
                                else
                                {
                                    textBox5.Text = mobile_phone.ToString();
                                }

                                if (home_phone == 0)
                                {
                                    textBox6.Text = "";
                                }
                                else
                                {
                                    textBox6.Text = home_phone.ToString();
                                }

                                textBox7.Text = parent_name;
                                textBox8.Text = nic;

                                if (parent_contact_number == 0)
                                {
                                    textBox9.Text = "";
                                }
                                else
                                {
                                    textBox9.Text = parent_contact_number.ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"An error occurred: {ex.Message}");
                        }

                        finally
                        {
                            connection.Close();
                        }
                        button1.Enabled = false;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                    }
                    else
                    {
                        button1.Enabled = true;
                        button2.Enabled = false;
                        button3.Enabled = true;
                        button4.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox5.Text))
            {
                if (!int.TryParse(textBox5.Text, out int value))
                {
                    MessageBox.Show("Please enter only whole numbers (integers) for the mobile phone number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox5.Clear();
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox6.Text))
            {
                if (!int.TryParse(textBox6.Text, out int value))
                {
                    MessageBox.Show("Please enter only whole numbers (integers) for the home phone number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox6.Clear();
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox9.Text))
            {
                if (!int.TryParse(textBox9.Text, out int value))
                {
                    MessageBox.Show("Please enter only whole numbers (integers) for the parent contact number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox9.Clear();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("INSERT INTO registration VALUES(@registerNo, @firstname, @lastname, @birthofdate, @gender, @address, @email, @mobilephone, @homephone, @parentname, @nic, @parentcontactno)", connection);
                cmd.Parameters.AddWithValue("@registerNo", Convert.ToInt32(comboBox1.Text));
                cmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                cmd.Parameters.AddWithValue("@birthofdate", dateTimePicker1.Value);

                string gender = "";
                if (radioButton1.Checked)
                {
                    gender = "Male";
                }
                else if (radioButton2.Checked)
                {
                    gender = "Female";
                }
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@address", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);

                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    int mobilePhone = 0;
                    cmd.Parameters.AddWithValue("@mobilephone", mobilePhone);
                }
                else
                {
                    int mobilePhone = Convert.ToInt32(textBox5.Text);
                    cmd.Parameters.AddWithValue("@mobilephone", mobilePhone);
                }

                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    int homePhone = 0;
                    cmd.Parameters.AddWithValue("@homephone", homePhone);
                }
                else
                {
                    int homePhone = Convert.ToInt32(textBox6.Text);
                    cmd.Parameters.AddWithValue("@homephone", homePhone);
                }

                cmd.Parameters.AddWithValue("@parentname", textBox7.Text);
                cmd.Parameters.AddWithValue("@nic", textBox8.Text);

                if (string.IsNullOrEmpty(textBox9.Text))
                {
                    int parentcontactno = 0;
                    cmd.Parameters.AddWithValue("@parentcontactno", parentcontactno);
                }
                else
                {
                    int parentcontactno = Convert.ToInt32(textBox9.Text);
                    cmd.Parameters.AddWithValue("@parentcontactno", parentcontactno);
                }

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Register No '{comboBox1.Text}' records has been successfully added to the database.", "Register", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Register form = new Register();
                form.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            finally
            {
                connection.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("UPDATE registration SET first_name=@firstname, last_name=@lastname, date_of_birth=@birthofdate, gender=@gender, address=@address, email=@email, mobile_phone=@mobilephone, home_phone=@homephone, parent_name=@parentname, parent_contact_number=@parentcontactno, nic=@nic WHERE reg_no=@registerNo", connection);
                cmd.Parameters.AddWithValue("@registerNo", Convert.ToInt32(comboBox1.Text));
                cmd.Parameters.AddWithValue("@firstname", textBox1.Text);
                cmd.Parameters.AddWithValue("@lastname", textBox2.Text);
                cmd.Parameters.AddWithValue("@birthofdate", dateTimePicker1.Value);

                string gender = "";
                if (radioButton1.Checked)
                {
                    gender = "Male";
                }
                else if (radioButton2.Checked)
                {
                    gender = "Female";
                }
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@address", textBox3.Text);
                cmd.Parameters.AddWithValue("@email", textBox4.Text);

                if (string.IsNullOrEmpty(textBox5.Text))
                {
                    int mobilePhone = 0;
                    cmd.Parameters.AddWithValue("@mobilephone", mobilePhone);
                }
                else
                {
                    int mobilePhone = Convert.ToInt32(textBox5.Text);
                    cmd.Parameters.AddWithValue("@mobilephone", mobilePhone);
                }

                if (string.IsNullOrEmpty(textBox6.Text))
                {
                    int homePhone = 0;
                    cmd.Parameters.AddWithValue("@homephone", homePhone);
                }
                else
                {
                    int homePhone = Convert.ToInt32(textBox6.Text);
                    cmd.Parameters.AddWithValue("@homephone", homePhone);
                }

                cmd.Parameters.AddWithValue("@parentname", textBox7.Text);
                cmd.Parameters.AddWithValue("@nic", textBox8.Text);

                if (string.IsNullOrEmpty(textBox9.Text))
                {
                    int parentcontactno = 0;
                    cmd.Parameters.AddWithValue("@parentcontactno", parentcontactno);
                }
                else
                {
                    int parentcontactno = Convert.ToInt32(textBox9.Text);
                    cmd.Parameters.AddWithValue("@parentcontactno", parentcontactno);
                }

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Register No '{comboBox1.Text}' records has been successfully updated in the database.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Register form = new Register();
                form.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

            finally
            {
                connection.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, Do you realty want to Delete this Record...?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    cmd = new SqlCommand("DELETE FROM registration WHERE reg_no=@registerNo", connection);
                    cmd.Parameters.AddWithValue("@registerNo", Convert.ToInt32(comboBox1.Text));
                    connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Register No '{comboBox1.Text}' records has been successfully deleted from the database.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Register form = new Register();
                    form.Show();
                    this.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }

                finally
                {
                    connection.Close();
                }
            }
        }

        private void ClearFormInputs()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            dateTimePicker1.Value = DateTime.Now;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            comboBox1.Text = "";
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = false;
            button1.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ClearFormInputs();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, Do you really want to Logout...?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login form = new Login();
                form.Show();
                this.Close();
            }
        }

        private void label15_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure, Do you really want to Exit...?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
