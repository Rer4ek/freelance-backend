namespace Freelance.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Discription { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Resume { get; set; } = string.Empty;
    }
}
