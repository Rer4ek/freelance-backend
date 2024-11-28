namespace Freelance.Core.Models
{
    public class Session
    {
        public Guid Id { get; set; }

        public Guid UserID { get; set; }

        private Session(Guid id, Guid userId)
        {
            Id = id;
            UserID = userId;
        }

        public static Session Create(Guid id, Guid userId)
        {
            return new Session(id, userId);
        }
    }
}
