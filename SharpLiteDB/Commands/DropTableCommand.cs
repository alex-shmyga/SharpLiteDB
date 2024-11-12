using SharpLiteDB.Storage;

namespace SharpLiteDB.Commands
{
    internal class DropTableCommand : BaseCommand
    {
        internal DropTableCommand(InMemoryStorage storage, string tableName)
            : base(storage, tableName)
        {
        }

        public override void Execute()
        {
            try
            {
                _storage.DropTable(_tableName);
                Console.WriteLine($"DropTableCommand: Table '{_tableName}' was removed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
