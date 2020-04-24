namespace Quizler.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadPictureAsync(string pictureFile, string fileName);
    }
}
