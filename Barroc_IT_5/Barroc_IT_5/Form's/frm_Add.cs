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
using System.Runtime.InteropServices;

namespace Barroc_IT_5
{
    public partial class frm_Add : Form
    {
        public int permissions;
        SQLDatabaseHandler dbh;
        public TextBox[] tb;
        public Label[] lb;
        public ComboBox[] combo;
        public DateTimePicker[] dtp;
        public CheckBox[] cb;
        public SqlCommand cmd, sqlCmd;
        public string query;
        List<string> columns;

        //Code to move to window, since we're using a custom bar.
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public string table;
        public frm_Add()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
            sqlCmd = new SqlCommand("Select top 1* from Tbl_Customer", dbh.GetCon());
            SqlDataReader sqlDR = sqlCmd.ExecuteReader();
            columns = new List<string>();
            for (int i = 1; i < sqlDR.FieldCount; i++)
            {
                columns.Add(sqlDR.GetName(i));
            }
            sqlDR.Close();
        }

        public frm_Add(int permissions)
        {
            this.permissions = permissions;
            dbh = new SQLDatabaseHandler();
            InitializeComponent();
        }

        public frm_Add(int permissions, string table)
        {
            this.permissions = permissions;
            this.table = table;
            dbh = new SQLDatabaseHandler();
            InitializeComponent();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Form frm_Main = new frm_Main(permissions);
            frm_Main.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Main);
            this.Close();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            Form frm_Main = new frm_Main(permissions);
            frm_Main.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Main);
            this.Close();
        }

        private void frm_Add_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            tb = new TextBox[60];
            lb = new Label[60];
            cb = new CheckBox[60];
            dtp = new DateTimePicker[60];
            combo = new ComboBox[60];
            int x = 150;
            int y = 100;
            int xx = x - 95;

            string[] temp = new string[tb.Length];
            temp = getColumnsName();

            for (int i = 1; i < temp.Length; i++)
            {
                tb[i] = new TextBox();
                tb[i].Name = "tb_" + temp[i].ToString();
                tb[i].Size = new System.Drawing.Size(130, 21);
                tb[i].Location = new Point(x, y);

                lb[i] = new Label();
                lb[i].Name = "lb_" + temp[i].ToString();
                lb[i].Location = new Point(xx, y);
                lb[i].Text = "" + temp[i];

                dtp[i] = new DateTimePicker();
                dtp[i].Name = "dtp_" + temp[i].ToString();
                dtp[i].Size = new System.Drawing.Size(130, 21);
                dtp[i].Location = new Point(x, y);

                cb[i] = new CheckBox();
                cb[i].Name = "cb_" + temp[i].ToString();
                cb[i].Location = new Point(x, y);

                combo[i] = new ComboBox();
                combo[i].Name = "combo_" + temp[i].ToString();
                combo[i].Size = new System.Drawing.Size(130, 21);
                combo[i].Location = new Point(x, y);

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

                if (dtp[i].Name == "dtp_date")
                {
                    this.Controls.Add(dtp[i]);
                    dtp[i].Format = DateTimePickerFormat.Custom;
                    dtp[i].CustomFormat = "yyyy-MM-dd";
                }
                else if (tb[i].Name == "tb_is_paid" || tb[i].Name == "tb_invoice_sent" || tb[i].Name == "tb_is_done" || tb[i].Name == "tb_BKR" || tb[i].Name == "tb_creditworthy" || tb[i].Name == "tb_maintenance_contract")
                {
                    this.Controls.Add(cb[i]);

                    //Checks for checkboxes for BKR and creditworthy.
                    if (table == "tbl_Projects" && i == 10)
                    {
                        cb[10].Enabled = false;
                        cb[9].Click += new EventHandler(this.cbIndexChanged);
                        cb[9].KeyPress += new KeyPressEventHandler(this.cbIndexChanged);
                    }
                }
                else if (tb[i].Name == "tb_Id_project" || tb[i].Name == "tb_Id_customer")
                {

                    this.Controls.Add(combo[i]);
                    SetComboBox(combo[i]);
                }
                else if (tb[i].Name == "tb_potential_prospect")
                {
                    this.Controls.Add(combo[i]);
                    SetComboBoxProspect(combo[i]);
                }
                else
                {
                    this.Controls.Add(tb[i]);
                }

                this.Controls.Add(lb[i]);
                lb[i].Text = lb[i].Text.Replace("_", " ");
            }
            lb_Add.Text = "Add " + ChangeLabel(table);
        }

        //Chooses the text to be added to the label at the top of the window.
        private string ChangeLabel(string table)
        {
            switch(table)
            {
                case "tbl_Customers":
                    return "Customer";
                case "tbl_Projects":
                    return "Project";
                case "tbl_Invoices":
                    return "Invoice";
                case "tbl_Appointments":
                    return "Appointment";
                default:
                    return "";
            }
        }

        //Checks if BKR is checked before creditworthy is allowed to be checked.
        private void cbIndexChanged(object sender, EventArgs e)
        {
            if (cb[9].Checked == false)
            {
                cb[10].Enabled = false;
                cb[10].Checked = false;
            }
            else
            {
                cb[10].Enabled = true;
            }
        }

        private string[] getColumnsName()
        {
            List<string> listacolumnas = new List<string>();
            using (SqlCommand command = dbh.GetCon().CreateCommand())
            {
                command.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = '" + table + "' and t.type = 'U'";
                dbh.OpenCon();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listacolumnas.Add(reader.GetString(0));
                    }
                }
                dbh.CloseCon();
            }
            return listacolumnas.ToArray();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Add();
        }

        public void Add()
        {
            bool isFinished = false;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbh.GetCon();

                if (table == "tbl_Appointments")
                {
                    int ID = Convert.ToInt32(combo[4].SelectedValue);
                    if (ID != 0)
                    {
                        cmd.CommandText = "INSERT INTO TBL_APPOINTMENTS(DESCRIPTION, DATE, NEXT_ACTION, ID_PROJECT, NAME) VALUES (@DESCRIPTION, @DATE, @NEXT_ACTION, @ID_PROJECT, @NAME)";
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO TBL_APPOINTMENTS(DESCRIPTION, DATE, NEXT_ACTION, NAME) VALUES (@DESCRIPTION, @DATE, @NEXT_ACTION, @NAME)";
                    }
                    cmd.Parameters.AddWithValue("@DESCRIPTION", tb[1].Text);
                    cmd.Parameters.AddWithValue("@DATE", dtp[2].Value);
                    cmd.Parameters.AddWithValue("@NEXT_ACTION", tb[3].Text);
                    if (ID != 0)
                    {
                        cmd.Parameters.AddWithValue("@ID_PROJECT", combo[4].SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@NAME", tb[5].Text);
                }
                else if (table == "tbl_Customers")
                {
                    cmd.CommandText = "INSERT INTO TBL_CUSTOMERS (NAME, ADDRESS1, HOUSENR1, ZIP_CODE1, PLACE1, COUNTRY1, ADDRESS2, HOUSENR2, ZIP_CODE2, PLACE2, COUNTRY2, PHONE, FAX, EMAIL, POTENTIAL_PROSPECT) VALUES (@NAME, @ADDRESS1, @HOUSENR1, @ZIP_CODE1, @PLACE1, @COUNTRY1, @ADDRESS2, @HOUSENR2, @ZIP_CODE2, @PLACE2, @COUNTRY2, @PHONE, @FAX, @EMAIL, @POTENTIAL_PROSPECT)";
                    cmd.Parameters.AddWithValue("@NAME", tb[1].Text);
                    cmd.Parameters.AddWithValue("@ADDRESS1", tb[2].Text);
                    cmd.Parameters.AddWithValue("@HOUSENR1",tb[3].Text);
                    cmd.Parameters.AddWithValue("@ZIP_CODE1", tb[4].Text);
                    cmd.Parameters.AddWithValue("@PLACE1", tb[5].Text);
                    cmd.Parameters.AddWithValue("@COUNTRY1",tb[6].Text);
                    cmd.Parameters.AddWithValue("@ADDRESS2", tb[7].Text);
                    cmd.Parameters.AddWithValue("@HOUSENR2", tb[8].Text);
                    cmd.Parameters.AddWithValue("@ZIP_CODE2", tb[9].Text);
                    cmd.Parameters.AddWithValue("@PLACE2", tb[10].Text);
                    cmd.Parameters.AddWithValue("@COUNTRY2", tb[11].Text);
                    cmd.Parameters.AddWithValue("@PHONE", tb[12].Text);
                    cmd.Parameters.AddWithValue("@FAX", tb[13].Text);
                    cmd.Parameters.AddWithValue("@EMAIL", tb[14].Text);
                    cmd.Parameters.AddWithValue("@POTENTIAL_PROSPECT", combo[15].SelectedValue);
                }

                else if(table == "tbl_Invoices")
                {
                    int ID = Convert.ToInt32(combo[9].SelectedValue);

                    if (ID != 0)
                    {
                        cmd.CommandText = "INSERT INTO TBL_INVOICES ( AMOUNT, BANK_ACC_NR, GROSS_REV, LEDGER_ACC_NR, TAX_CODE, ID_PROJECT, IS_PAID, DATE, INVOICE_SENT, NAME) VALUES ( @AMOUNT, @BANK_ACC_NR, @GROSS_REV, @LEDGER_ACC_NR, @TAX_CODE, @ID_PROJECT, @IS_PAID, @DATE, @INVOICE_SENT, @NAME)";
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO TBL_INVOICES ( AMOUNT, BANK_ACC_NR, GROSS_REV, LEDGER_ACC_NR, TAX_CODE, IS_PAID, DATE, INVOICE_SENT, NAME) VALUES ( @AMOUNT, @BANK_ACC_NR, @GROSS_REV, @LEDGER_ACC_NR, @TAX_CODE, @IS_PAID, @DATE, @INVOICE_SENT, @NAME)";
                    }

                    cmd.Parameters.AddWithValue("@AMOUNT", tb[1].Text.Replace(".", ","));
                    cmd.Parameters.AddWithValue("@BANK_ACC_NR", tb[2].Text);
                    cmd.Parameters.AddWithValue("@GROSS_REV", tb[3].Text.Replace(".", ","));
                    cmd.Parameters.AddWithValue("@LEDGER_ACC_NR", tb[4].Text);
                    cmd.Parameters.AddWithValue("@TAX_CODE", tb[5].Text);
                    cmd.Parameters.AddWithValue("@IS_PAID", checkboxState(cb[6]));
           
                    cmd.Parameters.AddWithValue("@INVOICE_SENT", checkboxState(cb[7]));
                    cmd.Parameters.AddWithValue("@DATE", dtp[8].Value);
                    if (ID != 0)
                    {
                        cmd.Parameters.AddWithValue("@ID_PROJECT", combo[9].SelectedValue);
                    }
                    cmd.Parameters.AddWithValue("@NAME", tb[10].Text);
                }

                else if (table == "tbl_Projects")
                {
                    int ID = Convert.ToInt32(combo[11].SelectedValue);

                    if (ID != 0)
                    {
                        cmd.CommandText = "INSERT INTO TBL_PROJECTS (NAME, HARDWARE, OPERATING_SYSTEM, MAINTENANCE_CONTRACT, APPLICATIONS, LIMIT, ID_CUSTOMER, IS_DONE, NR_INVOICES, BKR, CREDITWORTHY) VALUES (@NAME, @HARDWARE, @OPERATING_SYSTEM, @MAINTENANCE_CONTRACT, @APPLICATIONS, @LIMIT, @ID_CUSTOMER, @IS_DONE, @NR_INVOICES, @BKR, @CREDITWORTHY)";
                    }
                    else
                    {
                        cmd.CommandText = "INSERT INTO TBL_PROJECTS (NAME, HARDWARE, OPERATING_SYSTEM, MAINTENANCE_CONTRACT, APPLICATIONS, LIMIT, IS_DONE, NR_INVOICES, BKR, CREDITWORTHY) VALUES (@NAME, @HARDWARE, @OPERATING_SYSTEM, @MAINTENANCE_CONTRACT, @APPLICATIONS, @LIMIT, @IS_DONE, @NR_INVOICES, @BKR, @CREDITWORTHY)";

                    }
                    cmd.Parameters.AddWithValue("@NAME", tb[1].Text);
                    cmd.Parameters.AddWithValue("@HARDWARE", tb[2].Text);
                    cmd.Parameters.AddWithValue("@OPERATING_SYSTEM", tb[3].Text);
                    cmd.Parameters.AddWithValue("@MAINTENANCE_CONTRACT", checkboxState(cb[4]));
                    cmd.Parameters.AddWithValue("@APPLICATIONS", tb[5].Text);
                    cmd.Parameters.AddWithValue("@LIMIT", tb[6].Text.Replace(".", ","));
                    
                    cmd.Parameters.AddWithValue("@IS_DONE", checkboxState(cb[7]));
                    cmd.Parameters.AddWithValue("@NR_INVOICES", tb[8].Text);
                    cmd.Parameters.AddWithValue("@BKR", checkboxState(cb[9]));
                    cmd.Parameters.AddWithValue("@CREDITWORTHY", checkboxState(cb[10]));
                    if (ID != 0)
                    {
                        cmd.Parameters.AddWithValue("@ID_CUSTOMER", combo[11].SelectedValue);
                    }
                }

                dbh.OpenCon();
                cmd.ExecuteNonQuery();
                dbh.CloseCon();

                cmd.Dispose();

                MessageBox.Show("Sucessfully added this record.", "Succes!");

                isFinished = true;
            }

            catch(SqlException ex)
            {
                MessageBox.Show("One or more fields contain incorrect data." + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            finally
            {
                if (isFinished)
                {
                    dbh.CloseCon();
                    Form frm_Main = new frm_Main();
                    frm_Main.StartPosition = FormStartPosition.CenterScreen;
                    Program.setForm(frm_Main);
                    this.Close();
                }
            }
        }

        private void SetComboBoxProspect(ComboBox comboBox)
        {
            comboBox.KeyPress += new KeyPressEventHandler(Combo_keyPress);
            comboBox.DisplayMember = "Text";
            comboBox.ValueMember = "Value";

            var items = new[] { 
                new { Text = "Active", Value = "Active" }, 
                new { Text = "Inactive", Value = "Inactive" }, 
                new { Text = "Potential", Value = "Potential" },
            };
            comboBox.DataSource = items;
        }

        private void SetComboBox(ComboBox combo)
        {
            cmd = new SqlCommand("SELECT ID,Name FROM " + GetFKTable() + "", dbh.GetCon());
            SqlDataReader reader;
            combo.KeyPress += new KeyPressEventHandler(Combo_keyPress);
            dbh.OpenCon();
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

        private void Combo_keyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
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
            cmd = new SqlCommand("SELECT Name FROM " + GetFKTable() + " WHERE ID = " + ID_customer + "", dbh.GetCon());
            SqlDataReader reader;

            reader = cmd.ExecuteReader();
            string temp = "";

            while (reader.Read())
            {
                temp = reader.GetString(0).ToString();
            }
            return temp;
        }

        private string checkboxState(CheckBox cb)
        {
            int temp = 235;
                    if(cb.Checked)
                    {
                        temp = 1;
                    }
                    else
                    {
                        temp = 0;
                    }
            return temp.ToString();
        }

        private object CheckIDNull(int ID)
        {
            if (ID == 0)
            {
                return null;
            }
            else
            {
                return ID;
            }
        }

        //Continuation of the code to move the window.
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void lb_Add_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}