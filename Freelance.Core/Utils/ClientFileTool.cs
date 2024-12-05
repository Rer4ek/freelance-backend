using Freelance.Core.Models;
using Freelance.Core.Сonstants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using MimeKit;
using System.Web;

namespace Freelance.Core.Utils
{
    public static class ClientFileTool
    {

        private static readonly FileExtensionContentTypeProvider _provider = new FileExtensionContentTypeProvider();

        public static async Task<ClientFile?> Download(string path, IFormFile? file, Guid? id = null)
        {
            if (file == null)
            {
                return null;
            }

            Guid newId = id ?? Guid.NewGuid();
            string filePath = Path.Combine(path, newId.ToString() + GetExtension(file.ContentType));

            using (FileStream stream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                await file.CopyToAsync(stream);
            }
            return ClientFile.Create(newId, filePath);
        }

        public static async Task<FileContentResult?> Upload(string? path, string name, string defaultPath = "")
        {
            if (!File.Exists(path))
            {
                path = defaultPath;
            }

            if (path == "")
            {
                return null;
            }

            byte[] bytes = await File.ReadAllBytesAsync(path);
            string contentType = GetContentType(path);
            await Console.Out.WriteLineAsync(contentType);
            FileContentResult file = new FileContentResult(bytes, contentType);
            file.FileDownloadName = name;
            return file;
        }

        public static string GetContentType(string path)
        {
            _provider.TryGetContentType(path, out string? type);
            return type ?? "application/octet-stream";
        }

        public static string GetExtension(string type)
        {
            MimeTypes.TryGetExtension(type, out string? extension);
            return extension ?? string.Empty;
        }

    }
}
