using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
namespace projectPP
{
    public class CustomerValidator3 : AbstractValidator<Customer2>
    {
        public CustomerValidator3()
        {
            RuleFor(customer => customer.Opisanie).NotNull().NotEmpty().Length(5, 150).Must(c => c.Any(Char.IsLetterOrDigit) || c.Any(Char.IsSymbol) || c.Any(Char.IsWhiteSpace)); 
            RuleFor(customer => customer.Name).NotNull().NotEmpty().Length(3, 14).Must(c => c.Any(Char.IsLetterOrDigit) || c.Any(Char.IsSymbol) || c.Any(Char.IsWhiteSpace));
            RuleFor(customer => customer.Price).NotNull().NotEmpty().Length(1, 7).Must(c => c.All(Char.IsDigit));
            RuleFor(customer => customer.Adress).NotNull().NotEmpty().Length(5, 25).Must(c => c.Any(Char.IsLetterOrDigit) || c.Any(Char.IsSymbol) || c.Any(Char.IsWhiteSpace));
        }
    }
}
