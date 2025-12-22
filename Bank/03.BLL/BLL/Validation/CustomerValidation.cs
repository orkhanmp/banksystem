using Entities.TableModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class CustomerValidation: AbstractValidator<Customer>
    {
        public CustomerValidation()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("First name cannot be empty.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Last name cannot be empty.");
            RuleFor(c=> c.Age).NotEmpty().WithMessage("Age cannot be empty.")
                               .GreaterThan(0).WithMessage("Age must be greater than 0.");
            RuleFor(c => c.City).NotEmpty().WithMessage("City cannot be empty.");
        }
    }
}
