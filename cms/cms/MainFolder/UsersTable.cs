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
    public partial class UsersTable : Template
    {
        public UsersTable()
        {
            InitializeComponent();
        }

        private void UsersTable_Load(object sender, EventArgs e)
        {
            LoadDataIntoComboBox();
        }
        //property to handle update and delete operations
        public string username{get;set;}
        public bool isUpdate { get; set; }

        private void LoadDataIntoComboBox()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_Users_LoadDataIntoComboBoxs", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    DataTable dta = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dta.Load(sdr);
                    comboBoxs.DataSource = dta;
                    comboBoxs.DisplayMember = "RoleTitle";                                  //forign table
                    comboBoxs.ValueMember = "RoleID";

                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveInformationToolStripMenuItem_Click(object sender, EventArgs e)         //method to store information into our databasee
        { 
            if(isFormValid()) 
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_LoadDataIntoUsersTb", con))
                    
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@UserName",txtUserName.Text.Trim());
                        cmd.Parameters.AddWithValue("@Password",EncryptPassword.GetPassword(txtPassword.Text.Trim())); //  GetPassword
                        cmd.Parameters.AddWithValue("@RoleID", comboBoxs.SelectedValue);                        //we are refeerencing it to the roles table
                        cmd.Parameters.AddWithValue("@isActive","True");
                        cmd.Parameters.AddWithValue("@CreatedBy", "Admin");
                        
                        if(con.State!=ConnectionState.Open)
                        {
                            con.Open();
                        }

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successful", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearForm();
                    }

                }
            }
        }

        private void ClearForm()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtUserName.Focus();
            comboBoxs.SelectedIndex = 0;                        //to select the first entry
        }

        private bool isFormValid()
        {
            if(txtPassword.Text.Trim()==string.Empty)
            {
                MessageBox.Show("Input Password", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                return false;
            }

            if(comboBoxs.SelectedIndex==-1)                                             //to chechh if user has inputed data in the combo box
            {
                MessageBox.Show("Select data from dropdown", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if(txtPassword.Text.Length>=50)
            {
                MessageBox.Show("Password out of range", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUserName.Focus();
                txtPassword.Clear();
                return false;
            }
            else
            {
                return true;
            }
        }


        public object SecureData { get; set; }

     
    }
}
