//#define TIMH
#define TIMG
//#define KEVIN
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Barroc_IT_Groep5
{
    class SQLDatabaseHandler
    {
        string conString;
        SqlConnection con;

        public SQLDatabaseHandler()
        {
#if TIMH
            conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Tim\Documents\Radius\2.1\Barroc-IT-groep-6\Main programma\Database\Barroc_DB.mdf;Integrated Security=True;Connect Timeout=30";
#endif
#if TIMG
            conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=Q:\Documenten\Barroc_DB.mdf;Integrated Security=True;Connect Timeout=30";
#endif
#if KEVIN
            conString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Kevin\Documents\GitHub\Barroc-IT-groep-6\Main programma\Database\Barroc_DB.mdf;Integrated Security=True;Connect Timeout=30";
#endif
            con = new SqlConnection(conString);
        }

        public SqlConnection getCon()
        {
            return con;
        }

        public void openCon()
        {
            con.Open();
        }

        public void closeCon()
        {
            con.Close();
        }


        public void testCon()
        {
            try
            {
                openCon();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(@"Er is iets fout gegaan

" + ex);
            }
            finally
            {
                closeCon();
            }
        }

        public void FillDataGridView(DataGridView dgv, string query)
        {
            openCon();

            SqlDataAdapter adapter = new SqlDataAdapter(query, getCon());
            DataSet dt = new DataSet();
            adapter.Fill(dt);
            dgv.DataSource = dt.Tables[0];

            closeCon();

        }
    }

}
