namespace Freelance.API.Contracts
{
    public record UserRequest(
        string? Name,
        string? Description,
        string? Resume,
        string? Photo,
        string Password,
        string Login
    );
}
