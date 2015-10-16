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
            btn_Refresh.Visible = false;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void cb_Department_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    foreach (TextBox t in tb)
            //    {
            //        this.Controls.Remove(t);
            //        t.Dispose();
            //    }
            //
            //    foreach(Label l in lb)
            //    {
            //        this.Controls.Remove(l);
            //        l.Dispose();
            //
            //    }
            //}
            //
            //catch
            //{
            //}

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

                if(y >= 450)
                {
                    y = 100;
                    x += 350;
                    xx += 350;
                }
                if(lb[i].Text == "Maintenance_Contract")
                {
                    lb[i].Text = @"Maintenance
Contract";
                    lb[i].Size = new System.Drawing.Size(100, 30);
                }
                this.Controls.Add(tb[i]);
                this.Controls.Add(lb[i]);
            }

            btn_Refresh.Visible = true;
            cb_Departments.Enabled = false;
        }

        private string[] getColumnsName()
        {
            switch (tempDepartment)
            {
                case"Customer":
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form add = new frm_Add();
            add.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(add);
            this.Close();
        }  
    }
}
