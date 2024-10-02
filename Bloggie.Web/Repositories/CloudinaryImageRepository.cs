
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account _account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _account = new Account(_configuration.GetSection("Cloudinary")["CloudName"], _configuration.GetSection("Cloudinary")["ApiKey"], _configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(_account);
            var uploadPrams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName,
            };

            var uploadresult =await client.UploadAsync(uploadPrams);

            if (uploadresult != null && uploadresult.StatusCode == HttpStatusCode.OK) 
            {
               return uploadresult.SecureUri.ToString(); 
            }
            return null;

        }
    }
}
