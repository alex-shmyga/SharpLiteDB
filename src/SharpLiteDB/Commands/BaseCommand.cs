using SharpLiteDB.Storage;

namespace SharpLiteDB.Commands
{
    // TODO: not sure if needed
    internal abstract class BaseCommand : ICommand
    {
        internal readonly IStorage _storage;
        internal readonly string _tableName;

        protected BaseCommand(IStorage storage, string tableName)
        {
            _storage = storage;
            _tableName = tableName;
        }

        public abstract void Execute();
    }
}
