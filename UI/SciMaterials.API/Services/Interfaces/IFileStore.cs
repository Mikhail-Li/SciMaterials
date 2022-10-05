using SciMaterials.API.Models;

namespace SciMaterials.API.Services.Interfaces;

public interface IFileStore
{
    Task<FileSaveResult> WriteAsync(string FilePath, Stream stream, CancellationToken Cancel = default);
    
    Task WriteMetadataAsync<T>(string FilePath, T data, CancellationToken Cancel = default);
    
    Stream OpenRead(string FilePath);
    
    Task<T> ReadMetadataAsync<T>(string FilePath, CancellationToken Cancel = default);
    
    void Delete(string FilePath);
}