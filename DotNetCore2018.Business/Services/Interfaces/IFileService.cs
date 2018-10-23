using System;
using System.IO;

namespace DotNetCore2018.Business.Services.Interfaces
{
    public interface IFileService
    {
        Guid? SaveFile(Stream file);
        Stream OpenFile(Guid imageName);
        Stream NoImageFile();
        void Delete(Guid? image);
    }
}