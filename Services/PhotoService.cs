using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings>config)
        {
            var acc = new Account
            (
                config.Value.Cloudname,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }


        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
           var uploadresult = new ImageUploadResult();

           if (file.Length > 0)
           {
               using var stream = file.OpenReadStream();

               var uploadParams = new ImageUploadParams
               {
                   File = new FileDescription(file.FileName, stream),
                   Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
               };
               uploadResult = await _cloudinary.UploadAsync(uploadParams);
           }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletephotoAsync(string publicId)
        {
            var deletParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}