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
                    dtp[i].CustomFormat = "MM-dd-yyy 'at' HH:mm";
                }
                else if (tb[i].Name == "tb_Potential_Prospect" || tb[i].Name == "tb_is_paid" || tb[i].Name == "tb_Invoice_Send" || tb[i].Name == "tb_Is_Done" || tb[i].Name == "tb_BKR" || tb[i].Name == "tb_Credit_Worthy")
                {
                    this.Controls.Add(cb[i]);
                }
                else
                {
                    this.Controls.Add(tb[i]);
                }

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
            Add();
        }

        public void Add()
        {
            try
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

                string[] values = new string[tb.Count()];

                for (int i = 1; i < values.Count(); i++)
                {
                    values[i] = tb[i].Text;
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = dbh.getCon();

                cmd.CommandText = string.Format("INSERT INTO " + tabel + " ({0}) VALUES('{1}')", string.Join(", ", columns.Where(s => !string.IsNullOrEmpty(s))), string.Join("', '", values));
                cmd.ExecuteNonQuery();

                MessageBox.Show("Sucessfully added " + tempDepartment + ".");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(@"Something went wrong : 
" + ex.ToString());
            }
        }
    }

}

