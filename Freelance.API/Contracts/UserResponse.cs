namespace Freelance.API.Contracts
{
    public record UserResponse(
        Guid Id,
        string Name,
        string? Discription,
        string Password,
        string? Resume
    );
}
