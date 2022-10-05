using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using SciMaterials.API.Data.Interfaces;
using SciMaterials.API.Mappings;
using SciMaterials.API.Services.Interfaces;

namespace SciMaterials.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly ILogger<FilesController> _logger;
    private readonly IFileService<Guid> _fileService;

    private void LogError(Exception ex, [CallerMemberName] string MethodName = null!)
        => _logger.LogError(ex, "ошибка выполнения {error}", MethodName);

    public FilesController(ILogger<FilesController> logger, IFileService<Guid> FileService)
    {
        _logger = logger;
        _fileService = FileService;
    }

    [HttpGet("GetByHash/{hash}")]
    public IActionResult GetByHash([FromRoute] string hash)
    {
        try
        {
            var file_info = _fileService.GetFileInfoByHash(hash);
            var file_stream = _fileService.GetFileStream(file_info.Id);
            return File(file_stream, file_info.ContentType, file_info.FileName);
        }
        catch (FileNotFoundException)
        {
            return BadRequest($"File with hash({hash}) not found");
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    [HttpGet("GetById/{id}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        try
        {
            var file_info = _fileService.GetFileInfoById(id);
            var file_stream = _fileService.GetFileStream(id);
            return File(file_stream, file_info.ContentType, file_info.FileName);
        }
        catch (FileNotFoundException)
        {
            return BadRequest($"File with id({id}) not found");
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

    [HttpPost("Upload")]
    public async Task<IActionResult> UploadAsync()
    {
        try
        {
            var request = HttpContext.Request;

            if (!request.HasFormContentType ||
               !MediaTypeHeaderValue.TryParse(request.ContentType, out var media_type_header) ||
               string.IsNullOrEmpty(media_type_header.Boundary.Value))
            {
                return new UnsupportedMediaTypeResult();
            }

            var reader = new MultipartReader(media_type_header.Boundary.Value, request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var has_content_disposition_header = ContentDispositionHeaderValue.TryParse(
                    section.ContentDisposition,
                    out var content_disposition);

                if (has_content_disposition_header 
                    && content_disposition is { } 
                    && content_disposition.DispositionType.Equals("form-data") 
                    && !string.IsNullOrEmpty(content_disposition.FileName.Value))
                {
                    _logger.LogInformation("Section contains file {file}", content_disposition.FileName.Value);
                    var result = await _fileService.UploadAsync(
                        section.Body, 
                        content_disposition.FileName.Value, 
                        section.ContentType ?? "application/octet-stream")
                       .ConfigureAwait(false);
                    return Ok(result.ToViewModel());
                }

                section = await reader.ReadNextSectionAsync();
            }

            return BadRequest("No files data in the request.");
        }
        catch (Exception ex)
        {
            LogError(ex);
            throw;
        }
    }

}
