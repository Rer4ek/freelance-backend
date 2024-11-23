namespace Freelance.Core.Models
{
    public class Resume
    {

        public Resume(Guid guid, string path)
        {
            Id = guid;
            Path = path;
        }

        public Guid Id { get; private set; }

        public string Path { get; private set; } = string.Empty;

    }
}
