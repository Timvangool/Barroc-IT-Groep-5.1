using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Barroc_IT_Groep5;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace Barroc_IT_5
{
    public partial class frm_Edit : Form
    {
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

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
        public int? id;

        //Default constructor
        public frm_Edit()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
        }

        //Custom constructor
        public frm_Edit(int permissions, string table)
        {
            this.permissions = permissions;
            this.table = table;
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
        }

        //Custom contructor
        public frm_Edit(int permissions, string table, int id)
        {
            this.permissions = permissions;
            this.table = table;
            this.id = id;
            
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

        //Fills combobox with IDs and Names from the table defined in the custom constructor.
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

            if (id != null)
            {
                cb_Customers.SelectedValue = id;
            }
            
            reader.Dispose();
        }

        //This creates the texboxes, comboboxes, checkboxes and labels filled with data from the selected record in the combobox.
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
                    this.Controls.Remove(cb[i]);
                    cb[i].Dispose();
                    this.Controls.Remove(combo[i]);
                    combo[i].Dispose();
                }
            }
            catch(Exception)
            {
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

                combo[i] = new ComboBox();
                combo[i].Name = "combo_" + temp[i].ToString();
                combo[i].Size = new System.Drawing.Size(130, 21);
                combo[i].Location = new Point(x, y);

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

                if (lb[i].Text == "maintenance_contract")
                {
                    lb[i].Text = @"Maintenance
Contract";
                    lb[i].Size = new System.Drawing.Size(100, 30);
                }
                if(tb[i].Name == "tb_date")
                {
                    this.Controls.Add(dtp[i]);
                }
                else if (tb[i].Name == "tb_potential_prospect" || tb[i].Name == "tb_is_paid" || tb[i].Name == "tb_invoice_sent" || tb[i].Name == "tb_is_done" || tb[i].Name == "tb_BKR" || tb[i].Name == "tb_creditworthy" || tb[i].Name == "tb_maintenance_contract")
                {
                    this.Controls.Add(cb[i]);
                }
                else if (tb[i].Name == "tb_Id_project" || tb[i].Name == "tb_Id_customer" )
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

                        string description = null, next_action = null, ID_project = null, name = null;
                        DateTime date = DateTime.Now;

                        while (reader.Read())
                        {
                            description = CheckForNullsString(reader, 1);
                            date = reader.GetDateTime(2);
                            next_action = CheckForNullsString(reader, 3);
                            ID_project = CheckForNullsInt(reader, 4);
                            name = CheckForNullsString(reader, 5);
                        }
                        reader.Dispose();
                        dbh.closeCon();

                        tb[1].Text = description;
                        dtp[2].Format = DateTimePickerFormat.Custom;
                        dtp[2].CustomFormat = "yyyy-MM-dd";
                        dtp[2].Text = Convert.ToString(date);
                        tb[3].Text = next_action;
                        SetComboBox(combo[4]);
                        if (ID_project == "")
                        {
                            combo[4].Text = ID_project;
                        }
                        else
                        {
                            combo[4].Text = SetComboText(ID_project);
                        }
                        tb[5].Text = name;

                        dbh.closeCon();
                    }
                    catch (Exception ex)
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

                        string name = null, address = null, address2 = null, housenr = null, housenr2 = null, zipcode = null, zipcode2 = null, place = null, place2 = null, country = null, country2 = null, phone = null, fax = null, email = null;
                        bool potential_prospect = false;

                        while (reader.Read())
                        {
                            name = CheckForNullsString(reader, 1);
                            address = CheckForNullsString(reader, 2);
                            address2 = CheckForNullsString(reader, 3);
                            housenr = CheckForNullsString(reader, 4);
                            housenr2 = CheckForNullsString(reader, 5);
                            zipcode = CheckForNullsString(reader, 6);
                            zipcode2 = CheckForNullsString(reader, 7);
                            place = CheckForNullsString(reader, 8);
                            place2 = CheckForNullsString(reader, 9);
                            country = CheckForNullsString(reader, 10);
                            country2 = CheckForNullsString(reader, 11);
                            phone = CheckForNullsString(reader, 12);
                            fax = CheckForNullsString(reader, 13);
                            email = CheckForNullsString(reader, 14);
                            potential_prospect = reader.GetBoolean(reader.GetOrdinal("potential_prospect"));
                        }
                        reader.Dispose();
                        dbh.closeCon();

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

                        dbh.closeCon();
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

                        string amount2 = null, bank_acc_number = null, gross_rev = null, ledger_acc_nr = null, tax_code = null, ID_project = null, name = null;
                        DateTime date = DateTime.Now;
                        bool is_paid = false, invoice_sent = false;

                        while (reader.Read())
                        {
                            amount2 = Math.Round(reader.GetDecimal(1), 2).ToString();
                            bank_acc_number = CheckForNullsString(reader, 2);
                            gross_rev = Math.Round(reader.GetDecimal(3), 2).ToString();
                            ledger_acc_nr = CheckForNullsString(reader, 4);
                            tax_code = CheckForNullsString(reader, 5);
                            is_paid = reader.GetBoolean(reader.GetOrdinal("is_paid"));
                            invoice_sent = reader.GetBoolean(reader.GetOrdinal("invoice_sent"));
                            date = reader.GetDateTime(8);
                            ID_project = CheckForNullsInt(reader, 9);
                            name = CheckForNullsString(reader, 10);
                        }
                        reader.Dispose();
                        dbh.closeCon();

                        tb[1].Text = amount2;
                        tb[2].Text = bank_acc_number;
                        tb[3].Text = gross_rev;
                        tb[4].Text = ledger_acc_nr;
                        tb[5].Text = tax_code;
                        cb[6].Checked = is_paid;
                        cb[7].Checked = invoice_sent;
                        dtp[8].Format = DateTimePickerFormat.Custom;
                        dtp[8].CustomFormat = "yyyy-MM-dd";
                        dtp[8].Text = Convert.ToString(date);
                        SetComboBox(combo[9]);
                        if (ID_project == "")
                        {
                            combo[9].Text = ID_project;
                        }
                        else
                        {
                            combo[9].Text = SetComboText(ID_project);
                        }
                        tb[10].Text = name;

                        dbh.closeCon();
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
                        string name = null, hardware = null, operating_system = null, applications = null, limit = null, nr_invoices = null, ID_customer = null;
                        bool maintenance_contract = false, is_done = false, BKR = false, creditworthy = false;
                        dbh.openCon();

                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            name = CheckForNullsString(reader, 1);
                            hardware = CheckForNullsString(reader, 2);
                            operating_system = CheckForNullsString(reader, 3);
                            maintenance_contract = reader.GetBoolean(reader.GetOrdinal("maintenance_contract"));
                            applications = CheckForNullsString(reader, 5);
                            limit = Math.Round(reader.GetDecimal(6), 2).ToString();
                            is_done = reader.GetBoolean(reader.GetOrdinal("is_done"));
                            nr_invoices = CheckForNullsInt(reader, 8);
                            BKR = reader.GetBoolean(reader.GetOrdinal("BKR"));
                            creditworthy = reader.GetBoolean(reader.GetOrdinal("creditworthy"));
                            ID_customer = CheckForNullsInt(reader, 11);
                        }

                        reader.Dispose();
                        dbh.closeCon();

                        tb[1].Text = name;
                        tb[2].Text = hardware;
                        tb[3].Text = operating_system;
                        cb[4].Checked = maintenance_contract;
                        tb[5].Text = applications;
                        tb[6].Text = limit;
                        cb[7].Checked = is_done;
                        tb[8].Text = nr_invoices;
                        cb[9].Checked = BKR;
                        cb[10].Checked = creditworthy;
                        SetComboBox(combo[11]);

                        if (ID_customer == "")
                        {
                            combo[11].Text = ID_customer;
                        }
                        else
                        {
                            combo[11].Text = SetComboText(ID_customer);
                        }
                        dbh.closeCon();
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

        //Fill the combobox with records from another table by using the foreign key from this table.
        private void SetComboBox(ComboBox combo)
        {
            cmd = new SqlCommand("SELECT ID,Name FROM "+ GetFKTable() + "", dbh.getCon());
            SqlDataReader reader;

            dbh.openCon();
            reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Load(reader);

            combo.ValueMember = "ID";
            combo.DisplayMember = "Name";

            DataRow emptyrow = dt.NewRow();
            emptyrow["ID"] = 0;
            emptyrow["Name"] = "";
            dt.Rows.Add(emptyrow);

            combo.DataSource = dt;

            reader.Dispose();
        }

        //returns the foreign key.
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

        //sets the combobox on the correct record by using the ID parameter
        private string SetComboText(string ID)
        {
            cmd = new SqlCommand("SELECT Name FROM " + GetFKTable() + " WHERE ID = " + ID +"", dbh.getCon());
            SqlDataReader reader;

            reader = cmd.ExecuteReader();
            string temp = "";

            while (reader.Read())
            {
                temp = reader.GetString(0).ToString();
            }
            return temp;
        }

        private void cb_Customers_MouseUp(object sender, MouseEventArgs e)
        {
            cb_Customers.DroppedDown = true;
        }

        private void cb_Customers_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        //returns all column headers in array.
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

        //saves record to database.
        private void btn_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string insertQuery;
                string ID = cb_Customers.SelectedValue.ToString();

                switch (table)
                {
                    #region Appointments
                    case "tbl_Appointments":
                        TextBox A_description = Application.OpenForms["frm_Edit"].Controls["tb_description"] as TextBox;
                        DateTimePicker A_date = Application.OpenForms["frm_Edit"].Controls["dtp_date"] as DateTimePicker;
                        TextBox A_next_action = Application.OpenForms["frm_Edit"].Controls["tb_next_action"] as TextBox;
                        TextBox A_name = Application.OpenForms["frm_Edit"].Controls["tb_name"] as TextBox;
                        ComboBox A_Id_project = Application.OpenForms["frm_Edit"].Controls["combo_Id_project"] as ComboBox;

                        int convertedID = Convert.ToInt32(A_Id_project.SelectedValue);

                        dbh.openCon();

                        insertQuery = "UPDATE " + table + " SET description='" + A_description.Text + "', date='" + A_date.Text + "', next_action='" + A_next_action.Text + "', ID_project='" + convertedID + "', name='" + A_name.Text + "' WHERE ID=" + ID;
                        SqlCommand cmd = new SqlCommand(insertQuery, dbh.getCon());
                        cmd.ExecuteNonQuery();

                        dbh.closeCon();
                        MessageBox.Show("Save succesful.");
                        break;
                    #endregion
                    #region Customers
                    case "tbl_Customers":
                        TextBox C_name = Application.OpenForms["frm_Edit"].Controls["tb_name"] as TextBox;
                        TextBox C_address1 = Application.OpenForms["frm_Edit"].Controls["tb_address1"] as TextBox;
                        TextBox C_housenr1 = Application.OpenForms["frm_Edit"].Controls["tb_housenr1"] as TextBox;
                        TextBox C_zip_code1 = Application.OpenForms["frm_Edit"].Controls["tb_zip_code1"] as TextBox;
                        TextBox C_place1 = Application.OpenForms["frm_Edit"].Controls["tb_place1"] as TextBox;
                        TextBox C_country1 = Application.OpenForms["frm_Edit"].Controls["tb_country1"] as TextBox;
                        TextBox C_address2 = Application.OpenForms["frm_Edit"].Controls["tb_address2"] as TextBox;
                        TextBox C_housenr2 = Application.OpenForms["frm_Edit"].Controls["tb_housenr2"] as TextBox;
                        TextBox C_zip_code2 = Application.OpenForms["frm_Edit"].Controls["tb_zip_code2"] as TextBox;
                        TextBox C_place2 = Application.OpenForms["frm_Edit"].Controls["tb_place2"] as TextBox;
                        TextBox C_country2 = Application.OpenForms["frm_Edit"].Controls["tb_country2"] as TextBox;
                        TextBox C_phone = Application.OpenForms["frm_Edit"].Controls["tb_phone"] as TextBox;
                        TextBox C_fax = Application.OpenForms["frm_Edit"].Controls["tb_fax"] as TextBox;
                        TextBox C_email = Application.OpenForms["frm_Edit"].Controls["tb_email"] as TextBox;
                        CheckBox C_potential_prospect = Application.OpenForms["frm_Edit"].Controls["cb_potential_prospect"] as CheckBox;

                        int cbIsChecked;

                        if (C_potential_prospect.Checked == true)
                        {
                            cbIsChecked = 1;
                        }
                        else
                        {
                            cbIsChecked = 0;
                        }

                        dbh.openCon();

                        insertQuery = "UPDATE " + table + " SET name='" + C_name.Text + "', address1='" + C_address1.Text + "', housenr1='" + C_housenr1.Text + "', zip_code1='" + C_zip_code1.Text + "', place1='" + C_place1.Text + "', country1='" + C_country1.Text + "', address2='" + C_address2.Text + "', housenr2='" + C_housenr2.Text + "', zip_code2='" + C_zip_code2.Text + "', place2='" + C_place2.Text + "', country2='" + C_country2.Text + "', phone='" + C_phone.Text + "', fax='" + C_fax.Text + "', email='" + C_email.Text + "', potential_prospect='" + cbIsChecked + "' WHERE ID=" + ID;
                        cmd = new SqlCommand(insertQuery, dbh.getCon());
                        cmd.ExecuteNonQuery();

                        dbh.closeCon();
                        MessageBox.Show("Save succesful.");
                        break;
                    #endregion
                    #region Invoices
                    case "tbl_Invoices":
                        TextBox I_amount = Application.OpenForms["frm_Edit"].Controls["tb_amount"] as TextBox;
                        TextBox I_bank_acc_nr = Application.OpenForms["frm_Edit"].Controls["tb_bank_acc_nr"] as TextBox;
                        TextBox I_gross_rev = Application.OpenForms["frm_Edit"].Controls["tb_gross_rev"] as TextBox;
                        TextBox I_ledger_acc_nr = Application.OpenForms["frm_Edit"].Controls["tb_ledger_acc_nr"] as TextBox;
                        TextBox I_tax_code = Application.OpenForms["frm_Edit"].Controls["tb_tax_code"] as TextBox;
                        CheckBox I_is_paid = Application.OpenForms["frm_Edit"].Controls["cb_is_paid"] as CheckBox;
                        CheckBox I_invoice_sent = Application.OpenForms["frm_Edit"].Controls["cb_invoice_sent"] as CheckBox;
                        DateTimePicker I_date = Application.OpenForms["frm_Edit"].Controls["dtp_date"] as DateTimePicker;
                        ComboBox I_id_project = Application.OpenForms["frm_Edit"].Controls["combo_Id_project"] as ComboBox;
                        TextBox I_name = Application.OpenForms["frm_Edit"].Controls["tb_name"] as TextBox;

                        int isPaidIsChecked, invoiceSendIsChecked;

                        convertedID = Convert.ToInt32(I_id_project.SelectedValue);

                        if (I_is_paid.Checked == true)
                        {
                            isPaidIsChecked = 1;
                        }
                        else { isPaidIsChecked = 0; }

                        if (I_invoice_sent.Checked == true)
                        {
                            invoiceSendIsChecked = 1;
                        }
                        else { invoiceSendIsChecked = 0; }

                        dbh.openCon();

                        I_amount.Text = I_amount.Text.Replace(",", ".");
                        I_gross_rev.Text = I_gross_rev.Text.Replace(",", ".");

                        insertQuery = "UPDATE " + table + " SET amount='" + I_amount.Text + "', bank_acc_nr='" + I_bank_acc_nr.Text + "', gross_rev='" + I_gross_rev.Text + "', ledger_acc_nr='" + I_ledger_acc_nr.Text + "', tax_code='" + I_tax_code.Text + "', is_paid='" + isPaidIsChecked + "', invoice_sent='" + invoiceSendIsChecked + "', date='" + I_date.Text + "', Id_project=" + CheckIDNull(convertedID) + ", name='" + I_name.Text + "' WHERE ID=" + ID;

                        cmd = new SqlCommand(insertQuery, dbh.getCon());
                        cmd.ExecuteNonQuery();

                        dbh.closeCon();
                        MessageBox.Show("Save succesful.");

                        break;
                    #endregion
                    #region Projects
                    case "tbl_Projects":
                        TextBox P_name = Application.OpenForms["frm_Edit"].Controls["tb_name"] as TextBox;
                        TextBox P_hardware = Application.OpenForms["frm_Edit"].Controls["tb_hardware"] as TextBox;
                        TextBox P_os = Application.OpenForms["frm_Edit"].Controls["tb_operating_system"] as TextBox;
                        CheckBox P_mc = Application.OpenForms["frm_Edit"].Controls["cb_maintenance_contract"] as CheckBox;
                        TextBox P_applications = Application.OpenForms["frm_Edit"].Controls["tb_applications"] as TextBox;
                        TextBox P_limit = Application.OpenForms["frm_Edit"].Controls["tb_limit"] as TextBox;
                        CheckBox P_isdone = Application.OpenForms["frm_Edit"].Controls["cb_is_done"] as CheckBox;
                        TextBox P_invoices = Application.OpenForms["frm_Edit"].Controls["tb_nr_invoices"] as TextBox;
                        CheckBox P_bkr = Application.OpenForms["frm_Edit"].Controls["cb_BKR"] as CheckBox;
                        CheckBox P_credit = Application.OpenForms["frm_Edit"].Controls["cb_creditworthy"] as CheckBox;
                        ComboBox P_id_customer = Application.OpenForms["frm_Edit"].Controls["combo_Id_customer"] as ComboBox;

                        convertedID = Convert.ToInt32(P_id_customer.SelectedValue);

                        int mcIsChecked, isDoneIsChecked, bkrIsChecked, creditIsChecked;

                        #region If Statements
                        if (P_mc.Checked == true)
                        {
                            mcIsChecked = 1;
                        }
                        else { mcIsChecked = 0; }

                        if (P_isdone.Checked == true)
                        {
                            isDoneIsChecked = 1;
                        }
                        else { isDoneIsChecked = 0; }

                        if (P_bkr.Checked == true)
                        {
                            bkrIsChecked = 1;
                        }
                        else { bkrIsChecked = 0; }

                        if (P_credit.Checked == true)
                        {
                            creditIsChecked = 1;
                        }
                        else { creditIsChecked = 0; }
                        #endregion

                        dbh.openCon();

                        P_limit.Text = P_limit.Text.Replace(",", ".");

                        insertQuery = "UPDATE " + table + " SET name='" + P_name.Text + "', hardware='" + P_hardware.Text + "', operating_system='" + P_os.Text + "', maintenance_contract='" + mcIsChecked + "', applications='" + P_applications.Text + "', limit='" + P_limit.Text + "', is_done='" + isDoneIsChecked + "', nr_invoices='" + P_invoices.Text + "', BKR='" + bkrIsChecked + "', creditworthy='" + creditIsChecked + "', Id_customer='" + CheckIDNull(convertedID) + "' WHERE ID=" + ID;

                        cmd = new SqlCommand(insertQuery, dbh.getCon());
                        cmd.ExecuteNonQuery();

                        dbh.closeCon();
                        MessageBox.Show("Save succesful.");

                        break;
                    #endregion
                }

                dbh.closeCon();
                Form frm_Main = new frm_Main(permissions);
                frm_Main.StartPosition = FormStartPosition.CenterScreen;
                Program.setForm(frm_Main);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Een van de velden is incorrect ingevoerd.");
            }
        }


        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Form frm_Main = new frm_Main(permissions);
            frm_Main.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Main);
            this.Close();
        }

        //checks for null value in ID.
        private object CheckIDNull(int ID)
        {
            if(ID == 0)
            {
                return "NULL";
            }
            else
            {
                return ID;
            }
        }

        //Checks if int is null. When null, return empty string.
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

        //Checks if string is null. When null, return empty string.
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