using Diffusion.Common;
namespace Diffusion.Database
{
    public partial class Migrations
    {
        [Migrate(MigrationType.Post)]
        private string Rotox20260425_0001_AddSchedulerColumn()
        {
            Logger.Log($"Adding Scheduler column to Image table");
            return "ALTER TABLE Image ADD COLUMN Scheduler varchar";
        }
    }
}