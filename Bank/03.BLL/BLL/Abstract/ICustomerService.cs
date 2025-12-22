using Core.Result.Abstract;
using Entities.DTOs.CustomerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Abstract
{
    public interface ICustomerService
    {
        IResult Add(CustomerAddDTO customerAddDTO);

        IResult Update(CustomerUpdateDTO customerUpdateDTO);
        IResult Delete(int id);

        IDataResult<List<CustomerListDTO>> GetAll();
        IDataResult<CustomerListDTO> Get(int id);
    }
}
