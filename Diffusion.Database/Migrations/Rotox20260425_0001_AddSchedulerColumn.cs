using Diffusion.Common;
namespace Diffusion.Database
{
    public partial class Migrations
    {
        [Migrate(MigrationType.Post)]
        private string Rotox20260425_0001_AddSchedulerColumn()
        {
            if (_db.HasColumn("Image", "Scheduler"))
            {
                Logger.Log("Scheduler column already exists on Image table, skipping");
                return "";
            }

            Logger.Log($"Adding Scheduler column to Image table");
            return "ALTER TABLE Image ADD COLUMN Scheduler varchar";
        }
    }
}