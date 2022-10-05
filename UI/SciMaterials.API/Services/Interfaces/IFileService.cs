using SciMaterials.API.Models;

namespace SciMaterials.API.Services.Interfaces;

public interface IFileService<T>
{
    Task<FileModel> UploadAsync(Stream FileStream, string FileName, string ContentType, CancellationToken Cancel = default);
    
    Stream GetFileStream(T id);
    
    FileModel GetFileInfoById(T id);
    
    FileModel GetFileInfoByHash(string hash);
}
