using EmployeeManagement.Application.DTOs.Master.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Application.Validators.Master.Employee
{
    public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email) 
                .NotEmpty() 
                .MaximumLength(150);

            RuleFor(x => x.PhoneNumber) 
                .MaximumLength(20);

        }
    }
}
