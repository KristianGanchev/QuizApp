namespace Quizler.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        string UploadPictureAsync(string pictureFile, string fileName);
    }
}
