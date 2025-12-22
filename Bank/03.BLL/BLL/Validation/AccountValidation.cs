using Entities.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class AccountValidation:AbstractValidator<Account>
    {
        public AccountValidation()
        {
            RuleFor(a => a.AccountNumber).NotEmpty().WithMessage("Account number cannot be empty.");
            RuleFor(a => a.Balance).GreaterThanOrEqualTo(0).WithMessage("Balance cannot be negative.");
            RuleFor(a => a.CustomerId).NotEmpty().WithMessage("Customer ID cannot be empty.");
        }
    }
}
