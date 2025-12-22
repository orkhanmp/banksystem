using Core.Abstract;
using Entities.TableModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface ICustomerDAL: IBaseRepository<Customer>
    {
        List<Customer> GetAllWithAccounts();
        Customer GetWithAccounts(int id);
    }
}
