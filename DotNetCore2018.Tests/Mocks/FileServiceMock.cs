using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DotNetCore2018.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DotNetCore2018.Tests.Mocks
{
    internal class FileServiceMock : IFileService
    {
        public void Delete(Guid? image)
        {
        }

        public Stream NoImageFile()
        {
            throw new NotImplementedException();
        }

        public Stream OpenFile(Guid imageName)
        {
            throw new NotImplementedException();
        }

        public Guid? SaveFile(Stream file) => Guid.Empty;
    }

    internal class ImageMock : IFormFile
    {
        public string ContentType => throw new NotImplementedException();

        public string ContentDisposition => throw new NotImplementedException();

        public IHeaderDictionary Headers => throw new NotImplementedException();

        public long Length => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string FileName => throw new NotImplementedException();

        public void CopyTo(Stream target)
        {
            throw new NotImplementedException();
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public Stream OpenReadStream() => null;
    }
}