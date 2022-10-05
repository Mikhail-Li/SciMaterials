namespace SciMaterials.API.Models;

public readonly struct FileSaveResult
{
    public string Hash { get; init; }
    public long Size { get; init; }

    public FileSaveResult(string hash, long size) => (Hash, Size) = (hash, size);
}