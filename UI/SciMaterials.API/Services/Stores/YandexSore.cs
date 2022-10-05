using System.Security.Cryptography;
using System.Text.Json;
using SciMaterials.API.Models;
using SciMaterials.API.Services.Interfaces;

namespace SciMaterials.API.Services.Stores;

public class YandexSore : IFileStore
{
    public void Delete(string FilePath)
    {
        throw new NotImplementedException();
    }

    public Stream OpenRead(string FilePath)
    {
        throw new NotImplementedException();
    }

    public Task<T> ReadMetadataAsync<T>(string FilePath, CancellationToken Cancel = default)
    {
        throw new NotImplementedException();
    }

    public Task<FileSaveResult> WriteAsync(string FilePath, Stream stream, CancellationToken Cancel = default)
    {
        throw new NotImplementedException();
    }

    public Task WriteMetadataAsync<T>(string FilePath, T data, CancellationToken Cancel = default)
    {
        throw new NotImplementedException();
    }
}