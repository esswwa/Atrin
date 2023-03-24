using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace projectPP
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.LoginV).NotNull().NotEqual("Логин").Length(5, 20).Must(c => c.All(Char.IsLetterOrDigit));
            RuleFor(customer => customer.NameV).NotNull().NotEqual("Имя").Length(2, 20).Must(c => c.All(Char.IsLetter));
            RuleFor(customer => customer.SurnameV).NotNull().NotEqual("Фамилия").Length(3, 20).Must(c => c.All(Char.IsLetter));
            RuleFor(customer => customer.OtchestvoV).NotNull().NotEqual("Отчество").Length(3, 20).Must(c => c.All(Char.IsLetter));
            RuleFor(customer => customer.NumberV).NotNull().NotEqual("Номер телефона").Length(11).Must(c => c.All(Char.IsDigit));
            //RuleFor(customer => customer.PasswordV).NotNull().NotEqual("Пароль").Length(10, 25).Must(c => c.All(Char.IsLetterOrDigit));
            RuleFor(customer => customer.PasswordV).NotNull().NotEqual("Пароль").Length(10, 25).Must(c => c.Any(Char.IsLetterOrDigit) || c.Any(Char.IsSymbol));

        }


    }
}
