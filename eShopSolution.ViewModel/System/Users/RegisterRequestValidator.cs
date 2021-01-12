using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.System.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required")
                .MaximumLength(200).WithMessage("FirstName can not over than 200 characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required")
               .MaximumLength(200).WithMessage("Last Name can not over than 200 characters");
            RuleFor(x => x.DoB).GreaterThan(DateTime.Now.AddYears(-100)).WithMessage("Birth Day cannot greater than 100");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required").EmailAddress();
            //RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required")
            //    .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
            //    .WithMessage("Email not match");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone is required");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password is at least 6 characters");

            RuleFor(x => x).Custom((request, context) =>
            {
                if (request.Password != request.ConfirmPassword)
                {
                    context.AddFailure("Confirm Password is not match");
                }
            });
        }
    }
}