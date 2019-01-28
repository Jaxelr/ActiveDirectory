using Api.Model.ActiveDirectory.Operations;
using ServiceStack.FluentValidation;

namespace Api.Logic.Validators
{
    public class UserValidator : AbstractValidator<GetUser>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }

    public class UserGroupsValidator : AbstractValidator<GetUserGroups>
    {
        public UserGroupsValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }

    public class GroupUsersValidator : AbstractValidator<GetGroupUsers>
    {
        public GroupUsersValidator()
        {
            RuleFor(x => x.Group).NotEmpty();
        }
    }

    public class IsUserInGroupsValidator : AbstractValidator<IsUserInGroups>
    {
        public IsUserInGroupsValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }

    public class AuthenticateValidator : AbstractValidator<AuthenticateUser>
    {
        public AuthenticateValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.Domain).NotEmpty().Unless(y => y.Domain == null);
        }
    }
}