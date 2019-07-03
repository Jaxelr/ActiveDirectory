﻿using ActiveDirectory.Entities.Operations;
using FluentValidation;

namespace ActiveDirectory.Validation
{
    public class AuthenticUserValidator : AbstractValidator<AuthenticUserRequest>
    {
        public AuthenticUserValidator()
        {
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
