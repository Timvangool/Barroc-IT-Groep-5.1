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

namespace Barroc_IT_5
{
    public partial class frm_Login : Form
    {
        SQLDatabaseHandler dbh;

        public string uN, pW;
        public bool temp;
        public int permission;

        public frm_Login()
        {
            InitializeComponent();
            dbh = new SQLDatabaseHandler();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            IsLoggedIn(tb_Username.Text, tb_Password.Text);
            SetPermissions();
        }

        public bool IsLoggedIn(string user, string pass)
        {
            user = tb_Username.Text;
            pass = tb_Password.Text;

            string query = "SELECT * FROM TBL_LOGIN WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD";

            dbh.openCon();

            SqlCommand com = new SqlCommand(query, dbh.getCon());
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
                MessageBox.Show("Login Successful.");
                temp = true;
            }
            else if (uN != user || pW != pass)
            {
                MessageBox.Show("Invalid Username and/or Password.");
                temp = false;
            }

            dbh.closeCon();

            return temp;
        }

        public int SetPermissions()
        {
            string query = "SELECT PERMISSIONS FROM TBL_LOGIN WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD";

            SqlCommand com = new SqlCommand(query, dbh.getCon());
            com.Parameters.Add(new SqlParameter("@USERNAME", tb_Username.Text));
            com.Parameters.Add(new SqlParameter("@PASSWORD", tb_Password.Text));

            SqlDataReader reader = com.ExecuteReader();

            while (reader.Read())
            {
                permission = reader.GetInt32(2);
            }

            return permission;
        }

        public int GetPermissions()
        {
            return permission;
        }

    }
}
