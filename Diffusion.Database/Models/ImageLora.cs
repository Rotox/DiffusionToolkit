using SQLite;

namespace Diffusion.Database.Models;

public class ImageLora
{
    [Indexed(Name = "IDX_ImageLora", Order = 1, Unique = true)]
    public int ImageId { get; set; }
    [Indexed(Name = "IDX_ImageLora", Order = 2, Unique = true)]
    public int LoraId { get; set; }
}
