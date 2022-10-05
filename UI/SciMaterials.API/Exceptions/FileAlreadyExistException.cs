namespace SciMaterials.API.Exceptions;

public class FileAlreadyExistException : Exception
{
    public string FileName { get; }

    public FileAlreadyExistException(string FileName) : this(FileName, $"File {FileName} already exist") { }
    public FileAlreadyExistException(string FileName, string message) : base(message)
       => this.FileName = FileName;
}
