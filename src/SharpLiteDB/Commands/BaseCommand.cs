using SharpLiteDB.Storage;

namespace SharpLiteDB.Commands
{
    // TODO: not sure if needed
    internal abstract class BaseCommand : ICommand
    {
        internal readonly InMemoryStorage _storage;
        internal readonly string _tableName;

        protected BaseCommand(InMemoryStorage storage, string tableName)
        {
            _storage = storage;
            _tableName = tableName;
        }

        public abstract void Execute();
    }
}
