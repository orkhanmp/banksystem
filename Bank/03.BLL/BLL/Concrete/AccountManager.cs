using AutoMapper;
using BLL.Abstract;
using BLL.Extension;
using BLL.Validation;
using Core.Result.Abstract;
using Core.Result.Concrete;
using DAL.Abstract;
using Entities.DTOs.AccountDTOs;
using Entities.TableModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountDAL _accountDAL;
        private readonly IMapper _mapper;

        public AccountManager(IAccountDAL accountDAL, IMapper mapper)
        {
            _accountDAL = accountDAL;
            _mapper = mapper;
        }
        public IResult Add(AccountAddDTO accountAddDTO)
        {
            var mapperAccount = _mapper.Map<Account>(accountAddDTO);
            var validateValidator = new AccountValidation();
            var validationResult = validateValidator.Validate(mapperAccount);

            if (!validationResult.IsValid)
                return new ErrorResult(validationResult.Errors.FluentErrorString());

            _accountDAL.Add(mapperAccount);
            return new SuccessResult("Added Successfully");
        }

        public IResult Delete(int id)
        {
            var accountGet = _accountDAL.Get(x => x.Id == id);

            if (accountGet != null)
            {
                accountGet.Deleted = id;

                _accountDAL.Update(accountGet);
                return new SuccessResult("Deleted Successfully");
            }

            return new ErrorResult("Account not found");
        }

        public IDataResult<AccountListDTO> Get(int id)
        {
            var accountById = _accountDAL.GetAll(x => x.Deleted == 0 && x.Id == id).FirstOrDefault();
            return new SuccessDataResult<AccountListDTO>(_mapper.Map<AccountListDTO>(accountById));
        }

        public IDataResult<List<AccountListDTO>> GetAll()
        {
            var accounts = _accountDAL.GetAllWithCustomer().Where(c=>c.Deleted==0).ToList();
            var result = accounts.Select(a => new AccountListDTO
            {
                Id = a.Id,
                AccountNumber = a.AccountNumber,
                AccountType = a.AccountType,
                Balance = a.Balance,
                CustomerId = a.CustomerId,
                CustomerFullName = a.Customer != null && a.Customer.Deleted == 0 
                ? $"{a.Customer.FirstName} {a.Customer.LastName}"
                : "Unknown"
            }).ToList();
            return new SuccessDataResult<List<AccountListDTO>>(_mapper.Map<List<AccountListDTO>>(result));
        }

        public IResult Update(AccountUpdateDTO accountUpdateDTO)
        {
            var accountMapper = _mapper.Map<Account>(accountUpdateDTO);
            var validateValidator = new AccountValidation();
            var validationResults = validateValidator.Validate(accountMapper);

            if (!validationResults.IsValid)
                return new ErrorResult(validationResults.Errors.FluentErrorString());

            _accountDAL.Update(accountMapper);
            return new SuccessResult("Updated Successfully");
        }
    }
}
