using HealthyHabbitsWeb.Data.ViewModels;

namespace HealthyHabbitsWeb.Data
{
    public interface IUserRegistrationService
    {
        SignUpViewModel User { get; set; }
    }

    public class UserRegistrationService : IUserRegistrationService
    {
        public SignUpViewModel User { get; set; }
    }
}
