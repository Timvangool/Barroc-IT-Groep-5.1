﻿//#define TIMH
//#define TIMG
#define KEVIN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Barroc_IT_Groep5
{
    class SQLDatabaseHandler
    {
        string conString;
        SqlConnection con;

        public SQLDatabaseHandler()
        {
#if TIMH
            conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Tim\Documents\Radius\2.1\Barroc-IT-groep-6\Main programma\Database\Barroc_DB.mdf;Integrated Security=True;Connect Timeout=30";
#endif
#if TIMG
            conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=Q:\Documenten\Barroc_DB.mdf;Integrated Security=True;Connect Timeout=30";
#endif
#if KEVIN
            conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Kevin\Documents\Visual Studio 2013\Projects\Barroc-IT\Database\Barroc_DB.mdf;Integrated Security=True;Connect Timeout=30";
#endif
            con = new SqlConnection(conString);
        }

        public string GetConState()
        {
            if (con.State == ConnectionState.Open)
            {
                return "Open";
            }
            else
            {
                return "False";
            }

        }
        public SqlConnection GetCon()
        {
            return con;
        }

        public void OpenCon()
        {
            if (con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }

        public void CloseCon()
        {
            con.Close();
        }

        public void TestCon()
        {
            try
            {
                OpenCon();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(@"An error has occured:

" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CloseCon();
            }
        }

        public void FillDataGridView(DataGridView dgv, string query)
        {
            OpenCon();

            SqlDataAdapter adapter = new SqlDataAdapter(query, GetCon());
            DataSet dt = new DataSet();
            adapter.Fill(dt);
            dgv.DataSource = dt.Tables[0];
            if(dgv.Columns.Contains("Limit"))
            {
                dgv.Columns["Limit"].DefaultCellStyle.Format = "N2";
            }
            else if (dgv.Columns.Contains("Gross_Rev") || dgv.Columns.Contains("Amount"))
            {
                dgv.Columns["Gross_Rev"].DefaultCellStyle.Format = "N2";
                dgv.Columns["Amount"].DefaultCellStyle.Format = "N2";
            }
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.ReadOnly = true;

            CloseCon();
        }
    }
}
