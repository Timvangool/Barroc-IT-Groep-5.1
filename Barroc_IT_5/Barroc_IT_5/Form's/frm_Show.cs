﻿using System;
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

        string query;

        public frm_Show()
        {
            InitializeComponent();
            
        }

        public frm_Show(string query)
        {
            this.query = query;
            InitializeComponent();
            
        }
    }
}
