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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;

namespace Barroc_IT_5
{
    public partial class frm_Login : Form
    {
        //Code to move to window, since we're using a custom bar.
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        SQLDatabaseHandler dbh;

        public string uN, pW;
        public int permissions;

        public frm_Login()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
        }

        public frm_Login(int permissions)
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
            this.permissions = permissions;
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            LogIn(tb_Username.Text, tb_Password.Text);
        }

        public void LogIn(string user, string pass)
        {
            user = tb_Username.Text;
            pass = tb_Password.Text;

            string query = "SELECT * FROM TBL_LOGIN WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD";

            dbh.OpenCon();

            SqlCommand com = new SqlCommand(query, dbh.GetCon());
            com.Parameters.Add(new SqlParameter("@USERNAME", user));
            com.Parameters.Add(new SqlParameter("@PASSWORD", pass));

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                uN = reader.GetString(0);
                pW = reader.GetString(1);
            }

            if (uN == user && pW == pass)
            {
                MessageBox.Show("Login Successful.", "Succes!");

                switch (uN)
                {
                    case "Admin":
                        permissions = 1;
                        break;
                    case "Sales":
                        permissions = 2;
                        break;
                    case "Development":
                        permissions = 4;
                        break;
                    case "Finance":
                        permissions = 3;
                        break;
                    default:
                        MessageBox.Show("Er is iets fout gegaan bij 'IsLoggedIn()'");
                        permissions = 0;
                        break;
                }

                Form frmMain = new frm_Main(permissions);
                frmMain.StartPosition = FormStartPosition.CenterScreen;
                Program.setForm(frmMain);
                this.Close();
            }
            else if (uN != user || pW != pass)
            {
                MessageBox.Show("Invalid Username and/or Password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
  
            }
            dbh.CloseCon();
        }

        private void tb_Password_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Login_Click(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Login_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        //Continuation of the code to move the window.
        private void lb_Login_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
