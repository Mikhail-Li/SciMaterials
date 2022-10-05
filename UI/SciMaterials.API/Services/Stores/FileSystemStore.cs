using System.Security.Cryptography;
using System.Text.Json;

using SciMaterials.API.Models;
using SciMaterials.API.Services.Interfaces;

namespace SciMaterials.API.Services.Stores;

public class FileSystemStore : IFileStore
{
    private const long __BufferSize = 100 * 1024 * 1024;
    private readonly ILogger<FileSystemStore> _logger;

    public FileSystemStore(ILogger<FileSystemStore> logger) => _logger = logger;

    public async Task<FileSaveResult> WriteAsync(string FilePath, Stream stream, CancellationToken Cancel = default)
    {
        var directory = Path.GetDirectoryName(FilePath);
        if (!Directory.Exists(directory))
        {
            _logger.LogInformation("Create directory {directory}", directory);
            Directory.CreateDirectory(directory);
        }

        await using var write_stream = File.Create(FilePath);
        using var hash_algorithm = SHA512.Create();

        int bytes_read;
        long total_bytes_read = 0;
        var buffer = new byte[__BufferSize];
        while ((bytes_read = await stream.ReadAsync(buffer, 0, buffer.Length, Cancel).ConfigureAwait(false)) > 0)
        {
            await write_stream.WriteAsync(buffer, 0, bytes_read, Cancel).ConfigureAwait(false);
            hash_algorithm.TransformBlock(buffer, 0, bytes_read, null, 0);
            total_bytes_read += bytes_read;
        }

        hash_algorithm.TransformFinalBlock(buffer, 0, 0);

        if (hash_algorithm is not { Hash: { } hash })
        {
            var exception = new NullReferenceException("Unable to calculate Hash");
            _logger.LogError(exception, null);
            throw exception;
        }

        var hash_string = Convert.ToHexString(hash);
        return new FileSaveResult(hash_string, total_bytes_read);
    }

    public async Task WriteMetadataAsync<T>(string FilePath, T data, CancellationToken Cancel = default)
    {
        var meta_data = JsonSerializer.Serialize(data);
        await File.WriteAllTextAsync(FilePath, meta_data, Cancel).ConfigureAwait(false);
    }

    public Stream OpenRead(string FilePath)
    {
        if (!File.Exists(FilePath))
            throw new FileNotFoundException($"File {Path.GetFileName(FilePath)}  not found");

        var stream = File.OpenRead(FilePath);
        return stream;
    }

    public async Task<T> ReadMetadataAsync<T>(string FilePath, CancellationToken Cancel = default)
    {
        if (!File.Exists(FilePath))
            throw new FileNotFoundException($"Metadata file {Path.GetFileName(FilePath)}  not found");

        await using var reader = File.OpenRead(FilePath);
        var meta_data = await JsonSerializer.DeserializeAsync<T>(reader, cancellationToken: Cancel).ConfigureAwait(false);

        if (meta_data is null)
            throw new NullReferenceException("Metadata cannot be read");

        return meta_data;
    }

    public void Delete(string FilePath)
    {
        File.Delete(FilePath);
    }
}