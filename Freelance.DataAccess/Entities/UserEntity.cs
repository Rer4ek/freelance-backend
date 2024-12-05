namespace Freelance.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public string Login { get; set; } = string.Empty;

        public string? Name { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public Guid? PhotoId { get; set; }
        public PhotoFileEntity? Photo { get; set; }

        public Guid? ResumeId { get; set; }
        public ResumeFileEntity? Resume { get; set; }

        public string Password { get; set; } = string.Empty;

        public List<SessionEntity> Sessions { get; set; } = new List<SessionEntity>(); 

    }
}
