using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Barroc_IT_Groep5;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace Barroc_IT_5
{
    public partial class frm_Show : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        SQLDatabaseHandler dbh;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public string query, department;
        public int permissions;
        SqlCommand cmd;


        public frm_Show()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
            
        }

        public frm_Show(string query, int permissions, string department)
        {
            this.query = query;
            this.permissions = permissions;
            this.department = department;
            dbh = new SQLDatabaseHandler();
            InitializeComponent();

            #region IfAdmin
            if (permissions == 1)
            {
                btn_Add.Enabled = true;
                btn_Edit.Enabled = true;
                btn_Delete.Enabled = true; 
            }
            #endregion
            #region IfSales
            if (permissions == 2)
            {
                if (department == "TBL_INVOICES")
                {
                    btn_Add.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                }
                else
                {
                    btn_Add.Enabled = true;
                    btn_Edit.Enabled = true;
                    btn_Delete.Enabled = true;
                }
            }
            #endregion
            #region IfFinance
            if (permissions == 3)
            {
                if (department == "TBL_PROJECTS" || department == "TBL_APPOINTMENTS")
                {
                    btn_Add.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                }
                else
                {
                    btn_Add.Enabled = true;
                    btn_Edit.Enabled = true;
                    btn_Delete.Enabled = true;
                }
            }
            #endregion
            #region IfDevelopment
            if (permissions == 4)
            {
                if (department == "TBL_CUSTOMERS" || department == "TBL_INVOICES" || department == "TBL_APPOINTMENTS")
                {
                    btn_Add.Enabled = false;
                    btn_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                }
                else
                {
                    btn_Add.Enabled = false;
                    btn_Edit.Enabled = true;
                    btn_Delete.Enabled = false;
                }
            }
            #endregion
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void frm_Show_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            dbh.FillDataGridView(dgv_Show, query);

            switch (department)
            {
                case "TBL_CUSTOMERS":
                    string[] customers = new string[15] { "Name", "Address1", "HouseNr1", "Zip_Code1", "Place1", "Country1", "Address2", "HouseNr2", "Zip_Code2", "Place2", "Country2", "Phone", "Fax", "Email", "Potential_Prospect" };
                    foreach (var item in customers)
                    {
                        cb_Tabledata.Items.Add(item);
                        cb_Tabledata.SelectedItem = customers[0];
                    }
                    break;
                case "TBL_INVOICES":
                    string[] invoices = new string[10] { "Amount", "Bank_Acc_Nr", "Gross_Rev", "Ledger_Acc_Nr", "Tax_code", "Is_Paid", "Invoice_Sent", "Date", "Id_Project", "Name" };
                    foreach (var item in invoices)
                    {
                        cb_Tabledata.Items.Add(item);
                        cb_Tabledata.SelectedItem = invoices[0];
                    }
                    break;
                case "TBL_APPOINTMENTS":
                    string[] appointments = new string[5] { "Description", "Date", "Next_Action", "Id_Project", "Name" };
                    foreach (var item in appointments)
                    {
                        cb_Tabledata.Items.Add(item);
                        cb_Tabledata.SelectedItem = appointments[0];
                    }
                    break;
                case "TBL_PROJECTS":
                    string[] projects = new string[10] { "Name", "Hardware", "Operating_System", "Maintenance_Contract", "Applications", "Limit", "Is_Done", "Nr_Invoices", "BKR", "Creditworthy"};
                    foreach (var item in projects)
                    {
                        cb_Tabledata.Items.Add(item);
                        cb_Tabledata.SelectedItem = projects[0];
                    }
                    break;
                default:
                    MessageBox.Show("Something went wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
            SetComboBox(cb_Find);
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Form frm_Main = new frm_Main(permissions);
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
        
        private void btn_Search_Click(object sender, EventArgs e)
        {
            string tempQuery = "SELECT * FROM " + department + " WHERE " + cb_Tabledata.Text + " LIKE '%" + textBox1.Text + "%'";

            dbh.FillDataGridView(dgv_Show, tempQuery);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            Form frm_Add = new frm_Add(permissions, GetDepartment(department));
            frm_Add.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_Add);
            this.Close();
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            int selectedIndex = 0;
            int rowID = 0;

            try
            {
                selectedIndex = dgv_Show.SelectedRows[0].Index;
                rowID = int.Parse(dgv_Show[0, selectedIndex].Value.ToString());
                Form frm_edit = new frm_Edit(permissions, GetDepartment(department), rowID);
                frm_edit.StartPosition = FormStartPosition.CenterScreen;
                Program.setForm(frm_edit);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please select a record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetDepartment(string department)
        {
            switch (department)
            {
                case "TBL_CUSTOMERS":
                    return "tbl_Customers";
                case "TBL_INVOICES":
                    return "tbl_Invoices";
                case "TBL_APPOINTMENTS":
                    return "tbl_Appointments";
                case "TBL_PROJECTS":
                    return "tbl_Projects";
                default:
                    return "";
            }
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvr in this.dgv_Show.SelectedRows)
            {
                int selectedIndex = dgv_Show.SelectedRows[0].Index;
                int rowID = 0;
                DialogResult result;
                result = MessageBox.Show("Are you sure you want to delete this record?", "Delete record", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    try
                    {
                        rowID = int.Parse(dgv_Show[0, selectedIndex].Value.ToString());
          
                        cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "DELETE FROM " + department + " WHERE ID=" + rowID + "";
                        cmd.Connection = dbh.getCon();
          
                        dbh.openCon();
          
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch
                        {
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "UPDATE " + ConvertDepartment() + " SET " + ConvertSqlSet() + " WHERE " + ConvertSqlWhere(rowID) + "";
                            cmd2.Connection = dbh.getCon();
                            cmd2.ExecuteNonQuery();
                            cmd.ExecuteNonQuery();
                        }
                        dbh.FillDataGridView(dgv_Show, "SELECT * FROM " + department);
                        dbh.closeCon();
                        MessageBox.Show("Delete was succesful!","Succes!");
                    }
                    
                    catch
                    {
                        MessageBox.Show("Can't delete this record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        } 

        public string ConvertDepartment()
        {
            string temp = "";

            switch (department)
            {
                case "TBL_CUSTOMERS":
                    temp = "TBL_PROJECTS";
                    break;
                case "TBL_PROJECTS":
                    temp = "TBL_INVOICES, TBL_APPOINTMENTS";
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    break;
            }
            return temp;
        }

        public string ConvertSqlSet()
        {
            string temp = "";

            switch (department)
            {
                case "TBL_CUSTOMERS":
                    temp = "Id_customer=NULL";
                    break;
                case "TBL_PROJECTS":
                    temp = "Id_project=NULL";
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    break;
            }
            return temp;
        }

        public string ConvertSqlWhere(int ID)
        {
            string temp = "";

            switch (department)
            {
                case "TBL_CUSTOMERS":
                    temp = "Id_customer=" + ID + "";
                    break;
                case "TBL_PROJECTS":
                    temp = "Id_project="+ ID + "";
                    break;
                default:
                    MessageBox.Show("Something went wrong.");
                    break;
            }
            return temp;
        }

        private void cb_Tabledata_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
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

            DataRow emptyrow = dt.NewRow();
            emptyrow["ID"] = 0;
            emptyrow["Name"] = "";
            dt.Rows.Add(emptyrow);

            combo.DataSource = dt;

            reader.Dispose();
        }

        private string GetFKTable()
        {
            switch (department)
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

        private void btn_FKSearch_Click(object sender, EventArgs e)
        {
            string tempQuery;
            int ID = Convert.ToInt32(cb_Find.SelectedValue);
            if(ID != 0)
            {
                tempQuery = "SELECT * FROM " + department + " WHERE " + SetID() + "=" + cb_Find.SelectedValue + "";
            }
            else
            {
                tempQuery = "SELECT * FROM " + department + "";
            }

            dbh.FillDataGridView(dgv_Show, tempQuery);
        }

        private string SetID()
        {
            switch (department)
            {
                case "TBL_APPOINTMENTS":
                    return "Id_Projects";
                case "TBL_PROJECTS":
                    return "Id_customer";
                case "TBL_INVOICES":
                    return "Id_Projects";
                default:
                    return "";
            }
        }
    }
}