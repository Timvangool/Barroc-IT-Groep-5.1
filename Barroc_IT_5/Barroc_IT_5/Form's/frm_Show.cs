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

            string[] invoices = new string[9] {"Bank_Acc_Nr", "Price", "Gross_Rev", "Ledger_Acc_Nr", "Tax_code", "Id_Project", "Is_Payed", "Date", "Invoice_Send"};

            string[] appointments = new string[4] { "Description", "Date", "Next_Action", "Id_Project" };

            string[] customers = new string[14] { "Name", "Address1", "HouseNr1", "Zip_Code1", "Place1", "Country", "Address2", "HouseNr2", "Zip_Code2", "Place2", "Fax", "Email", "Potential_Prospect", "Phone" };

            string[] projects = new string[10] {"Naam","Hardware","Operating_System","Maintenance_Contract","Applications","Limit","Nr_Invoices","Is_Done","BKR","Credit_Worthy" };

            switch (department)
            {
                case "TBL_CUSTOMERS":
                    foreach (var item in customers)
                    {
                        cb_Tabledata.Items.Add(item);
                    }
                    break;
                case "TBL_INVOICES":
                    foreach (var item in invoices)
                    {
                        cb_Tabledata.Items.Add(item);
                    }
                    break;
                case "TBL_APPOINTMENTS":
                    foreach (var item in appointments)
                    {
                        cb_Tabledata.Items.Add(item);
                    }
                    break;
                case "TBL_PROJECTS":
                    foreach (var item in projects)
                    {
                        cb_Tabledata.Items.Add(item);
                    }
                    break;
                default:
                    MessageBox.Show("Something went wrong :C");
                    break;
            }
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
            int selectedIndex = dgv_Show.SelectedRows[0].Index;
            int rowID = int.Parse(dgv_Show[0, selectedIndex].Value.ToString());

            Form frm_edit = new frm_Edit(permissions, GetDepartment(department), rowID);
            frm_edit.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frm_edit);
            this.Close();
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
                int rowID = int.Parse(dgv_Show[0, selectedIndex].Value.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM " + department + " WHERE ID="  + rowID + "";
                cmd.Connection = dbh.getCon();

                dbh.openCon();
                cmd.ExecuteNonQuery();
                dbh.FillDataGridView(dgv_Show, "SELECT * FROM " + department);
                dbh.closeCon();          
            }
        }

        public string ConvertDepartment()
        {
            string temp = "";

            switch (department)
            {
                case "TBL_CUSTOMERS":
                    temp = "ID_CUSTOMER";
                    break;
                case "TBL_INVOICES":
                    temp = "ID_INVOICE";
                    break;
                case "TBL_APPOINTMENTS":
                    temp = "ID_APPOINTMENT";
                    break;
                case "TBL_PROJECTS":
                    temp = "ID_PROJECT";
                    break;
                default:
                    MessageBox.Show("Something went wrong :C");
                    break;
            }
            return temp;
        }

        private void btn_Print_Click(object sender, EventArgs e)
        {

        }
    }
}
