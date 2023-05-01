using SixLabors.ImageSharp.Formats.Png;
using SupplyChain.App.Utils.Contracts;

namespace SupplyChain.App.Utils
{
    public class UploadFile : IUploadFile
    {
        private IWebHostEnvironment _webHostEnvironment;
        private IConfiguration _configuration;
        public UploadFile(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        public bool IsImageExist(IFormFile file)
        {
            string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");
            string fileName = file.FileName + extension;
            string filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
                return true;
            else
                return false;
            
        }
        public async Task<string> UploadImage(IFormFile file)
        {
            string filePath = string.Empty;
            int width = int.Parse(_configuration["ImageDimensions:Width"] ?? "150");
            int height = int.Parse(_configuration["ImageDimensions:Height"] ?? "150");
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageData = memoryStream.ToArray();
                byte[] croppedImageData = await CropImageAsync(imageData, width, height);
                string extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, @"images\products");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string fileName = file.FileName + extension;
                filePath = Path.Combine(uploadsFolder, fileName);

                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await fileStream.WriteAsync(croppedImageData);
                }
                //to save in Database
                filePath = @$"\images\products\{fileName}";
            }
            return filePath;
        }
        private async Task<byte[]> CropImageAsync(byte[] imageData, int width, int height)
        {
            using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageData))
            {
                image.Mutate(x => x.Resize(new Size(200, 200)).Crop(new Rectangle(0, 0, width, height)));
                using (var memoryStream = new MemoryStream())
                {
                    await image.SaveAsync(memoryStream, new PngEncoder());
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
