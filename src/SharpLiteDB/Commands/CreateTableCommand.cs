using SharpLiteDB.Storage;

namespace SharpLiteDB.Commands
{
    internal class CreateTableCommand : BaseCommand
    {
        internal CreateTableCommand(IStorage storage, string tableName)
            : base(storage, tableName)
        {
        }

        public override void Execute()
        {
            try
            {
                _storage.CreateTable(_tableName);
                Console.WriteLine($"CreateTableCommand: Table '{_tableName}' created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
