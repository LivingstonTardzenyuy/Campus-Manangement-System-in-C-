using cms.ConFolder;
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

namespace cms.MainFolder
{
    public partial class LoginForm : Template
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void picLogo_Click(object sender, EventArgs e)
        {

        }

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)                 // using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
        {

            if(isFormValid())
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))   
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Users_LoginIntoApp", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserName",txtUserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password", EncryptPassword.GetPassword(txtPassword.Text.Trim()));

                        if(con.State!=ConnectionState.Open)
                        {
                            con.Open();
                        }

                        DataTable dta = new DataTable();
                        SqlDataReader sdr = cmd.ExecuteReader();


                        dta.Load(sdr);
                        MessageBox.Show("Successful","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        
                    }
                }
            }
        }

        private bool isFormValid()
        {
            if(txtUserName.Text.Trim()==string.Empty)
            {
                MessageBox.Show("Missing Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Clear();
                txtUserName.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Missing Field", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Clear();
                return false;
               // txtUserName.Focus();
            }
            else
            {
                return true;
            }
        }

    }
}
