using BudgetPlanner.DbModels;
using BudgetPlanner.Properties;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetPlanner.Validation
{
    internal class PersonValidator : AbstractValidator<Person>
    {
        public PersonValidator()
        {
            var msg = "Incorrect input {PropertyName}: Invalid value: {PropertyValue}";

            RuleFor(c => c.Name)
            .NotEmpty().WithMessage(Resources.EmptyInput)
            .Must(c => c.All(Char.IsLetter)).WithMessage(msg)
            .MaximumLength(50).WithMessage(msg);

            RuleFor(c => c.Surname)
            .NotEmpty().WithMessage(Resources.EmptyInput)
            .Must(c => c.Where(ch => ch != '-').All(Char.IsLetter)).WithMessage(msg)
            .MaximumLength(50).WithMessage(msg);

            RuleFor(c => c.Age)
            .GreaterThan(8).WithMessage(msg)
            .LessThan(110).WithMessage(msg);
        }

    }
}
