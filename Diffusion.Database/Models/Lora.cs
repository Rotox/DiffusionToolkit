using SQLite;

namespace Diffusion.Database.Models;

public class Lora
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
}
