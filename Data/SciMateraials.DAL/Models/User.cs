using SciMaterials.DAL.Models.Base;

namespace SciMaterials.DAL.Models;

public class User : NamedModel
{
    public string Email { get; set; } = string.Empty;

    public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public ICollection<File> Files { get; set; } = new HashSet<File>();
    public ICollection<Rating> Ratings { get; set; } = new HashSet<Rating>();
}
