using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using DTO;
using DTO.Entities;
using HotelManage.Forms;

namespace HotelManagement
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        public static void DropDatabase()
        {

            using (var context = new Context())
            {
                if (context.Database.Exists())
                {
                    context.Database.Delete();
                    MessageBox.Show("Deleted!!!");
                }
            }
        }

        public static void CreateDatabase()
        {
            using (var context = new Context())
            {
                if (context.Database.CreateIfNotExists())
                {
                    MessageBox.Show("Created!!!");
                }
                else
                {
                    MessageBox.Show("Failure!!!");
                }
            }
        }
        static void Main()
        {
            /*Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());*/
            Application.Run(new LoginForm());
            //Application.Run(new EmployeeForm());
            try
            {
                DropDatabase();
                CreateDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
