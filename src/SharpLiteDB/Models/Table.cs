namespace SharpLiteDB.Models
{
    internal class Table
    {
        internal string Name { get; private set; }
        
        internal IList<Row> Rows { get; private set; } = new List<Row>();

        internal Table(string name)
        {
            Name = name;
        }

        internal void InsertRow(Row row)
        {
            Rows.Add(row);
        }
    }

}
