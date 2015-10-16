using Barroc_IT_5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barroc_IT_Groep5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static Form nextForm;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            nextForm = new frm_Login();
            nextForm.StartPosition = FormStartPosition.CenterScreen;

            while (!nextForm.IsDisposed)
            {
                Application.Run(nextForm);
            }
        }    

        public static void setForm(Form targetForm)
        {
            nextForm = targetForm;
        }
    }
}
