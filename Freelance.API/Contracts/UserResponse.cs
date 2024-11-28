namespace Freelance.API.Contracts
{
    public record UserResponse(
        Guid Id,
        string? Name,
        string? Description,
        string? Resume,
        string? Photo,
        string Password,
        string Login
    );
}
