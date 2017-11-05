using System.Data;

namespace Savage.Data
{
    public interface ICommandBuilder
    {
        IDbCommand BuildCommand(ISqlCommand sqlCommand);
    }
}
