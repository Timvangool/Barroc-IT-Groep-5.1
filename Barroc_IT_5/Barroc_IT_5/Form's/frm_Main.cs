using Barroc_IT_Groep5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barroc_IT_5
{
    public partial class frm_Main : Form
    {
        public int permissions;
        string department;
        string table;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public frm_Main()
        {
            InitializeComponent();
        }

        public frm_Main(int permissions)
        {
            this.permissions = permissions;
            InitializeComponent();

            #region IfAdmin
            if (permissions == 1)
            {
                btn_C_Create.Visible = true;
                btn_C_Edit.Visible = true;
                btn_C_List.Visible = true;
                btn_P_Create.Visible = true;
                btn_P_Edit.Visible = true;
                btn_P_List.Visible = true;
                btn_I_Create.Visible = true;
                btn_I_Edit.Visible = true;
                btn_I_List.Visible = true;
                btn_A_Create.Visible = true;
                btn_A_Edit.Visible = true;
                btn_A_List.Visible = true;
            }
            #endregion
            #region IfSales
            if (permissions == 2)
            {
                btn_C_Create.Visible = true;
                btn_C_Edit.Visible = true;
                btn_C_List.Visible = true;
                btn_P_Create.Visible = true;
                btn_P_Edit.Visible = true;
                btn_P_List.Visible = true;
                btn_I_Create.Visible = false;
                btn_I_Edit.Visible = false;
                btn_I_List.Visible = true;
                btn_A_Create.Visible = true;
                btn_A_Edit.Visible = true;
                btn_A_List.Visible = true;
            }
            #endregion
            #region IfFinance
            if (permissions == 3)
            {
                btn_C_Create.Visible = true;
                btn_C_Edit.Visible = true;
                btn_C_List.Visible = true;
                btn_P_Create.Visible = false;
                btn_P_Edit.Visible = false;
                btn_P_List.Visible = true;
                btn_I_Create.Visible = true;
                btn_I_Edit.Visible = true;
                btn_I_List.Visible = true;
                btn_A_Create.Visible = false;
                btn_A_Edit.Visible = false;
                btn_A_List.Visible = true;
            }
            #endregion
            #region IfDevelopment
            if (permissions == 4)
            {
                btn_C_Create.Visible = false;
                btn_C_Edit.Visible = false;
                btn_C_List.Visible = true;
                btn_P_Create.Visible = false;
                btn_P_Edit.Visible = true;
                btn_P_List.Visible = true;
                btn_I_Create.Visible = false;
                btn_I_Edit.Visible = false;
                btn_I_List.Visible = true;
                btn_A_Create.Visible = false;
                btn_A_Edit.Visible = false;
                btn_A_List.Visible = false;

            }
            #endregion
        }

        #region Customer Buttons
        private void btn_C_Create_Click(object sender, EventArgs e)
        {
            table = "tbl_Customers";
            Form frmAdd = new frm_Add(permissions, table);
            frmAdd.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_C_Edit_Click(object sender, EventArgs e)
        {
            table = "tbl_Customers";
            Form frmEdit = new frm_Edit(permissions, table);
            frmEdit.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_C_List_Click(object sender, EventArgs e)
        {
            department = "TBL_CUSTOMERS";
            string query = "SELECT * FROM tbl_Customers";
            Form frmShow = new frm_Show(query, permissions, department);
            frmShow.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        #region Invoice Buttons
        private void btn_I_Create_Click(object sender, EventArgs e)
        {
            table = "tbl_Invoices";
            Form frmAdd = new frm_Add(permissions);
            frmAdd.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_I_Edit_Click(object sender, EventArgs e)
        {
            table = "tbl_Invoices";
            Form frmEdit = new frm_Edit(permissions, table);
            frmEdit.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_I_List_Click(object sender, EventArgs e)
        {
            department = "TBL_INVOICES";
            string query = "SELECT * FROM tbl_Invoices";
            Form frmShow = new frm_Show(query, permissions, department);
            frmShow.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        #region Project Buttons
        private void btn_P_Create_Click(object sender, EventArgs e)
        {
            table = "tbl_Projects";
            Form frmAdd = new frm_Add(permissions, table);
            frmAdd.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_P_Edit_Click(object sender, EventArgs e)
        {
            table = "tbl_Projects";
            Form frmEdit = new frm_Edit(permissions, table);
            frmEdit.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_P_List_Click(object sender, EventArgs e)
        {
            department = "TBL_PROJECTS";
            string query = "SELECT * FROM tbl_Projects";
            Form frmShow = new frm_Show(query, permissions, department);
            frmShow.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        #region Appointment Buttons
        private void btn_A_Create_Click(object sender, EventArgs e)
        {
            table = "tbl_Appointments";
            Form frmAdd = new frm_Add(permissions, table);
            frmAdd.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_A_Edit_Click(object sender, EventArgs e)
        {
            table = "tbl_Appointments";
            Form frmEdit = new frm_Edit(permissions, table);
            frmEdit.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_A_List_Click(object sender, EventArgs e)
        {
            department = "TBL_APPOINTMENTS";
            string query = "SELECT * FROM tbl_Appointments";
            Form frmShow = new frm_Show(query, permissions, department);
            frmShow.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Form frmLogin = new frm_Login();
            frmLogin.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmLogin);
            this.Close();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Form frmLogin = new frm_Login();
            frmLogin.StartPosition = FormStartPosition.CenterScreen;
            Program.setForm(frmLogin);
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}
