namespace Barroc_IT_5
{
    partial class frm_Edit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cb_Customers = new System.Windows.Forms.ComboBox();
            this.btn_SelectCustomer = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_Customers
            // 
            this.cb_Customers.FormattingEnabled = true;
            this.cb_Customers.Location = new System.Drawing.Point(12, 70);
            this.cb_Customers.Name = "cb_Customers";
            this.cb_Customers.Size = new System.Drawing.Size(216, 21);
            this.cb_Customers.TabIndex = 0;
            this.cb_Customers.SelectedIndexChanged += new System.EventHandler(this.cb_Customers_SelectedIndexChanged);
            this.cb_Customers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_Customers_KeyPress);
            this.cb_Customers.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_Customers_MouseUp);
            // 
            // btn_SelectCustomer
            // 
            this.btn_SelectCustomer.Location = new System.Drawing.Point(235, 71);
            this.btn_SelectCustomer.Name = "btn_SelectCustomer";
            this.btn_SelectCustomer.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectCustomer.TabIndex = 1;
            this.btn_SelectCustomer.Text = "Select";
            this.btn_SelectCustomer.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btn_Exit);
            this.panel1.Location = new System.Drawing.Point(-182, -10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 43);
            this.panel1.TabIndex = 10;
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Exit.Location = new System.Drawing.Point(789, 15);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(21, 23);
            this.btn_Exit.TabIndex = 3;
            this.btn_Exit.Text = "X";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // frm_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 387);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_SelectCustomer);
            this.Controls.Add(this.cb_Customers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Edit";
            this.Text = "Barroc_IT";
            this.Load += new System.EventHandler(this.frm_Edit_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Customers;
        private System.Windows.Forms.Button btn_SelectCustomer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Exit;
    }
}