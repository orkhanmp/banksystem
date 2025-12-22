using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Extension
{
    internal static class ErrorValidateToStringExtension
    {
        internal static string FluentErrorString(this List<ValidationFailure> failures)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in failures)
            {
                sb.Append(item);
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
