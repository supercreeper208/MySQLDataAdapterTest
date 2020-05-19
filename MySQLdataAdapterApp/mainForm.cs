using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Runtime.InteropServices.WindowsRuntime;
using System.CodeDom.Compiler;

namespace MySQLdataAdapterApp
{
    public partial class mainForm : Form
    {
        //objecten van belang bij het maken van een connectie met de database
        MySqlConnection myConnection;
        MySqlDataAdapter myDataAdapter;
        MySqlCommandBuilder myCommandBuidler;
        DataTable myTable;
        string connectionString;
        string selectQuery = "SELECT * FROM producten";

        invulForm invulForm = new invulForm();
        public mainForm()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
            invulForm.Hide();
            invulForm.wijzegingenOplsaan += InvulFormOnWijzigingenOpslaan;
            invulForm.nieuwRecordOplsaan += InvulFormOnNieuwRecordOpslaan;
        }

        private void InvulFormOnNieuwRecordOpslaan(object sender, List<string> e)
        {
            DataRow nieweRij = myTable.NewRow();
            nieweRij[0] = DBNull.Value;
            nieweRij[1] = e[0];
            nieweRij[2] = e[1];
            nieweRij[3] = e[2];
            myTable.Rows.Add(nieweRij);
            invulForm.Hide();
        }
        private void InvulFormOnWijzigingenOpslaan(object sender, List<string> e)
        {
            int rij = Int32.Parse(e[3]);
            PasDataTabelAan(myTable, rij, 1, e[0]);
            PasDataTabelAan(myTable, rij, 2, e[1]);
            PasDataTabelAan(myTable, rij, 3, e[2]);
            invulForm.Hide();
        }

        private void updateDataTable()
        {
            using (myDataAdapter = new MySqlDataAdapter(selectQuery, myConnection))
            {
                DataSet dataset = new DataSet();
                myCommandBuidler = new MySqlCommandBuilder(myDataAdapter);
                myTable.Clear();
                myDataAdapter.Fill(myTable);
            }
        }

        private bool IsDatabaseAllive()
        {
            try
            {
                myConnection.Open();
                myConnection.Close();
            }
            catch (MySqlException)
            {
                return false;
            }
            return true;

        }

        private void BtnExecuteSelectQuery_Click(object sender, EventArgs e)
        {
            myConnection = new MySqlConnection(connectionString);
            if(!IsDatabaseAllive())
            {
                MessageBox.Show("Database Error!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Console.WriteLine(myConnection.State);
            myTable = new DataTable();
            DgvProducten.DataSource = myTable;
            using (myDataAdapter = new MySqlDataAdapter(selectQuery, myConnection))
            {
                myCommandBuidler = new MySqlCommandBuilder(myDataAdapter);
                myDataAdapter.Fill(myTable);
                
            }
            DgvProducten.ClearSelection();
            BtnRecordVerwijderen.Enabled = false;
            BtnRecordToevoegen.Enabled = true;
            BtnRecordWijzigen.Enabled = true;
        }

        private void ExecuteMySqlCommand(MySqlConnection connection ,string command)
        {
            using (MySqlCommand tempComm = new MySqlCommand())
            {
                tempComm.Connection = connection;
                connection.Open();
                tempComm.CommandText = "set foreign_key_checks = 0;";
                tempComm.CommandType = CommandType.Text;
                tempComm.ExecuteNonQuery();
                connection.Close();
            }
        }

        private void BtnUpdateTabel_Click(object sender, EventArgs e)
        {  
            DataTable myChanges = myTable.GetChanges();
            if (myChanges != null)
            {
                using (myDataAdapter = new MySqlDataAdapter(selectQuery, myConnection))
                {
                    ExecuteMySqlCommand(myConnection, "set foreign_key_checks = 0;");
                    myCommandBuidler = new MySqlCommandBuilder(myDataAdapter);
                    myDataAdapter.Update(myChanges);
                    myTable.AcceptChanges();
                    updateDataTable();
                    ExecuteMySqlCommand(myConnection, "set foreign_key_checks = 0;");
                }
            }
            else
            {
                MessageBox.Show("Er zijn geen veranderingen!"); ;
            }
        }

        private void PasDataGridVieuwAan(DataGridView grid, int rij, int kol, string data)
        {
            if(rij < grid.Rows.Count && kol < grid.Columns.Count)
            {
                grid.Rows[rij].Cells[kol].Value = data;
            }
        }

        private void PasDataTabelAan(DataTable tabel, int rij, int kol, string data)
        {
            if (rij < tabel.Rows.Count && kol < tabel.Columns.Count)
            {
                tabel.Rows[rij][kol] = data;
            }
        }

        private void DgvProducten_DoubleClick(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection geselecteerdeRijen = DgvProducten.SelectedRows;

            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewRow r in geselecteerdeRijen)
            {
                sb.Append(r.Index.ToString());
            }

            MessageBox.Show("rij "+sb.ToString()+ " geselecteerd...");

            /*PasDataGridVieuwAan(DgvProducten, 0, 1, "manneken pis");
            Console.WriteLine(myTable.Rows[0][1]);
            Console.WriteLine(DgvProducten.Rows[0].Cells[1].Value);
            */
        }

        private void verwijderRecordDataTable(DataGridView grid, int rij)
        {
            if(rij < grid.Rows.Count)
            {
                grid.Rows.RemoveAt(rij);
            }
        }

        private void BtnRecordVerwijderen_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Ben je zeker dat je " +DgvProducten.SelectedRows[0].Cells[1].Value +
                " wilt verwijderen?","Zeker delete?", MessageBoxButtons.YesNo) 
                == DialogResult.Yes)
            {
                verwijderRecordDataTable(DgvProducten, DgvProducten.SelectedRows[0].Index);
            }
        }

        private void BtnRecordWijzigen_Click(object sender, EventArgs e)
        {
            DataGridViewRow temp = DgvProducten.SelectedRows[0];
            invulForm.recordAanpassen(temp.Index, temp.Cells[1].Value.ToString(), temp.Cells[2].Value.ToString()
                , temp.Cells[3].Value.ToString());
            invulForm.Show();
            invulForm.BringToFront();
        }

        private void BtnRecordToevoegen_Click(object sender, EventArgs e)
        {
            invulForm.recordToevoegen();
            invulForm.Show();
            invulForm.BringToFront();
        }

        private void DgvProducten_SelectionChanged(object sender, EventArgs e)
        {
            BtnRecordVerwijderen.Enabled = true;
        }
    }
}
