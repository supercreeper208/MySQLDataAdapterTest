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

namespace MySQLdataAdapterApp
{
    public partial class Form1 : Form
    {
        //objecten van belang bij het maken van een connectie met de database
        MySqlConnection myConnection;
        MySqlDataAdapter myDataAdapter;
        MySqlCommandBuilder myCommandBuidler;
        DataTable myTable;
        string connectionString;
        string selectQuery = "SELECT productNaam, productStock, beschikbaar FROM producten";


        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myConnection = new MySqlConnection(connectionString);
            myDataAdapter = new MySqlDataAdapter(selectQuery, myConnection);
            myCommandBuidler = new MySqlCommandBuilder(myDataAdapter);
            myTable = new DataTable();
            myDataAdapter.Fill(myTable);
            DgvProducten.DataSource = myTable;
        }

        private void BtnUpdateTabel_Click(object sender, EventArgs e)
        {
            DataTable myChanges = myTable.GetChanges();
            myDataAdapter.Update(myChanges);
            myTable.AcceptChanges();
        }
    }
}
