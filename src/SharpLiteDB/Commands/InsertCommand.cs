using SharpLiteDB.Storage;

namespace SharpLiteDB.Commands
{
    internal class InsertCommand : BaseCommand
    {
        private readonly Dictionary<string, object> _rowData;

        internal InsertCommand(IStorage storage, string tableName, Dictionary<string, object> rowData)
            : base(storage, tableName)
        {
            _rowData = rowData;
        }

        public override void Execute()
        {
            try
            {
                _storage.InsertRow(_tableName, _rowData);
                Console.WriteLine("InsertCommand: Row inserted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
