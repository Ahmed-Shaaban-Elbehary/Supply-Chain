using SkiaSharp;
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

            // Create an SKPath to define the custom cropping shape (e.g., a square)
            SKPath cropShape = new SKPath();
            cropShape.AddRect(SKRect.Create(targetWidth, targetHeight));

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageData = memoryStream.ToArray();
                byte[] croppedImageData = await CropAndResizeImageAsync(imageData, targetWidth, targetHeight, cropShape);

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "/images", "products");
                Directory.CreateDirectory(uploadsFolder);

                string fileName = Path.GetFileName(file.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                File.WriteAllBytes(filePath, croppedImageData);

                // Return the relative path for database storage
                return Path.Combine("images", "products", fileName).Replace("\\", "/");
            }
        }
        private async Task<byte[]> CropAndResizeImageAsync(byte[] imageData, int targetWidth, int targetHeight, SKPath cropShape)
        {
            try
            {
                if (imageData == null || imageData.Length == 0)
                {
                    throw new ArgumentException("Image data is empty or null.");
                }

                if (targetWidth <= 0 || targetHeight <= 0)
                {
                    throw new ArgumentException("Invalid target dimensions.");
                }

                using (SKBitmap inputBitmap = SKBitmap.Decode(imageData))
                {
                    if (cropShape != null)
                    {
                        using (SKBitmap croppedBitmap = new SKBitmap(targetWidth, targetHeight))
                        {
                            using (SKCanvas canvas = new SKCanvas(croppedBitmap))
                            {
                                // Set the clip path for non-rectangular cropping
                                canvas.ClipPath(cropShape);

                                // Draw the input bitmap onto the canvas, applying the non-rectangular clip
                                canvas.DrawBitmap(inputBitmap, SKRect.Create(targetWidth, targetHeight));
                            }

                            // Convert the cropped SKBitmap to a byte array
                            using (SKImage image = SKImage.FromBitmap(croppedBitmap))
                            using (SKData data = image.Encode())
                            {
                                return await Task.FromResult(data.ToArray());
                            }
                        }
                    }
                    else
                    {
                        // If no crop shape is provided, perform a regular rectangular resize
                        using (SKImage scaledImage = SKImage.FromBitmap(inputBitmap))
                        using (SKData data = scaledImage.Encode())
                        {
                            return await Task.FromResult(data.ToArray());
                        }
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