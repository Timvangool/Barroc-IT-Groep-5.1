﻿using System;
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
        public int permissions;
        public SqlDataReader reader;
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

        private void cb_Customers_SelectedIndexChanged(object sender, EventArgs e)
        {
            dbh.closeCon();
            string ID = cb_Customers.SelectedValue.ToString();

            object[] temp2 = new object[7583];

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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                dbh.closeCon();
            }

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

                            
                            string description = reader.GetString(1);
                            //string date = reader.GetString(2).ToString();
                            string next_action = reader.GetString(3);
                            string ID_project;

                            if ( reader.IsDBNull(4) )
                            {
                                ID_project = "";
                            }
                            else
                            {
                                ID_project = reader.GetString(4).ToString();
                            }                       
                            string name = reader.GetString(5).ToString();

                            tb[1].Text = description;
                            //tb[2].Text = date;
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

                case "tbl_Customers":
                    try
                    {
                        dbh.openCon();
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {


                            string name = reader.GetString(1);
                            string address = reader.GetString(2);
                            //string date = reader.GetString(2).ToString();
                            string next_action = reader.GetString(3);
                            string ID_project;

                            if (reader.IsDBNull(4))
                            {
                                ID_project = "";
                            }
                            else
                            {
                                ID_project = reader.GetString(4).ToString();
                            }
                            string name = reader.GetString(5).ToString();

                            tb[1].Text = description;
                            //tb[2].Text = date;
                            tb[3].Text = next_action;
                            tb[4].Text = ID_project;
                            tb[5].Text = name;
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;

                case "tbl_Invoices":
                    break;

                case "tbl_Projects":
                    break;
                    
            }
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
            List<string> listacolumnas = new List<string>();
            using (SqlCommand command = dbh.getCon().CreateCommand())
            {
                command.CommandText = "select c.name from sys.columns c inner join sys.tables t on t.object_id = c.object_id and t.name = '" + table + "' and t.type = 'U'";
                dbh.openCon();
                using (reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listacolumnas.Add(reader.GetString(0));
                    }
                }
                dbh.closeCon();
            }
            reader.Dispose();

            return listacolumnas.ToArray();
        }
    }
}
