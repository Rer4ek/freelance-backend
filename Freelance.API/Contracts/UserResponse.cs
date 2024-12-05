using Microsoft.AspNetCore.Mvc;

namespace Freelance.API.Contracts
{
    public record UserResponse(
        Guid Id,
        string? Name,
        string? Description,
        FileContentResult? Resume,
        FileContentResult? Photo,
        string Login
    );
}
