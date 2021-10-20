using FluentValidation;
using PlannerApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlannerApp.Shared.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .EmailAddress()
                .WithMessage("Email is not a valid email address")
                ;

            RuleFor(r => r.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .MaximumLength(25)
                .WithMessage("First name must be less than 25 characters");


            RuleFor(r => r.LastName)
              .NotEmpty()
              .WithMessage("Last name is required")
              .MaximumLength(25)
              .WithMessage("last name must be less than 25 characters");



            RuleFor(r => r.Password)
            .NotEmpty()
            .WithMessage("Password  is required")
            .MaximumLength(25)
            .WithMessage("last name must be less than 25 characters");


            RuleFor(r => r.ConfirmPassword)
            .Equal(r => r.Password)
            .WithMessage("Confirm password and password dont match");  



        }
    }
}
