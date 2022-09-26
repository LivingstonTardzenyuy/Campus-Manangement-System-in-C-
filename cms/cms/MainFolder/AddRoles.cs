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
    public partial class AddRoles : Template
    {
        public AddRoles()
        {
            InitializeComponent();
        //    FocusAndFalseMethodInSaveInfo();
        }


        // all the methods are writting here to reduce the complexity of the code
        //FocusAndFalseMethodInSaveInfo()
        //{

          //  txtRoleTitle.Focus();
            //return false;
        //}

        private void clearAll()
        {
            textBox1.Clear();
            textBox2.Clear();
        }
        private void AddRoles_Load(object sender, EventArgs e)
        {
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void saveInformationToolStripMenuItem_Click(object sender, EventArgs e)                     // method to help us store info
        {
            if(isFormValid())
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Role_InsertINtoDB", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RoleTitle", textBox1.Text.Trim());
                        cmd.Parameters.AddWithValue("@Discription", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@CreatedBy", "Admin");

                        if (con.State != ConnectionState.Open)
                        {
                            con.Open();
                        }
                        
                        MessageBox.Show("Succesfully inserted","Success",MessageBoxButtons.OK ,MessageBoxIcon.Information);
                        cmd.ExecuteNonQuery();
                        
                    }
                }
            }
        }

        public bool isFormValid()
        {
            if(textBox1.Text.Trim()==string.Empty)
            {
                MessageBox.Show("Input fill is empty", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              //  FocusAndFalseMethodInSaveInfo();
                textBox1.Focus();
                clearAll();
                return false;
            }

            if(textBox2.Text.Length >=50)
            {
                MessageBox.Show("More data inputed", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //  FocusAndFalseMethodInSaveInfo();
                textBox1.Focus();
                clearAll();
                return false;
            }
            return true;

           
        }
    }
}
