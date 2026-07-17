using Diffusion.Common;

namespace Diffusion.Database
{
    public partial class Migrations
    {
        private class ImagePromptRow
        {
            public int Id { get; set; }
            public string Prompt { get; set; }
        }

        [Migrate(MigrationType.Post)]
        private string Rotox20260717_0001_BackfillImageLoras()
        {
            Logger.Log("Backfilling Lora/ImageLora tables from existing Prompt data");

            var images = _db.Query<ImagePromptRow>(
                "SELECT Id, Prompt FROM Image WHERE Prompt IS NOT NULL AND Prompt != ''");

            _db.BeginTransaction();
            try
            {
                var loraIdCache = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

                foreach (var image in images)
                {
                    var names = LoraParser.ParseLoraTokens(image.Prompt)
                        .Select(t => t.Name)
                        .Distinct(StringComparer.OrdinalIgnoreCase);

                    foreach (var name in names)
                    {
                        if (!loraIdCache.TryGetValue(name, out var loraId))
                        {
                            _db.Execute("INSERT INTO Lora (Name) VALUES (?) ON CONFLICT(Name) DO NOTHING", name);
                            loraId = _db.Query<DataStore.ReturnId>("SELECT Id FROM Lora WHERE Name = ?", name).First().Id;
                            loraIdCache[name] = loraId;
                        }

                        _db.Execute("INSERT INTO ImageLora (ImageId, LoraId) VALUES (?, ?)", image.Id, loraId);
                    }
                }

                _db.Commit();
            }
            catch
            {
                _db.Rollback();
                throw;
            }

            return ""; // NOT null - records the ledger row; empty string means the generic split-and-execute step is a no-op
        }
    }
}
