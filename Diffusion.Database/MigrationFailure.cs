using System;

namespace Diffusion.Database;

public class MigrationFailure
{
    public string Name { get; }
    public MigrationType MigrationType { get; }
    public Exception Exception { get; }

    public MigrationFailure(string name, MigrationType migrationType, Exception exception)
    {
        Name = name;
        MigrationType = migrationType;
        Exception = exception;
    }
}
