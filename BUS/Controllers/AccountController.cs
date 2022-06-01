using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DTO;
using DTO.Entities;

namespace BUS.Controllers
{
    public class AccountController
    {
        private Account GetAcount(string EmployeeId, string Username, string Password, string Address)
        {
            Account acc = new Account();
            acc.EmployeeId = EmployeeId;
            acc.Username = Username;
            acc.Password = Password;
            return acc;
        }

        public List<Account> GetAllAccount(ref string error)
        {
            using (var context = new Context())
            {
                try
                {
                    var account = context.Accounts.ToList();
                    error = "Get All Account Success!!!";
                    return account;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return null;
                }
            }
        }

        public bool GetLogin(string Username, string Password, ref string error)
        {
            using (var context = new Context())
            {
                var acc = context.Accounts.SingleOrDefault(a => a.Username == Username);
                if (acc != null)
                {

                    if (acc.Password == Password)
                    {
                        error = "Login Success!!!";
                        return true;
                    }
                    error = "User Is Not Exits";
                    return false;
                }
                error = "Login failure";
                return false;
            }
        }

        public bool UpdateAccountByEmployeeId(string Id, string NewUser, string NewPassWord, ref string error)
        {
            try
            {
                using (var context = new Context())
                {
                    var account = context.Accounts.SingleOrDefault(a => a.EmployeeId == Id);
                    if (account != null)
                    {
                        account.Username = NewUser;
                        account.Password = NewPassWord;
                        var numOfState = context.SaveChanges();
                        if (numOfState > 0)
                        {
                            error = "Update Account By EmployeeId Success!!!";
                            return true;
                        }
                        error = "Account Has No Change!!!";
                        return false;
                    }
                    error = "Account Is Not Exsit!!!";
                    return false;
                }
            }
            catch
            {
                error = "Something Was Wrong!!!";
                return false;
            }
        }
    }
}
