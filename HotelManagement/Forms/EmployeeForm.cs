using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using HotelManage.Resources.Utils;
using BUS.Controllers;
using DTO.Entities;
using DTO;

namespace HotelManage.Forms
{
    public partial class EmployeeForm : Form
    {
        AccountController ac = null;
        EmployeeController emp = null;
        public EmployeeForm()
        {
            InitializeComponent();
            this.emp = new EmployeeController();
            this.ac = new AccountController();
        }

        private DataTable GetDataTable(List<Employee> employees, List<Account> accounts)
        {
            DataTable table = new DataTable();
            table.Columns.Add("EmpID", typeof(string));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("SDT", typeof(string));
            table.Columns.Add("UserName", typeof(string));
            table.Columns.Add("PassWord", typeof(string));


            foreach (var employee in employees)
            {
                foreach(var account in accounts)
                {
                    if (employee.Id == account.EmployeeId)
                    {
                        table.Rows.Add(employee.Id, employee.Name, employee.Address, employee.Phonenumber, account.Username, account.Password);
                    }
                }
            }
            return table;
        }

        private void FillDataGrid()
        {
            string error = "";
            var employee = emp.GetAllEmployee(ref error);
            var account = ac.GetAllAccount(ref error);

            // Return databale 
            var dt = this.GetDataTable(employee,account);
            this.dataGridEmployee.DataSource = dt;
            this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["EmpID"], ListSortDirection.Ascending);
            this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["Name"], ListSortDirection.Ascending);
            this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["Address"], ListSortDirection.Ascending);
            this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["SDT"], ListSortDirection.Ascending);
            this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["UserName"], ListSortDirection.Ascending);
            this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["PassWord"], ListSortDirection.Ascending);
        }

        private void ClearAllTextBox()
        {
            txtEmpId.ResetText();
            txtEmpName.ResetText();
            txtEmpAddress.ResetText();
            txtEmpPhone.ResetText();
            txtEmpAccUserN.ResetText();
            txtEmpAccPassW.ResetText();
            txtEmpId.Focus();
        }

        private string GetValueOfCellDataGrid(int rowIndex, int columnIndex)
        {
            string value = dataGridEmployee.Rows[rowIndex].Cells[columnIndex].Value.ToString();
            return value;
        }

        private void DataGridEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridEmployee.CurrentCell.RowIndex;
            // Get Value of current row
            string Id = GetValueOfCellDataGrid(rowIndex, 0);
            string EmployeeName = GetValueOfCellDataGrid(rowIndex, 1);
            string Address = GetValueOfCellDataGrid(rowIndex, 2);
            string PhoneNumber = GetValueOfCellDataGrid(rowIndex, 3);
            string User = GetValueOfCellDataGrid(rowIndex, 4);
            string PassWord = GetValueOfCellDataGrid(rowIndex, 5);

            this.txtEmpId.Text = Id;
            this.txtEmpName.Text = EmployeeName;
            this.txtEmpAddress.Text = Address;
            this.txtEmpPhone.Text = PhoneNumber;
            this.txtEmpAccUserN.Text = User;
            this.txtEmpAccPassW.Text = PassWord;
        }

        private void dataGridEmployee_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = dataGridEmployee.CurrentCell.RowIndex;
            // Get Value of current row
            string Id = GetValueOfCellDataGrid(rowIndex, 0);
            string EmployeeName = GetValueOfCellDataGrid(rowIndex, 1);
            string Address = GetValueOfCellDataGrid(rowIndex, 2);
            string PhoneNumber = GetValueOfCellDataGrid(rowIndex, 3);
            string User = GetValueOfCellDataGrid(rowIndex, 4);
            string PassWord = GetValueOfCellDataGrid(rowIndex, 5);

            this.txtEmpId.Text = Id;
            this.txtEmpName.Text = EmployeeName;
            this.txtEmpAddress.Text = Address;
            this.txtEmpPhone.Text = PhoneNumber;
            this.txtEmpAccUserN.Text = User;
            this.txtEmpAccPassW.Text = PassWord;
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            this.ClearAllTextBox();
            this.FillDataGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = txtEmpId.Text;
                string EmployeeName = txtEmpName.Text;
                string Address = txtEmpAddress.Text;
                string PhoneNumber = txtEmpPhone.Text;
                string User = txtEmpAccUserN.Text;
                string PassWord = txtEmpAccPassW.Text;

                string error = "";
                bool isCreated = emp.AddNewEmployee(Id, EmployeeName, PhoneNumber, Address, User, PassWord, ref error);
                if (isCreated)
                {
                    // Refresh Data
                    this.FillDataGrid();

                    // Clear Text Box If Insert success
                    this.ClearAllTextBox();
                    MessageBox.Show("Thêm nhân viên thành công");
                }
                else
                {
                    MessageBox.Show("Thêm Không Thành Công");
                }
            }
            catch
            {
                MessageBox.Show("Đã Xảy Ra Lỗi, Vui Lòng Thử Lại");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = dataGridEmployee.CurrentCell.RowIndex;
            string Id = dataGridEmployee.Rows[rowIndex].Cells[0].Value.ToString();

            string error = "";
            bool isDeleted = emp.RemoveEmployeeById(Id, ref error);
            if (isDeleted)
            {
                this.FillDataGrid();
                MessageBox.Show("Xóa nhân viên thành công");
            }
            else
            {
                MessageBox.Show(error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                string Id = txtEmpId.Text;
                string EmployeeName = txtEmpName.Text;
                string Address = txtEmpAddress.Text;
                string PhoneNumber = txtEmpPhone.Text;
                string User = txtEmpAccUserN.Text;
                string PassWord = txtEmpAccPassW.Text;
                string error = "";
                bool isUpdated = emp.UpdateEmployeeById(Id, EmployeeName, PhoneNumber, Address, ref error);
                bool isAccUpdated = ac.UpdateAccountByEmployeeId(Id, User, PassWord, ref error);
                if (isUpdated || isAccUpdated)
                {
                    this.ClearAllTextBox();
                    this.FillDataGrid();
                }
                else
                {
                    MessageBox.Show(error);
                }
            }
            catch
            {
                MessageBox.Show("Update Employee failure!!!");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string NameSearch = txtSearch.Text;

                string error = "";
                var employees = emp.GetAllEmployeeByName(NameSearch, ref error);

                if (employees.Count() > 0)
                {
                    var accounts = ac.GetAllAccount(ref error);
                    DataTable table = new DataTable();
                    table.Columns.Add("EmpID", typeof(string));
                    table.Columns.Add("Name", typeof(string));
                    table.Columns.Add("Address", typeof(string));
                    table.Columns.Add("SDT", typeof(string));
                    table.Columns.Add("UserName", typeof(string));
                    table.Columns.Add("PassWord", typeof(string));


                    foreach (var employee in employees)
                    {
                        foreach (var account in accounts)
                        {
                            if (employee.Id == account.EmployeeId)
                            {
                                table.Rows.Add(employee.Id, employee.Name, employee.Address, employee.Phonenumber, account.Username, account.Password);
                            }
                        }
                    }

                    this.dataGridEmployee.DataSource = table;
                    this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["EmpID"], ListSortDirection.Ascending);
                    this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["Name"], ListSortDirection.Ascending);
                    this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["Address"], ListSortDirection.Ascending);
                    this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["SDT"], ListSortDirection.Ascending);
                    this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["UserName"], ListSortDirection.Ascending);
                    this.dataGridEmployee.Sort(this.dataGridEmployee.Columns["PassWord"], ListSortDirection.Ascending);
                    // Return databale
                }
                else
                {
                    MessageBox.Show("Employee không tồn tại");
                }
                txtSearch.ResetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearAllTextBox();
            FillDataGrid();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            HomeForm homeForm = new HomeForm();
            homeForm.Show();
            this.Close();
        }
    }
}
