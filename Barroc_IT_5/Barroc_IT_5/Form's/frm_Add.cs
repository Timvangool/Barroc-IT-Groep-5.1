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

        public string tempDepartment, tabel;
        public frm_Add()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
            sqlCmd = new SqlCommand("Select top 1* from Tbl_Customer", dbh.getCon());
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

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Form frm_Main = new frm_Main();
            frm_Main.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Main);
            this.Close();
        }

        private void btn_Back_Click(object sender, EventArgs e)
        {
            Form frm_Main = new frm_Main();
            frm_Main.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Main);
            this.Close();
        }

        private void frm_Add_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void cb_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 1; i < tb.Count(); i++)
                {
                    this.Controls.Remove(tb[i]);
                    tb[i].Dispose();
                    this.Controls.Remove(lb[i]);
                    lb[i].Dispose();
                    this.Controls.Remove(cb[i]);
                    cb[i].Dispose();
                    this.Controls.Remove(dtp[i]);
                    dtp[i].Dispose();
                    this.Controls.Remove(combo[i]);
                    combo[i].Dispose();
                }
            }
            catch
            {

            }

            tempDepartment = cb_Departments.Text;

            //switch (tempDepartment)
            //{
            //    case "Customer":
            //        tb = new TextBox[14];
            //        lb = new Label[14];
            //        cb = new CheckBox[14];
            //        dtp = new DateTimePicker[14];
            //        break;
            //    case "Invoice":
            //        tb = new TextBox[10];
            //        lb = new Label[10];
            //        cb = new CheckBox[10];
            //        dtp = new DateTimePicker[10];
            //        break;
            //    case "Project":
            //        tb = new TextBox[11];
            //        lb = new Label[11];
            //        cb = new CheckBox[11];
            //        dtp = new DateTimePicker[11];
            //        break;
            //    case "Appointment":
            //        tb = new TextBox[6];
            //        lb = new Label[6];
            //        cb = new CheckBox[6];
            //        dtp = new DateTimePicker[6];
            //        break;
            //    default:
            //        MessageBox.Show("Something went wrong :c");
            //        break;
            //}

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

                if (dtp[i].Name == "dtp_Date")
                {
                    this.Controls.Add(dtp[i]);
                    dtp[i].Format = DateTimePickerFormat.Custom;
                    dtp[i].CustomFormat = "yyyy-MM-dd";
                }
                else if (tb[i].Name == "tb_Potential_Prospect" || tb[i].Name == "tb_is_paid" || tb[i].Name == "tb_Invoice_Send" || tb[i].Name == "tb_Is_Done" || tb[i].Name == "tb_BKR" || tb[i].Name == "tb_Credit_Worthy" || tb[i].Name == "tb_Maintenance_Contract")
                {
                    this.Controls.Add(cb[i]);
                }
                else if (tb[i].Name == "tb_Id_Project" || tb[i].Name == "tb_Id_Customer" )
                {

                    this.Controls.Add(combo[i]);
                    SetComboBox(combo[i]);
                }
                else
                {
                    this.Controls.Add(tb[i]);
                }

                this.Controls.Add(lb[i]); 
                lb[i].Text = lb[i].Text.Replace("_", " ");

            }
        }

        private string[] getColumnsName()
        {
            switch (tempDepartment)
            {
                case "Customer":
                    tabel = "TBL_CUSTOMERS";
                    break;
                case "Invoice":
                    tabel = "TBL_INVOICES";
                    break;
                case "Project":
                    tabel = "TBL_PROJECTS";
                    break;
                case "Appointment":
                    tabel = "TBL_APPOINTMENTS";
                    break;
                default:
                    MessageBox.Show("Something went wrong :c");
                    break;
            }
            List<string> listacolumnas = new List<string>();
            using (SqlCommand command = dbh.getCon().CreateCommand())
            {
                command.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = '" + tabel + "' and t.type = 'U'";
                dbh.openCon();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listacolumnas.Add(reader.GetString(0));
                    }
                }
                dbh.closeCon();
            }
            return listacolumnas.ToArray();
        }

        private void cb_Departments_MouseUp(object sender, MouseEventArgs e)
        {
            cb_Departments.DroppedDown = true;
        }

        private void cb_Departments_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            switch (tempDepartment)
            {
                case "Customer":
                    tabel = "TBL_CUSTOMERS";
                    break;
                case "Invoice":
                    tabel = "TBL_INVOICES";
                    break;
                case "Project":
                    tabel = "TBL_PROJECTS";
                    break;
                case "Appointment":
                    tabel = "TBL_APPOINTMENTS";
                    break;
                default:
                    MessageBox.Show("Something went wrong :c");
                    break;
            }
            Add();
        }

        public void Add()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbh.getCon();

                if (cb_Departments.SelectedItem.ToString() == "Appointment")
                {
                    cmd.CommandText = "INSERT INTO TBL_APPOINTMENTS(DESCRIPTION, DATE, NEXT_ACTION, ID_PROJECT, NAME) VALUES (@DESCRIPTION, @DATE, @NEXT_ACTION, @ID_PROJECT, @NAME)";
                    cmd.Parameters.AddWithValue("@DESCRIPTION", tb[1].Text);
                    cmd.Parameters.AddWithValue("@DATE", dtp[2].Value);
                    cmd.Parameters.AddWithValue("@NEXT_ACTION", tb[3].Text);
                    cmd.Parameters.AddWithValue("@ID_PROJECT", combo[4].SelectedValue);
                    cmd.Parameters.AddWithValue("@NAME", tb[5].Text);
                }
                else if (cb_Departments.SelectedItem.ToString() == "Customer")
                {
                    cmd.CommandText = "INSERT INTO TBL_CUSTOMERS (NAME, ADDRESS1, HOUSENR1, ZIP_CODE1, PLACE1, COUNTRY, ADDRESS2, HOUSENR2, ZIP_CODE2, PLACE2, PHONE, FAX, EMAIL, POTENTIAL_PROSPECT) VALUES (@NAME, @ADDRESS1, @HOUSENR1, @ZIP_CODE1, @PLACE1, @COUNTRY, @ADDRESS2, @HOUSENR2, @ZIP_CODE2, @PLACE2, @PHONE, @FAX, @EMAIL, @POTENTIAL_PROSPECT)";
                    cmd.Parameters.AddWithValue("@NAME", tb[1].Text);
                    cmd.Parameters.AddWithValue("@ADDRESS1", tb[2].Text);
                    cmd.Parameters.AddWithValue("@HOUSENR1",tb[3].Text);
                    cmd.Parameters.AddWithValue("@ZIP_CODE1", tb[4].Text);
                    cmd.Parameters.AddWithValue("@PLACE1", tb[5].Text);
                    cmd.Parameters.AddWithValue("@COUNTRY",tb[6].Text);
                    cmd.Parameters.AddWithValue("@ADDRESS2", tb[7].Text);
                    cmd.Parameters.AddWithValue("@HOUSENR2", tb[8].Text);
                    cmd.Parameters.AddWithValue("@ZIP_CODE2", tb[9].Text);
                    cmd.Parameters.AddWithValue("@PLACE2", tb[10].Text);
                    cmd.Parameters.AddWithValue("@PHONE", tb[11].Text);
                    cmd.Parameters.AddWithValue("@FAX", tb[12].Text);
                    cmd.Parameters.AddWithValue("@EMAIL", tb[13].Text);
                    cmd.Parameters.AddWithValue("@POTENTIAL_PROSPECT", checkboxState());
                }

                else if(cb_Departments.SelectedItem.ToString() == "Invoice")
                {
                    cmd.CommandText = "INSERT INTO TBL_INVOICES (BANK_ACC_NR, PRICE, GROSS_REV, LEDGER_ACC_NR, TAX_CODE, ID_PROJECT, IS_PAID, DATE, INVOICE_SEND, NAME) VALUES (@BANK_ACC_NR, @PRICE, @GROSS_REV, @LEDGER_ACC_NR, @TAX_CODE, @ID_PROJECT, @IS_PAID, @DATE, @INVOICE_SEND, @NAME)";
                    cmd.Parameters.AddWithValue("@BANK_ACC_NR", tb[1].Text);
                    cmd.Parameters.AddWithValue("@PRICE", tb[2].Text);
                    cmd.Parameters.AddWithValue("@GROSS_REV", tb[3].Text);
                    cmd.Parameters.AddWithValue("@LEDGER_ACC_NR", tb[4].Text);
                    cmd.Parameters.AddWithValue("@TAX_CODE", tb[5].Text);
                    cmd.Parameters.AddWithValue("@ID_PROJECT", combo[6].SelectedValue);
                    cmd.Parameters.AddWithValue("@IS_PAID", checkboxState());
                    cmd.Parameters.AddWithValue("@DATE", dtp[8].Value);
                    cmd.Parameters.AddWithValue("@INVOICE_SEND", checkboxState());
                    cmd.Parameters.AddWithValue("@NAME", tb[10].Text);
                }

                else if (cb_Departments.SelectedItem.ToString() == "Project")
                {
                    cmd.CommandText = "INSERT INTO TBL_PROJECTS (NAME, HARDWARE, OPERATING_SYSTEM, MAINTENANCE_CONTRACT, APPLICATIONS, LIMIT, ID_CUSTOMER, IS_DONE, NR_INVOICES, BKR, CREDIT_WORTHY) VALUES (@NAME, @HARDWARE, @OPERATING_SYSTEM, @MAINTENANCE_CONTRACT, @APPLICATIONS, @LIMIT, @ID_CUSTOMER, @IS_DONE, @NR_INVOICES, @BKR, @CREDIT_WORTHY)";
                    cmd.Parameters.AddWithValue("@NAME", tb[1].Text);
                    cmd.Parameters.AddWithValue("@HARDWARE", tb[2].Text);
                    cmd.Parameters.AddWithValue("@OPERATING_SYSTEM", tb[3].Text);
                    cmd.Parameters.AddWithValue("@MAINTENANCE_CONTRACT", checkboxState());
                    cmd.Parameters.AddWithValue("@APPLICATIONS", tb[5].Text);
                    cmd.Parameters.AddWithValue("@LIMIT", tb[6].Text.Replace(".", ","));
                    cmd.Parameters.AddWithValue("@ID_CUSTOMER", combo[7].SelectedValue);
                    cmd.Parameters.AddWithValue("@IS_DONE", checkboxState());
                    cmd.Parameters.AddWithValue("@NR_INVOICES", tb[9].Text);
                    cmd.Parameters.AddWithValue("@BKR", checkboxState());
                    cmd.Parameters.AddWithValue("@CREDIT_WORTHY", checkboxState());

                }

                dbh.openCon();
                cmd.ExecuteNonQuery();
                dbh.closeCon();

                cmd.Dispose();

                MessageBox.Show("Sucessfully added " + tempDepartment + ".");
            }

            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

            finally
            {
                dbh.closeCon();
                Form frm_Main = new frm_Main();
                frm_Main.StartPosition = FormStartPosition.CenterScreen;
                Program.setForm(frm_Main);
                this.Close();
            }

        }
        private void SetComboBox(ComboBox combo)
        {
            cmd = new SqlCommand("SELECT ID,Name FROM " + GetFKTable() + "", dbh.getCon());
            SqlDataReader reader;

            dbh.openCon();
            reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Load(reader);

            combo.ValueMember = "ID";
            combo.DisplayMember = "Name";

            combo.DataSource = dt;

            reader.Dispose();
        }

        private string GetFKTable()
        {
            switch (tempDepartment)
            {
                case "Customer":
                    tabel = "TBL_CUSTOMERS";
                    break;
                case "Invoice":
                    tabel = "TBL_INVOICES";
                    break;
                case "Project":
                    tabel = "TBL_PROJECTS";
                    break;
                case "Appointment":
                    tabel = "TBL_APPOINTMENTS";
                    break;
                default:
                    MessageBox.Show("Something went wrong :c");
                    break;
            }

            switch (tabel)
            {
                case "TBL_APPOINTMENTS":
                    return "tbl_Projects";
                case "TBL_PROJECTS":
                    return "tbl_Customers";
                case "TBL_INVOICES":
                    return "tbl_Projects";
                default:
                    return "";
            }
        }
        private string SetComboText(string ID_customer)
        {
            cmd = new SqlCommand("SELECT Name FROM " + GetFKTable() + " WHERE ID = " + ID_customer + "", dbh.getCon());
            SqlDataReader reader;

            reader = cmd.ExecuteReader();
            string temp = "";

            while (reader.Read())
            {
                temp = reader.GetString(0).ToString();
            }
            return temp;
        }

        private string checkboxState()
        {
            int temp = 42;

            switch (tempDepartment)
            {
                case "Customer":
                    if(cb[14].Checked)
                    {
                        temp = 1;
                    }
                    else
                    {
                        temp = 0;
                    }
                    break;
                case "Invoice":
                    if (cb[7].Checked || cb[9].Checked)
                    {
                        temp = 1;
                    }
                    else
                    {
                        temp = 0;
                    }
                    break;
                case "Project":
                    if(cb[4].Checked || cb[8].Checked || cb[10].Checked || cb[11].Checked)
                    {
                        temp = 1;
                    }
                    else
                    {
                        temp = 0;
                    }
                    break;
                case "Appointment":

                    break;
                default:
                    MessageBox.Show("Something went wrong :c");
                    break;
            }

            return temp.ToString();
        }
    }
}