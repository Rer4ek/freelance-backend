namespace Freelance.API.Contracts
{
    public record UserRequest(
        string Name,
        string? Discription,
        string Password,
        string? Resume
    );
}
