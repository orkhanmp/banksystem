using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using Entities.DTOs.AccountDTOs;
using Entities.DTOs.CustomerDTOs;
using Entities.TableModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDAL _customerDAL;
        private readonly IMapper _mapper;

        public CustomerManager(ICustomerDAL customerDAL, IMapper mapper)
        {
            _customerDAL = customerDAL;
            _mapper = mapper;
        }
        public IResult Add(CustomerAddDTO customerAddDTO)
        {
            var mapperCustomer = _mapper.Map<Customer>(customerAddDTO);
            var validateValidator = new CustomerValidation();
            var validationResult = validateValidator.Validate(mapperCustomer);

            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            _customerDAL.Add(mapperCustomer);
            return new SuccessResult("Added Successfully");
        }

        public IResult Delete(int id)
        {
            var customerGet = _customerDAL.Get(x => x.Id == id);

            if (customerGet is not null)
            {
                customerGet.Deleted = id;
                _customerDAL.Update(customerGet);
                return new SuccessResult("Deleted Successfully");
            }

            return new ErrorResult("Customer not found");
        }

        public IDataResult<CustomerListDTO> Get(int id)
        {
            var customer = _customerDAL.GetWithAccounts(id);
        
            return new SuccessDataResult<CustomerListDTO>(new CustomerListDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Age = customer.Age,
                City = customer.City,
                AccountCount = customer.Accounts?.Where(a => a.Deleted == 0).Count() ?? 0,
                Accounts = customer.Accounts ? .Where(a => a.Deleted == 0)
                                             .Select(a => new AccountListDTO
                                             {
                                                 Id = a.Id,
                                                 AccountNumber = a.AccountNumber,
                                                 AccountType = a.AccountType,
                                                 Balance = a.Balance,
                                                 CustomerId = a.CustomerId,
                                                 
                                             }).ToList() 
            });
        }

        public IDataResult<List<CustomerListDTO>> GetAll()
        {
            var customers = _customerDAL.GetAllWithAccounts(); 

            var result = customers.Select(c => new CustomerListDTO
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Age = c.Age,
                City = c.City,
                AccountCount = c.Accounts?.Count ?? 0  
            }).ToList();

            return new SuccessDataResult<List<CustomerListDTO>>(result);
        }

        public IResult Update(CustomerUpdateDTO customerUpdateDTO)
        {
            var customerMapper = _mapper.Map<Customer>(customerUpdateDTO);
            var validateValidator = new CustomerValidation();
            var validationResults = validateValidator.Validate(customerMapper);

            if (!validationResults.IsValid)
                return new ErrorResult(validationResults.Errors.FluentErrorString());

            _customerDAL.Update(customerMapper);
            return new SuccessResult("Updated Successfully");
        }
    }
}
