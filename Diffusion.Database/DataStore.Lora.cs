using Diffusion.Database.Models;
using SQLite;

namespace Diffusion.Database
{
    public partial class DataStore
    {
        public IEnumerable<string> GetLoraNames()
        {
            using var db = OpenConnection();

            var loras = db.Query<Lora>($"SELECT Name FROM {nameof(Lora)} ORDER BY Name COLLATE NOCASE");

            db.Close();

            return loras.Select(l => l.Name);
        }

        // Caller supplies an already-open connection, since this is always called
        // from inside a larger bulk operation (migration backfill, scan indexing).
        public int UpsertLora(SQLiteConnection db, string name)
        {
            db.Execute($"INSERT INTO {nameof(Lora)} (Name) VALUES (?) ON CONFLICT(Name) DO NOTHING", name);

            return db.Query<Lora>($"SELECT Id FROM {nameof(Lora)} WHERE Name = ?", name).First().Id;
        }

        // Full resync for one image - deletes existing ImageLora rows first so a
        // rescan that changes the LoRA set (additions or removals) stays correct.
        public void SetImageLoras(SQLiteConnection db, int imageId, IEnumerable<string> loraNames)
        {
            db.Execute($"DELETE FROM {nameof(ImageLora)} WHERE ImageId = ?", imageId);

            foreach (var name in loraNames.Distinct(StringComparer.OrdinalIgnoreCase))
            {
                var loraId = UpsertLora(db, name);
                db.Execute($"INSERT INTO {nameof(ImageLora)} (ImageId, LoraId) VALUES (?, ?)", imageId, loraId);
            }
        }
    }
}
