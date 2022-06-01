using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;
using DTO.Entities;
namespace BUS.Controllers
{
    public class EmployeeController
    {
        
        private Employee GetEmployee(string Id, string EmployeeName, string Phonenumber, string Address)
        {
            Employee employee = new Employee();
            employee.Id = Id;
            employee.Name = EmployeeName;
            employee.Phonenumber = Phonenumber;
            employee.Address = Address;
            return employee;
        }

        private Account GetAccount(string EmployeeId, string Username, string Password)
        {
            Account account = new Account();
            account.EmployeeId = EmployeeId;
            account.Username = Username;
            account.Password = Password;
            return account;
        }

        public List<Employee> GetAllEmployee(ref string error)
        {
            using (var context = new Context())
            {
                try
                {
                    var employee = context.Employees.ToList();
                    error = "Get All Employee Success!!!";
                    return employee;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return null;
                }
            }
        }

        public Employee GetEmployeeId(string Id, ref string error)
        {
            try
            {
                using (var context = new Context())
                {
                    var employee = context.Employees.Where(emp => emp.Id == Id).FirstOrDefault();
                    if (employee != null)
                    {
                        error = "Get Employee By Id Success!!!";
                    }
                    error = "Employee Is Not Exsit!!!";
                    return employee;
                }
            }
            catch
            {
                error = "Get Employee By Id Failure!!!";
                return null;
            }
        }


        public bool AddNewEmployee(string EmployeeId,string EmployeeName,string PhoneNumber,string Address,string UserName,string PassWord,ref string error)
        {
            try
            {
                using (var context = new Context())
                {
                    // Check service
                    var employee = this.GetEmployee(EmployeeId, EmployeeName, PhoneNumber, Address);
                    var acc = this.GetAccount(EmployeeId,UserName,PassWord);
                    if (employee != null)
                    {
                        context.Employees.Add(employee);
                        var numOfState = context.SaveChanges();
                        error = $"Insert {numOfState} Success!!!";
                        context.Accounts.Add(acc);
                        var state = context.SaveChanges();
                        error = $"Insert {state} Success!!!";
                        return true;
                    }
                    error = "Employee invalid!!!";
                    return false;
                }
            }
            catch
            {
                error = "Add New Employee Failure!!!";
                return false;
            }
        }

        public bool RemoveEmployeeById(string Id, ref string error)
        {
            try
            {
                using (var context = new Context())
                {
                    var employee = context.Employees.
                        SingleOrDefault(e => e.Id == Id);
                    if (employee != null)
                    {
                        var account = context.Accounts.Find(Id);
                        if(account != null)
                        {
                            context.Accounts.Remove(account);
                            context.Employees.Remove(employee);
                            var numOfState = context.SaveChanges();
                            if (numOfState > 0)
                            {
                                error = "Remove Employee By Id Success!!!";
                                return true;
                            }
                            error = "Remove Employee By Id Failure!!!";
                            return false;
                        }
                        error = "Account Not EmployeeId!!";
                        return false;
                    }
                    error = "EmployeeId Is Not Exist!!!";
                    return false;
                }
            }
            catch
            {
                error = "Something was wrong!!!";
                return false;
            }
        }

        public bool UpdateEmployeeById(string Id,string NewEmployeeName,string NewPhoneNumber,string NewAddress,ref string error)
        {
            try
            {
                using (var context = new Context())
                {
                    var employee = context.Employees.SingleOrDefault(e => e.Id == Id);
                    if (employee != null)
                    {
                        employee.Name = NewEmployeeName;
                        employee.Phonenumber = NewPhoneNumber;
                        employee.Address = NewAddress;
                        var numOfState = context.SaveChanges();
                        if (numOfState > 0)
                        {
                            error = "Update Employee By Id Success!!!";
                            return true;
                        }
                        error = "Employee Has No Change!!!";
                        return false;
                    }
                    error = "Employee Is Not Exsit!!!";
                    return false;
                }
            }
            catch
            {
                error = "Something Was Wrong!!!";
                return false;
            }
        }
        
        public bool search(string textSearch)
        {
            try
            {
                using(var context = new Context())
                {
                    
                }
            }
            catch
            {

            }
            return false;
        }
    }
}
