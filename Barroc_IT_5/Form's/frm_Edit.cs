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
    public partial class frm_Edit : Form
    {
        public int permissions;
        public frm_Edit()
        {
            InitializeComponent();
        }

        public frm_Edit(int permissions)
        {
            this.permissions = permissions;
            InitializeComponent();
        }
    }
}
