namespace Security.Sample.Endpoints;

using FluentValidation;

sealed class CredentialsValidator : AbstractValidator<CredentialsRequest>
{
  public CredentialsValidator()
  {
    RuleFor(credentials => credentials.UserName).NotNull().MaximumLength(10);
    RuleFor(credentials => credentials.Password).NotNull().MaximumLength(10);
  }
}