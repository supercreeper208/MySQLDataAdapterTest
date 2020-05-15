using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySQLdataAdapterApp
{

    public partial class invulForm : Form
    {
        public event EventHandler<List<String>> wijzegingenOplsaan;
        public event EventHandler<List<string>> nieuwRecordOplsaan;

        private int mode = -1;

        public invulForm()
        {
            InitializeComponent();
        }

        protected virtual void OnWijzegingenOplsaan(List<string> e)
        {
            wijzegingenOplsaan?.Invoke(this, e);
        }

        protected virtual void OnNieuwRecordOplsaan(List<string> e)
        {
            nieuwRecordOplsaan?.Invoke(this, e);
        }


        public void recordAanpassen(int row, string productNaam, string stock, string beschikbaarheid)
        {
            label4.Text = "Record aanpassen";
            this.Text = "MySQL Databasebeheer - record aanpassen";

            mode = row;
            textBox1.Text = productNaam;
            textBox2.Text = stock;
            if(beschikbaarheid == "1")
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        public void recordToevoegen()
        {
            label4.Text = "Record toevoegen";
            this.Text = "MySQL Databasebeheer - record toevoegen";

            mode = -1;
            textBox1.Text = "";
            textBox2.Text = "";
            checkBox1.Checked = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            int val;
            if (Int32.TryParse(textBox2.Text, out val))
            {
                List<string> temp = new List<string>();
                temp.Add(textBox1.Text);
                temp.Add(textBox2.Text);
                if (checkBox1.Checked)
                {
                    temp.Add("1");
                }
                else
                {
                    temp.Add("0");
                }
                if (mode != -1)
                {
                    temp.Add(mode.ToString());
                    OnWijzegingenOplsaan(temp);
                }
                else
                {
                    OnNieuwRecordOplsaan(temp);
                }
            }
            else
            {
                MessageBox.Show("Dit is geen geldig getal" + textBox2.Text);
            }
        }

        private void invulForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }



}
