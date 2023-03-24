using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace projectPP
{
    public class CustomerValidator1 : AbstractValidator<Customer1>
    {
        public CustomerValidator1()
        {
            RuleFor(customer => customer.LoginV).NotNull().NotEqual("Логин").Length(5, 20);
            RuleFor(customer => customer.PasswordV).NotNull().NotEqual("Пароль").Length(10, 25).Must(c => c.All(Char.IsLetterOrDigit));
        }


    }
}
