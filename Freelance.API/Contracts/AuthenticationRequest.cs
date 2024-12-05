namespace Freelance.API.Contracts
{
    public record AuthenticationRequest (
        string Login,
        string Password
    );
}
