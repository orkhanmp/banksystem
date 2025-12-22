using Core.Concrete;
using DAL.Abstract;
using DAL.DataBase;
using Entities.TableModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class CustomerDAL: BaseRepository<Customer, ApplicationDbContext>, ICustomerDAL
    {
        public List<Customer> GetAllWithAccounts()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = from Customer in context.Customers
                             where Customer.Deleted == 0
                             select new Customer
                             {
                                 Id = Customer.Id,
                                 FirstName = Customer.FirstName,
                                 LastName = Customer.LastName,
                                 Age = Customer.Age,
                                 City = Customer.City,
                                 Deleted = Customer.Deleted,
                                 Accounts = (from account in context.Accounts
                                              where account.CustomerId == Customer.Id && account.Deleted == 0
                                             select account).ToList()
                             };

                return result.ToList();
            }
        }

        public Customer GetWithAccounts(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var result = from Customer in context.Customers
                             where Customer.Id == id
                             && Customer.Deleted == 0
                             select new Customer
                             {
                                 Id = Customer.Id,
                                 FirstName = Customer.FirstName,
                                 LastName = Customer.LastName,
                                 Age = Customer.Age,
                                 City = Customer.City,
                                 Deleted = Customer.Deleted,
                                 Accounts = (from account in context.Accounts
                                             where account.CustomerId == Customer.Id && account.Deleted == 0
                                             select account).ToList()
                             };

                return result.FirstOrDefault();
            }
        }
    }
}
