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
    public partial class frm_Show : Form
    {
        frm_Login frmLogin;
       
        public frm_Show()
        {
            InitializeComponent();
            frmLogin = new frm_Login();
        }

        private void frm_Show_Load(object sender, EventArgs e)
        {
            switch (frmLogin.GetPermissions())
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    break;
            }
        }
    }
}
