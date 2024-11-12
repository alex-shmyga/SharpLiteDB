using SharpLiteDB.Storage;

namespace SharpLiteDB.Commands
{
    internal class SelectCommand : BaseCommand
    {
        internal SelectCommand(InMemoryStorage storage, string tableName)
            : base(storage, tableName)
        {
        }

        public override void Execute()
        {
            try
            {
                var rows = _storage.SelectAll(_tableName);
                foreach (var row in rows)
                {
                    Console.WriteLine(string.Join(", ", row.Data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
