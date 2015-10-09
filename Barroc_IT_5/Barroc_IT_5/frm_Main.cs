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
        private frm_Add frm_Add = new frm_Add();
        private frm_Edit frm_Edit = new frm_Edit();
        private frm_Show frm_Show = new frm_Show();
        private frm_Login frm_Login = new frm_Login();

        public frm_Main()
        {
            InitializeComponent();
        }

        #region Customer Buttons
        private void btn_C_Create_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Add.Show();
        }

        private void btn_C_Edit_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Edit.Show();
        }

        private void btn_C_List_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Show.Show();
        }
        #endregion

        #region Invoice Buttons
        private void btn_I_Create_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Add.Show();
        }

        private void btn_I_Edit_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Edit.Show();
        }

        private void btn_I_List_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Show.Show();
        }
        #endregion

        #region Project Buttons
        private void btn_P_Create_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Add.Show();
        }

        private void btn_P_Edit_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Edit.Show();
        }

        private void btn_P_List_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Show.Show();
        }
        #endregion

        #region Appointment Buttons
        private void btn_A_Create_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Add.Show();
        }

        private void btn_A_Edit_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Edit.Show();
        }

        private void btn_A_List_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Show.Show();
        }
        #endregion

        private void btn_Logout_Click(object sender, EventArgs e)
        {
            Hide();
            frm_Login.Show();
        }
    }
}
