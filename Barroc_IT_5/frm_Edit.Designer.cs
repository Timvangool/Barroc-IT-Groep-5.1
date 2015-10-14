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
            this.SuspendLayout();
            // 
            // cb_Customers
            // 
            this.cb_Customers.FormattingEnabled = true;
            this.cb_Customers.Location = new System.Drawing.Point(12, 12);
            this.cb_Customers.Name = "cb_Customers";
            this.cb_Customers.Size = new System.Drawing.Size(216, 21);
            this.cb_Customers.TabIndex = 0;
            // 
            // btn_SelectCustomer
            // 
            this.btn_SelectCustomer.Location = new System.Drawing.Point(235, 13);
            this.btn_SelectCustomer.Name = "btn_SelectCustomer";
            this.btn_SelectCustomer.Size = new System.Drawing.Size(75, 23);
            this.btn_SelectCustomer.TabIndex = 1;
            this.btn_SelectCustomer.Text = "Select";
            this.btn_SelectCustomer.UseVisualStyleBackColor = true;
            // 
            // frm_Edit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(325, 47);
            this.Controls.Add(this.btn_SelectCustomer);
            this.Controls.Add(this.cb_Customers);
            this.Name = "frm_Edit";
            this.Text = "Barroc_IT";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cb_Customers;
        private System.Windows.Forms.Button btn_SelectCustomer;
    }
}