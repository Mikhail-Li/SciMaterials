using System.Diagnostics;
using System.Security.Cryptography;
using SciMaterials.API.Data.Interfaces;
using SciMaterials.API.Exceptions;
using SciMaterials.API.Models;
using SciMaterials.API.Services.Interfaces;

namespace SciMaterials.API.Services;

public class FileService : IFileService<Guid>
{
    private readonly ILogger<FileService> _Logger;
    private readonly IFileRepository<Guid> _FileRepository;
    private readonly IFileStore _FileStore;
    private readonly string _FilePath;
    private readonly bool _OverWrite;

    public FileService(
        ILogger<FileService> Logger, 
        IConfiguration Configuration, 
        IFileRepository<Guid> FileRepository, 
        IFileStore FileStore)
    {
        _Logger = Logger;
        _FileRepository = FileRepository;
        _FileStore = FileStore;
        _FilePath = Configuration.GetValue<string>("BasePath");
        if (string.IsNullOrEmpty(_FilePath))
            throw new ArgumentNullException(nameof(_FilePath));
    }

    public FileModel GetFileInfoById(Guid id)
    {
        var model = _FileRepository.GetById(id);

        if (model is null)
            throw new FileNotFoundException($"File with id {id} not found");

        return model;
    }

    public FileModel GetFileInfoByHash(string hash)
    {
        var model = _FileRepository.GetByHash(hash);

        if (model is null)
            throw new FileNotFoundException($"File with hash {hash} not found");

        return model;
    }
    public Stream GetFileStream(Guid id)
    {
        var read_from_path = Path.Combine(_FilePath, id.ToString());
        return _FileStore.OpenRead(read_from_path);
    }

    public async Task<FileModel> UploadAsync(Stream FileStream, string FileName, string ContentType, CancellationToken Cancel = default)
    {
        var file_name_with_exension = Path.GetFileName(FileName);
        var file_model = _FileRepository.GetByName(file_name_with_exension);

        if (file_model is not null && !_OverWrite)
        {
            var exception = new FileAlreadyExistException(FileName);
            _Logger.LogError(exception, null);
            throw exception;
        }

        var random_file_name = file_model?.Id ?? Guid.NewGuid();
        var save_to_path = Path.Combine(_FilePath, random_file_name.ToString());
        var metadata_path = Path.Combine(_FilePath, random_file_name + ".json");


        var sw = new Stopwatch();
        sw.Start();
        var save_result = await _FileStore.WriteAsync(save_to_path, FileStream, Cancel).ConfigureAwait(false);
        sw.Stop();
        _Logger.LogInformation("Ellapsed:{ellapsed} сек", sw.Elapsed.TotalSeconds);

        if (CheckFileExistByHash(save_result.Hash, out var existing_file_info) && existing_file_info is not null)
        {
            var exception = new FileAlreadyExistException(FileName, $"File with the same hash {existing_file_info.Hash} already exists with id: {existing_file_info.Id.ToString()}");
            _FileStore.Delete(save_to_path);
            _Logger.LogError(exception, null);
            throw exception;
        }

        if (file_model is null)
        {
            file_model = new FileModel
            {
                Id = random_file_name,
                FileName = file_name_with_exension,
                ContentType = ContentType,
                Hash = save_result.Hash,
                Size = save_result.Size
            };
            // _fileRepository.Add(fileModel);
        }
        else
        {
            file_model.Hash = save_result.Hash;
            file_model.Size = save_result.Size;
            // _fileRepository.Update(fileModel);
        }
        _FileRepository.AddOrUpdate(file_model);

        await _FileStore.WriteMetadataAsync(metadata_path, file_model, Cancel).ConfigureAwait(false);
        return file_model;
    }

    private void AddOrUpdateFileInfo(ref FileModel FileInfo)
    {

    }

    private bool CheckFileExistByHash(string hash, out FileModel? FileInfo)
    {
        FileInfo = _FileRepository.GetByHash(hash);
        return FileInfo is not null;
    }
}
