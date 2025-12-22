using Core.Result.Abstract;
using Entities.DTOs.AccountDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface IAccountService
    {
        IResult Add(AccountAddDTO accountAddDTO);

        IResult Update(AccountUpdateDTO accountUpdateDTO);
        IResult Delete(int id);

        IDataResult<List<AccountListDTO>> GetAll();
        IDataResult<AccountListDTO> Get(int id);
    }
}
