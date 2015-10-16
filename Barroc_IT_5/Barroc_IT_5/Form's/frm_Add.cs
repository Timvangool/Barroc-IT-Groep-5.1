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
        public SqlCommand cmd;
        public string query;

        public string tempDepartment, tabel;
        public frm_Add()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
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
                }
            }
            catch
            {

            }

            tempDepartment = cb_Departments.Text;
            int amount = 101010;
            tb = new TextBox[amount];
            lb = new Label[amount];
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
                this.Controls.Add(tb[i]);
                this.Controls.Add(lb[i]);
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

            string[] temp = new string[132213213];

            for (int i = 0; i < tb.Length; i++)
            {
                temp[i] = tb[i].ToString();
            }

            #region customers

            query = "INSERT INTO TBL_CUSTOMERS (NAME, ADDRESS1, HOUSENR1, ZIP_CODE1, PLACE1, COUNTRY, ADDRESS2, HOUSENR2, ZIP_CODE2, PHONE, FAX, EMAIL, POTENTIAL_PROSPECT) VALUES ('@NAME','@ADDRESS1','@HOUSENR1','@ZIP_CODE1','@PLACE1','@COUNTRY','@ADDRESS2','@HOUSENR2', '@ZIP_CODE2','@PHONE','@FAX','@EMAIL','@POTENTIAL_PROSPECT')";

            cmd = new SqlCommand();

            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@Name", temp[1]));
            cmd.Parameters.Add(new SqlParameter("@ADDRESS1", temp[2]));
            cmd.Parameters.Add(new SqlParameter("@HOUSENR1", temp[3]));
            cmd.Parameters.Add(new SqlParameter("@ZIP_CODE1", temp[4]));
            cmd.Parameters.Add(new SqlParameter("@PLACE1", temp[5]));
            cmd.Parameters.Add(new SqlParameter("@COUNTY", temp[6]));
            cmd.Parameters.Add(new SqlParameter("@ADDRESS2", temp[7]));
            cmd.Parameters.Add(new SqlParameter("@HOUSENR2", temp[8]));
            cmd.Parameters.Add(new SqlParameter("@ZIP_CODE2", temp[9]));
            cmd.Parameters.Add(new SqlParameter("@PLACE2", temp[10]));
            cmd.Parameters.Add(new SqlParameter("@PHONE", temp[11]));
            cmd.Parameters.Add(new SqlParameter("@FAX", temp[12]));
            cmd.Parameters.Add(new SqlParameter("@EMAIL", temp[13]));
            cmd.Parameters.Add(new SqlParameter("@POTENTIAL_PROSPECT", temp[14]));

            dbh.openCon();
            cmd.ExecuteNonQuery();
            dbh.closeCon();

            #endregion

            #region invoices

            query = "INSERT INTO TBL_INVOICES (BANK_ACC_NR, PRICE, GROSS_REV, LEDGER_ACC_NR, TAX_CODE, ID_PROJECT, IS_PAYED, DATE, INVOICE_SEND) VALUES ('@BANK_ACC_NR', '@PRICE', '@GROSS_REV', '@LEDGER_ACC_NR', '@TAX_CODE', '@ID_PROJECT', '@IS_PAYED', '@DATE', '@INVOICE_SEND')";

            cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@BANK_ACC_NR", temp[1]));
            cmd.Parameters.Add(new SqlParameter("@PRICE", temp[2]));
            cmd.Parameters.Add(new SqlParameter("@GROSS_REV", temp[3]));
            cmd.Parameters.Add(new SqlParameter("@LEDGER_ACC_NR", temp[4]));
            cmd.Parameters.Add(new SqlParameter("@TAX_CODE", temp[5]));
            cmd.Parameters.Add(new SqlParameter("@ID_PROJECT", Convert.ToInt32(temp[6])));
            cmd.Parameters.Add(new SqlParameter("@IS_PAYED", temp[7]));
            cmd.Parameters.Add(new SqlParameter("@DATE", temp[8]));
            cmd.Parameters.Add(new SqlParameter("@INVOICE_SEND", temp[9]));

            dbh.openCon();
            cmd.ExecuteNonQuery();
            dbh.closeCon();

            #endregion

            #region projects
            query = "INSERT INTO TBL_PROJECTS (NAME, HARDWARE, OPERATING_SYSTEM, MAINTENANCE_CONTRACT, APPLICATIONS, LIMIT, IS_DONE, NR_INVOICES, BKR, CREDITWORTHY, ID_CUSTOMER) VALUES ('@NAME','@HARDWARE','@OPERATING_SYSTEM','@MAINTENANCE_CONTRACT','@APPLICATIONS','@LIMIT','@IS_DONE','@NR_INVOICES', '@BKR','@CREDITWORTHY','@ID_CUSTOMER')";

            cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@NAME", temp[1]));
            cmd.Parameters.Add(new SqlParameter("@HARDWARE", temp[2]));
            cmd.Parameters.Add(new SqlParameter("@OPERATING_SYSTEM", temp[3]));
            cmd.Parameters.Add(new SqlParameter("@MAINTENANCE_CONTRACT", temp[4]));
            cmd.Parameters.Add(new SqlParameter("@APPLICATIONS", temp[5]));
            cmd.Parameters.Add(new SqlParameter("@LIMIT", temp[6]));
            cmd.Parameters.Add(new SqlParameter("@IS_DONE", temp[7]));
            cmd.Parameters.Add(new SqlParameter("@NR_INVOICES", temp[8]));
            cmd.Parameters.Add(new SqlParameter("@BKR", temp[9]));
            cmd.Parameters.Add(new SqlParameter("@CREDITWORTHY", temp[10]));
            cmd.Parameters.Add(new SqlParameter("@ID_CUSTOMER", Convert.ToInt32(temp[11])));

            dbh.openCon();
            cmd.ExecuteNonQuery();
            dbh.closeCon();
            #endregion

            #region appointments

            query = "INSERT INTO TBL_APPOINTMENTS (DESCRIPTION, DATE, NEXT_ACTION, ID_PROJECT) VALUES ('@DESCRIPTION','@DATE','@NEXT_ACTION','@ID_PROJECT')";

            cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(new SqlParameter("@DESCRIPTION", temp[1]));
            cmd.Parameters.Add(new SqlParameter("@DATE", temp[2]));
            cmd.Parameters.Add(new SqlParameter("@NEXT_ACTION", temp[3]));
            cmd.Parameters.Add(new SqlParameter("@ID_PROJECT", Convert.ToInt32(temp[4])));

            dbh.openCon();
            cmd.ExecuteNonQuery();
            dbh.closeCon();

            #endregion
        }
    }
}

