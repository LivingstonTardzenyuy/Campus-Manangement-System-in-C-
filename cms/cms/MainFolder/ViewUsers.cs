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
    public partial class ViewUsers : Template
    {
        public ViewUsers()
        {
            InitializeComponent();
            LoadDataIntoDGV();
        }

        private void ViewUsers_Load(object sender, EventArgs e)
        {

        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UsersTable ust = new UsersTable();                                               //creating an intance of an object
            ust.ShowDialog();
           

        }

        private void LoadDataIntoDGV()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))

            {
                using (SqlCommand cmd = new SqlCommand("usp_User_LoadDataIntoDataGridView", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if(con.State!=ConnectionState.Open)
                    {
                        con.Open();
                    }

                    DataTable dta = new DataTable();
                    SqlDataReader dtr = cmd.ExecuteReader();
                    dta.Load(dtr);
                    dataGridView.DataSource = dta;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text!=string.Empty)
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))

                {
                    using (SqlCommand cmd = new SqlCommand("usp_UsersTb_SearchDataFromDGV", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserName", txtSearchMenu.Text.Trim());
                        if(con.State!=ConnectionState.Open)
                        {
                            con.Open();
                        }
                        DataTable dta = new DataTable();
                        SqlDataReader sdr = cmd.ExecuteReader();

                        if(sdr.HasRows)
                        {
                            dta.Load(sdr);
                            dataGridView.DataSource = dta;
                        }
                        else
                        {
                            MessageBox.Show("Not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtSearchMenu.Clear();
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDataIntoDGV();
            txtSearchMenu.Clear();
        }

        private void dataGridView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if(dataGridView.Rows.Count>0)                               //checking if DGV contains data
            {
                UsersTable ut=new UsersTable();                          // creating an object of the userstable
                ut.username = username;                                   // since our searach operation is based on useranme
                ut.isUpdate = true;
                ut.ShowDialog();                                                    // show a form for data to be inputed
                LoadDataIntoDGV();                                                  //store the datain DGV
            }
        }

        public string username { get; set; }
    }
}
