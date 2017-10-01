using System.Data;

namespace Savage.Data
{
    public interface ICommandBuilder
    {
        IDbCommand BuildCommand(IStoredProcedure storedprocedure);
    }
}
