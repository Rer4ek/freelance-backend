namespace Freelance.API.Contracts
{
    public record UserRequest(
        string? Name,
        string? Description,
        IFormFile? Resume,
        IFormFile? Photo,
        string Password,
        string Login
    );
}
