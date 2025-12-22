using Core.Concrete;
using DAL.Abstract;
using DAL.DataBase;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class AccountDAL: BaseRepository<Account, ApplicationDbContext>, IAccountDAL
    {
        public List<Account> GetAllWithCustomer()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = from account in context.Accounts
                             join customer in context.Customers
                             on account.CustomerId equals customer.Id
                             where account.Deleted == 0
                             && customer.Deleted == 0  
                             select new Account
                             {
                                 Id = account.Id,
                                 AccountNumber = account.AccountNumber,
                                 AccountType = account.AccountType,
                                 Balance = account.Balance,
                                 CustomerId = account.CustomerId,
                                 Deleted = account.Deleted,  
                                 Customer = customer
                             };
                return result.ToList();
            }
        }

        public Account GetWithCustomer(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = from account in context.Accounts
                             join customer in context.Customers
                             on account.CustomerId equals customer.Id
                             where account.Id == id
                             && account.Deleted == 0
                             && customer.Deleted == 0  
                             select new Account
                             {
                                 Id = account.Id,
                                 AccountNumber = account.AccountNumber,
                                 AccountType = account.AccountType,
                                 Balance = account.Balance,
                                 CustomerId = account.CustomerId,
                                 Deleted = account.Deleted,  
                                 Customer = customer
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
