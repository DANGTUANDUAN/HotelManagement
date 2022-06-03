using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using BUS.Controllers;
using DTO.Entities;
using DTO;

namespace HotelManage.Forms
{
    public partial class LoginForm : Form
    {
        AccountController ac = null;
        public LoginForm()
        {
            InitializeComponent();
            this.ac = new AccountController();
        }

        private void bntLogin_Click(object sender, EventArgs e)
        {
            try
            {

                string user = txtUserName.Text;
                string password = txtPassWord.Text;

                string error = "";
                bool iscreated = ac.GetLogin(user, password, ref error);
                if (iscreated)
                {
                    this.Hide();
                    HomeForm homeForm = new HomeForm();
                    homeForm.Show();
                }
                else
                {
                    MessageBox.Show("đăng nhập thất bại");
                }
            }
            catch
            {
                MessageBox.Show("Đã xảy ra lỗi vui lòng kiểm tra và thử lại");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }
    }
}
