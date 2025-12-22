using Core.Abstract;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public interface IAccountDAL: IBaseRepository<Account>
    {
        List<Account> GetAllWithCustomer();
        Account GetWithCustomer(int id);
    }
}
