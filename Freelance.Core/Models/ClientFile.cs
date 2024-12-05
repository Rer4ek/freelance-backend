namespace Freelance.Core.Models
{
    public class ClientFile
    {

        public ClientFile(Guid guid, string path)
        {
            Id = guid;
            Path = path;
        }

        public Guid Id { get; private set; }

        public string Path { get; private set; } = string.Empty;

        public static ClientFile Create(Guid id, string path)
        {
            return new ClientFile(id, path);
        }

    }
}
