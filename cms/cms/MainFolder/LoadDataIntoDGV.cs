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
    public partial class LoadDataIntoDGV : Template
    {
        public LoadDataIntoDGV()
        {
            InitializeComponent();
            LoadDataIntoDataGridView();
            
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadDataIntoDataGridView()
        {
            using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("usp_LoadDataIntoDataGridView", con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    if (con.State != ConnectionState.Open)
                    {
                        con.Open();
                    }

                    DataTable dta = new DataTable();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    dta.Load(sdr);
                    dataGridView1.DataSource = dta;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                using (SqlConnection con = new SqlConnection(AppConnection.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Role_SearchINfor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@RoleTitle",textBox1.Text.Trim());

                        if(con.State!=ConnectionState.Open)
                        {
                            con.Open();
                        }


                        DataTable dta= new DataTable();
                        SqlDataReader sdr = cmd.ExecuteReader();


                        if(sdr.HasRows)
                        {
                            dta.Load(sdr);
                            dataGridView1.DataSource = dta;
                        }
                        else
                        {
                            MessageBox.Show("Rows not found","Validation error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                            textBox1.Clear();
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Empy record", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void saveInformationToolStripMenuItem_Click(object sender, EventArgs e)                 // connect to anothe file
        {
            AddRoles ar = new AddRoles();
            ar.ShowDialog();
            LoadDataIntoDataGridView();
        }

        private void mtDataBase_Click(object sender, EventArgs e)                   // load the data after the search operation is completed
        {
            if(textBox1.Text !=string.Empty)
            {
                LoadDataIntoDataGridView();
                textBox1.Clear();
            }
        
            
        }

        private void LoadDataIntoDGV_Load(object sender, EventArgs e)
        {

        }
    }
}
