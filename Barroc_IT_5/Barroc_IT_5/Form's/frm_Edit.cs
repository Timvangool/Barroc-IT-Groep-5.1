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
        public TextBox[] tb;
        public Label[] lb;
        public DateTimePicker[] dtp;
        public CheckBox[] cb;
        public ComboBox[] combo;
        public int permissions;
        public SqlDataReader reader;
        public string table;

        public frm_Edit()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
        }

        public frm_Edit(int permissions)
        {
            this.permissions = permissions;
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
            Form frm_Main = new frm_Main(permissions);
            frm_Main.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Main);
            this.Close();
        }

        private void frm_Edit_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            cmd = new SqlCommand("SELECT ID,Name FROM " + table + "", dbh.getCon());
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

            reader.Dispose();
        }

        public void cb_Customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbh.closeCon();
            string ID = cb_Customers.SelectedValue.ToString();

            try
            {
                for (int i = 1; i < tb.Count(); i++)
                {
                    this.Controls.Remove(tb[i]);
                    tb[i].Dispose();
                    this.Controls.Remove(lb[i]);
                    lb[i].Dispose();
                    this.Controls.Remove(dtp[i]);
                    dtp[i].Dispose();
                }
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
                dbh.closeCon();
            }

            int amount = 101010;
            tb = new TextBox[amount];
            lb = new Label[amount];
            dtp = new DateTimePicker[amount];
            cb = new CheckBox[amount];
            combo = new ComboBox[amount];
            
            int x = 150;
            int y = 100;
            int xx = x - 95;

            string[] temp = new string[12032130];
            temp = getColumnsName();

            for (int i = 1; i < temp.Length; i++)
            {
                tb[i] = new TextBox();
                tb[i].Name = "tb_" + temp[i].ToString();
                tb[i].Size = new System.Drawing.Size(130, 21);
                tb[i].Location = new Point(x, y);

                dtp[i] = new DateTimePicker();
                dtp[i].Name = "dtp_" + temp[i].ToString();
                dtp[i].Size = new System.Drawing.Size(130, 21);
                dtp[i].Location = new Point(x, y);

                cb[i] = new CheckBox();
                cb[i].Name = "cb_" + temp[i].ToString();
                cb[i].Location = new Point(x, y);

                lb[i] = new Label();
                lb[i].Name = "lb_" + temp[i].ToString();
                lb[i].Location = new Point(xx, y);
                lb[i].Text = "" + temp[i];

                y += 50;

                if (y >= 450)
                {
                    y = 100;
                    x += 250;
                    xx += 250;
                }

                if (lb[i].Text == "Maintenance_Contract")
                {
                    lb[i].Text = @"Maintenance
Contract";
                    lb[i].Size = new System.Drawing.Size(100, 30);
                }
                if(tb[i].Name == "tb_date")
                {
                    this.Controls.Add(dtp[i]);
                }
                else if (tb[i].Name == "tb_potential_prospect" || tb[i].Name == "tb_is_paid" || tb[i].Name == "tb_invoice_sent" || tb[i].Name == "tb_is_done" || tb[i].Name == "tb_BKR" || tb[i].Name == "tb_creditworthy")
                {
                    this.Controls.Add(cb[i]);
                }
                else if (tb[i].Name == "tb_ID_project" || tb[i].Name == "tb_ID_customer" )
                {
                    this.Controls.Add(combo[i]);
                }
                else
                {
                    this.Controls.Add(tb[i]);
                }
                this.Controls.Add(lb[i]);
            }
            
            string query = "SELECT * FROM " + table + " WHERE ID=" + ID;
            SqlCommand cmd = new SqlCommand(query, dbh.getCon());
            SqlDataReader reader;

            switch(table)
            {
                #region Appointments
                case "tbl_Appointments":
                    try 
                    {
                        dbh.openCon();
                        reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {

                            string description = CheckForNullsString(reader, 1);
                            DateTime date = reader.GetDateTime(2);
                            string next_action = CheckForNullsString(reader, 3);
                            string ID_project = CheckForNullsInt(reader, 4);
                            string name = CheckForNullsString(reader, 5);

                            tb[1].Text = description;
                            dtp[2].Format = DateTimePickerFormat.Custom;
                            //dtp[2].CustomFormat = "MM-dd-yyyy 'at' HH:mm";
                            dtp[2].CustomFormat = "yyyy-MM-dd";
                            dtp[2].Text = Convert.ToString(date);
                            tb[3].Text = next_action;
                            tb[4].Text = ID_project;
                            tb[5].Text = name;
                            }
                    }

                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    break;
                #endregion
                #region Customers
                case "tbl_Customers":
                    try
                    {
                        dbh.openCon();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            string name = CheckForNullsString(reader, 1);
                            string address = CheckForNullsString(reader, 2);
                            string address2 = CheckForNullsString(reader, 3);
                            string housenr = CheckForNullsString(reader, 4);
                            string housenr2 = CheckForNullsString(reader, 5);
                            string zipcode = CheckForNullsString(reader, 6);
                            string zipcode2 = CheckForNullsString(reader, 7);
                            string place = CheckForNullsString(reader, 8);
                            string place2 = CheckForNullsString(reader, 9);
                            string country = CheckForNullsString(reader, 10);
                            string country2 = CheckForNullsString(reader, 11);
                            string phone = CheckForNullsString(reader, 12);
                            string fax = CheckForNullsString(reader, 13);
                            string email = CheckForNullsString(reader, 14);
                            bool potential_prospect = reader.GetBoolean(15);

                            tb[1].Text = name;
                            tb[2].Text = address;
                            tb[3].Text = address2;
                            tb[4].Text = housenr;
                            tb[5].Text = housenr2;
                            tb[6].Text = zipcode;
                            tb[7].Text = zipcode2;
                            tb[8].Text = place;
                            tb[9].Text = place2;
                            tb[10].Text = country;
                            tb[11].Text = country2;
                            tb[12].Text = phone;
                            tb[13].Text = fax;
                            tb[14].Text = email;
                            cb[15].Checked = potential_prospect;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                #endregion
                #region Invoices
                case "tbl_Invoices":
                    try
                    {
                        dbh.openCon();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {


                            string bank_acc_number = CheckForNullsString(reader, 1);
                            string price = CheckForNullsInt(reader, 2);
                            string gross_rev = CheckForNullsInt(reader, 3);
                            string ledger_acc_nr = CheckForNullsString(reader, 4);
                            string tax_code = CheckForNullsString(reader, 5);
                            bool is_paid = reader.GetBoolean(6);
                            DateTime date = reader.GetDateTime(7);
                            bool invoice_sent = reader.GetBoolean(8);
                            string ID_project = CheckForNullsInt(reader, 9);
                            string name = CheckForNullsString(reader, 10);

                            tb[1].Text = bank_acc_number;
                            tb[2].Text = price;
                            tb[3].Text = gross_rev;
                            tb[4].Text = ledger_acc_nr;
                            tb[5].Text = tax_code;
                            cb[6].Checked = is_paid;
                            dtp[7].Format = DateTimePickerFormat.Custom;
                            dtp[7].CustomFormat = "MM-dd-yyy 'at' HH:mm";
                            dtp[7].Text = Convert.ToString(date);
                            cb[8].Checked = invoice_sent;
                            tb[9].Text = ID_project;
                            tb[10].Text = name;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                #endregion
                #region Projects
                case "tbl_Projects":
                    try
                    {
                        dbh.openCon();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {


                            string name = CheckForNullsString(reader, 1);
                            string hardware = CheckForNullsString(reader, 2);
                            string operating_system = CheckForNullsString(reader, 3);
                            string maintenance_contract = CheckForNullsString(reader, 4);
                            string applications = CheckForNullsString(reader, 5);
                            string limit = CheckForNullsInt(reader, 6);
                            bool is_done = reader.GetBoolean(7);
                            string nr_invoices = CheckForNullsInt(reader, 8);
                            bool BKR = reader.GetBoolean(9);
                            bool creditworthy = reader.GetBoolean(10);
                            string ID_customer = CheckForNullsInt(reader, 11);

                            tb[1].Text = name;
                            tb[2].Text = hardware;
                            tb[3].Text = operating_system;
                            tb[4].Text = maintenance_contract;
                            tb[5].Text = applications;
                            tb[6].Text = limit;
                            cb[7].Checked = is_done;
                            tb[8].Text = nr_invoices;
                            cb[9].Checked = BKR;
                            cb[10].Checked = creditworthy;
                            SetComboBox();
                            combo[11].Text = SetComboText(ID_customer);
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                #endregion
            }
            dbh.closeCon();
        }

        private void SetComboBox()
        {
            cmd = new SqlCommand("SELECT ID,Name FROM "+ GetFKTable() + "", dbh.getCon());
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

            reader.Dispose();
        }

        private string GetFKTable()
        {
            switch (table)
            {
                case "tbl_Appointments":
                    return "tbl_Projects";
                case "tbl_Projects":
                    return "tbl_Customers";
                case "tbl_Invoices":
                    return "tbl_Projects";
                default:
                    return "";
            }
        }

        private string SetComboText(string ID_customer)
        {
            cmd = new SqlCommand("SELECT Name FROM " + GetFKTable() + " WHERE ID=" + ID_customer +"", dbh.getCon());
            SqlDataReader reader;

            dbh.openCon();
            reader = cmd.ExecuteReader();
            dbh.closeCon();
            return reader.GetString(0);
        }

        private void cb_Customers_MouseUp(object sender, MouseEventArgs e)
        {
            cb_Customers.DroppedDown = true;
        }

        private void cb_Customers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }


        private string[] getColumnsName()
        {
            List<string> listOfColumns = new List<string>();
            using (SqlCommand command = dbh.getCon().CreateCommand())
            {
                command.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = '" + table + "' and t.type = 'U'";
                dbh.openCon();
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfColumns.Add(reader.GetString(0));
                    }
                }
                dbh.closeCon();
            }
            reader.Dispose();

            return listOfColumns.ToArray();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string insertQuery;
            string ID = cb_Customers.SelectedValue.ToString();
            

            switch (table)
            {
                #region Appointments
                case "tbl_Appointments":
                    TextBox tb1 = Application.OpenForms["frm_Edit"].Controls["tb_description"] as TextBox;
                    DateTimePicker dtp1 = Application.OpenForms["frm_Edit"].Controls["dtp_date"] as DateTimePicker;
                    TextBox tb2 = Application.OpenForms["frm_Edit"].Controls["tb_next_action"] as TextBox;
                    TextBox tb3 = Application.OpenForms["frm_Edit"].Controls["tb_name"] as TextBox;
                    TextBox tb4 = Application.OpenForms["frm_Edit"].Controls["tb_Id_project"] as TextBox;

                    int convertedID = Convert.ToInt32(tb4.Text);
                    convertedID = int.Parse(tb4.Text);

                    dbh.openCon();

                    insertQuery = "UPDATE " + table + " SET description='" + tb1.Text + "', date='" + dtp1.Text + "', next_action='" + tb2.Text + "', ID_project='" + convertedID + "', name='" + tb3.Text + "' WHERE ID=" + ID;
                    SqlCommand cmd = new SqlCommand(insertQuery, dbh.getCon());
                    cmd.ExecuteNonQuery();

                    dbh.closeCon();
                    
                    MessageBox.Show("Save succesful.");
                    break;
                #endregion
                case "tbl_Customers":

                    break;
                case "tbl_Invoices":
                    break;
                case "tbl_Projects":
                    break;
            }
        }

        private string CheckForNullsInt(SqlDataReader reader, int i)
        {
            if (reader.IsDBNull(i))
            {
                return "";
            }
            else
            {
                return reader.GetInt32(i).ToString();
            }
        }

        private string CheckForNullsString(SqlDataReader reader, int i)
        {
            if (reader.IsDBNull(i))
            {
                return "";             
            }
            else
            {
                return reader.GetString(i);
            }
        }
    }
}
