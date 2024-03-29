namespace Security.Sample.Api;

using FluentValidation;

class CredentialsDtoValidator : AbstractValidator<CredentialsDto>
{
  public CredentialsDtoValidator()
  {
    RuleFor(credentials => credentials.UserName).NotNull().MaximumLength(10);
    RuleFor(credentials => credentials.Password).NotNull().MaximumLength(10);
  }
}