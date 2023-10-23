namespace TaskApiCosmos.Services.Interfaces
{
    public interface IStorageManager
    {
        string GetSignedUrl(string fileName);
        string? UploadFile(Stream stream, string fileName, string contentType);
        Task<string?> UploadFileAsync(Stream stream, string fileName, string contentType);

        bool DeleteFile(string fileName);
        Task<bool> DeleteFileAsync(string fileName);

    }
}
