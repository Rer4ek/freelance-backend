namespace Freelance.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string? Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? Photo {  get; set; } = string.Empty;

        public string? Resume { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<SessionEntity> Sessions { get; set; } = new List<SessionEntity>(); 

    }
}
