namespace SupplyChain.App.Utils.Contracts
{
    public interface IUploadFile
    {
        public Task<string> UploadImage(IFormFile file);
        public bool IsImageExist(IFormFile file);
    }
}
