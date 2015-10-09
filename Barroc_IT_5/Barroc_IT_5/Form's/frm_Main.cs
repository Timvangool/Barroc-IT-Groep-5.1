using Barroc_IT_Groep5;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barroc_IT_5
{
    public partial class frm_Main : Form
    {
        
        public frm_Main()
        {
            InitializeComponent();

        }

        #region Customer Buttons
        private void btn_C_Create_Click(object sender, EventArgs e)
        {
            Form frmAdd = new frm_Add();
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_C_Edit_Click(object sender, EventArgs e)
        {
            Form frmEdit = new frm_Edit();
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_C_List_Click(object sender, EventArgs e)
        {
            Form frmShow = new frm_Show();
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        #region Invoice Buttons
        private void btn_I_Create_Click(object sender, EventArgs e)
        {
            Form frmAdd = new frm_Add();
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_I_Edit_Click(object sender, EventArgs e)
        {
            Form frmEdit = new frm_Edit();
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_I_List_Click(object sender, EventArgs e)
        {
            Form frmShow = new frm_Show();
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        #region Project Buttons
        private void btn_P_Create_Click(object sender, EventArgs e)
        {
            Form frmAdd = new frm_Add();
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_P_Edit_Click(object sender, EventArgs e)
        {
             Form frmEdit = new frm_Edit();
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_P_List_Click(object sender, EventArgs e)
        {
            Form frmShow = new frm_Show();
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        #region Appointment Buttons
        private void btn_A_Create_Click(object sender, EventArgs e)
        {
            Form frmAdd = new frm_Add();
            Program.setForm(frmAdd);
            this.Close();
        }

        private void btn_A_Edit_Click(object sender, EventArgs e)
        {
            Form frmEdit = new frm_Edit();
            Program.setForm(frmEdit);
            this.Close();
        }

        private void btn_A_List_Click(object sender, EventArgs e)
        {
            Form frmShow = new frm_Show();
            Program.setForm(frmShow);
            this.Close();
        }
        #endregion

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Form frmLogin = new frm_Login();
            Program.setForm(frmLogin);
            this.Close();
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {

        }
    }
}
