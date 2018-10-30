using System;
using System.IO;
using DotNetCore2018.Business.Services.Interfaces;

namespace DotNetCore2018.Business.Services
{
    public class FileService : IFileService
    {
        public Guid? SaveFile(Stream file)
        {
            Guid? imageName = null;

            if (file != null)
            {
                imageName = Guid.NewGuid();
                using (var writer = File.OpenWrite(Path(imageName.Value)))
                {
                    try
                    {
                        file.CopyTo(writer);
                    }
                    finally
                    {
                        file.Dispose();
                    }
                }
            }

            return imageName;
        }

        public Stream OpenFile(Guid imageName)
        {
            if (File.Exists(Path(imageName)))
            {
                return File.OpenRead(Path(imageName));
            }
            return NoImageFile();
        }

        public Stream NoImageFile() => File.OpenRead(Path("NoImage", "jpg"));

        public void Delete(Guid? image)
        {
            if (image != null)
            {
                File.Delete(Path(image.Value));
            }
        }

        private string Path<T>(T name, string ext = "jpeg") => $"./wwwroot/images/{name}.{ext}";
    }
}