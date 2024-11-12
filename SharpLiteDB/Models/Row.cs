namespace SharpLiteDB.Models
{
    internal class Row
    {
        internal IDictionary<string, object> Data { get; private set; }

        internal Row(IDictionary<string, object> data)
        {
            Data = data;
        }
    }

}
