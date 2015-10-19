using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Barroc_IT_Groep5;
using System.Data.SqlClient;

namespace Barroc_IT_5
{
    public partial class frm_Edit : Form
    {
        SQLDatabaseHandler dbh;
        public SqlCommand cmd;
        public int permissions;
        public string table;

        public frm_Edit()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
        }

        public frm_Edit(int permissions, string table)
        {
            this.permissions = permissions;
            this.table = table;
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Form frm_Main = new frm_Main();
            frm_Main.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Main);
            this.Close();
        }

        private void frm_Edit_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            cmd = new SqlCommand("SELECT ID,Name FROM "+table+"", dbh.getCon());
            SqlDataReader reader;

            dbh.openCon();
            reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Load(reader);

            cb_Customers.ValueMember = "ID";
            cb_Customers.DisplayMember = "Name";
            cb_Customers.DataSource = dt;

            dbh.closeCon();
        }

        private void cb_Customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ID = cb_Customers.SelectedValue.ToString();
        }

        private void cb_Customers_MouseUp(object sender, MouseEventArgs e)
        {
            cb_Customers.DroppedDown = true;
        }

        private void cb_Customers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
