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
                using (var writer = File.OpenWrite($"./wwwroot/images/{imageName}.jpeg"))
                {
                    file.CopyTo(writer);
                }
            }

            return imageName;
        }

        public Stream OpenFile(Guid imageName) => File.OpenRead($"./wwwroot/images/{imageName}.jpeg");

        public Stream NoImageFile() => File.OpenRead($"./wwwroot/images/NoImage.jpg");
    }
}