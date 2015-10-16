namespace Barroc_IT_5
{
    partial class frm_Add
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
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Back = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.cb_Departments = new System.Windows.Forms.ComboBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(12, 447);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 0;
            this.btn_Add.Text = "Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            // 
            // btn_Back
            // 
            this.btn_Back.Location = new System.Drawing.Point(562, 447);
            this.btn_Back.Name = "btn_Back";
            this.btn_Back.Size = new System.Drawing.Size(75, 23);
            this.btn_Back.TabIndex = 1;
            this.btn_Back.Text = "Back";
            this.btn_Back.UseVisualStyleBackColor = true;
            this.btn_Back.Click += new System.EventHandler(this.btn_Back_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel1.Controls.Add(this.btn_Exit);
            this.panel1.Location = new System.Drawing.Point(-191, -11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 43);
            this.panel1.TabIndex = 9;
            // 
            // btn_Exit
            // 
            this.btn_Exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btn_Exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Exit.Location = new System.Drawing.Point(813, 15);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(21, 23);
            this.btn_Exit.TabIndex = 3;
            this.btn_Exit.Text = "X";
            this.btn_Exit.UseVisualStyleBackColor = false;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // cb_Departments
            // 
            this.cb_Departments.FormattingEnabled = true;
            this.cb_Departments.Items.AddRange(new object[] {
            "Customer",
            "Invoice",
            "Project",
            "Appointment"});
            this.cb_Departments.Location = new System.Drawing.Point(12, 39);
            this.cb_Departments.Name = "cb_Departments";
            this.cb_Departments.Size = new System.Drawing.Size(155, 21);
            this.cb_Departments.TabIndex = 10;
            this.cb_Departments.SelectedIndexChanged += new System.EventHandler(this.cb_Department_SelectedIndexChanged);
            this.cb_Departments.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cb_Departments_KeyPress);
            this.cb_Departments.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cb_Departments_MouseUp);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Location = new System.Drawing.Point(174, 39);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
            this.btn_Refresh.TabIndex = 11;
            this.btn_Refresh.Text = "Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.button1_Click);
            // 
            // frm_Add
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 482);
            this.Controls.Add(this.btn_Refresh);
            this.Controls.Add(this.cb_Departments);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Back);
            this.Controls.Add(this.btn_Add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "frm_Add";
            this.Text = "Add";
            this.Load += new System.EventHandler(this.frm_Add_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Back;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.ComboBox cb_Departments;
        private System.Windows.Forms.Button btn_Refresh;
    }
}