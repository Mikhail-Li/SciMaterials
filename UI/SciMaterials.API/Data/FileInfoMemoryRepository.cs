using System.Collections.Concurrent;
using SciMaterials.API.Data.Interfaces;
using SciMaterials.API.Models;

namespace SciMaterials.API.Data;

public class FileInfoMemoryRepository : IFileRepository<Guid>
{
    private readonly ConcurrentDictionary<Guid, FileModel> _Files = new();

    public bool Add(FileModel model) => _Files.TryAdd(model.Id, model);

    public void Update(FileModel model) => _Files[model.Id] = model;

    public void AddOrUpdate(FileModel model) => _Files.AddOrUpdate(model.Id, model, (_, _) => model);

    public void Delete(Guid id) => _Files.Remove(id, out _);

    public FileModel? GetByHash(string hash) => _Files.Values.SingleOrDefault(item => item.Hash == hash);

    public FileModel? GetById(Guid id) => _Files.GetValueOrDefault(id);

    public FileModel? GetByName(string FileName) => _Files.Values.SingleOrDefault(item => item.FileName == FileName);
}