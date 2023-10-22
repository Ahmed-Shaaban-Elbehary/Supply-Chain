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
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            int targetWidth = _configuration.GetValue<int>("ImageDimensions:targetWidth", 150);
            int targetHeight = _configuration.GetValue<int>("ImageDimensions:targetHeight", 150);
            int cropWidth = _configuration.GetValue<int>("ImageDimensions:cropWidth", 150);
            int cropHeight = _configuration.GetValue<int>("ImageDimensions:cropHeight", 150);


            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageData = memoryStream.ToArray();
                byte[] croppedImageData = await CropAndResizeImageAsync(imageData, targetWidth, targetHeight, cropWidth, cropHeight);

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "/images", "products");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                File.WriteAllBytes(filePath, croppedImageData);

                // Return the relative path for database storage
                return Path.Combine("images", "products", fileName).Replace("\\", "/");
            }
        }
        private async Task<byte[]> CropAndResizeImageAsync(byte[] imageData, int targetWidth, int targetHeight, int cropWidth, int cropHeight)
        {
            try
            {
                if (imageData == null || imageData.Length == 0)
                {
                    throw new ArgumentException("Image data is empty or null.");
                }

                if (targetWidth <= 0 || targetHeight <= 0 || cropWidth <= 0 || cropHeight <= 0)
                {
                    throw new ArgumentException("Invalid target or crop dimensions.");
                }

                using (var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageData))
                {
                    int cropX = (image.Width - cropWidth) / 2;
                    int cropY = (image.Height - cropHeight) / 2;
                    // Ensure the specified crop dimensions are within the bounds of the source image.
                    if (cropWidth > image.Width || cropHeight > image.Height)
                    {
                        throw new ArgumentException("Crop dimensions exceed source image size.");
                    }

                    image.Mutate(x => x
                        .Resize(new ResizeOptions
                        {
                            Size = new Size(targetWidth, targetHeight),
                            Position = AnchorPositionMode.Center
                        })
                        .Crop(new Rectangle(cropX, cropY, cropWidth, cropHeight)));

                    using (var memoryStream = new MemoryStream())
                    {
                        await image.SaveAsync(memoryStream, new PngEncoder());
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
