namespace SharpLiteDB.Models
{
    internal class Table
    {
        internal string _name { get; private set; }
        internal List<Page> _pages;

        internal IList<Row> Rows
        {
            get
            {
                return _pages.SelectMany(p => p.Rows).ToList();
            }
        }

        internal Table(string name)
        {
            _name = name;
            _pages = new List<Page>();
        }

        internal void InsertRow(Row row)
        {
            Page page = _pages.LastOrDefault();
            if (page == null || page.IsFull)
            {
                page = new Page();
                _pages.Add(page);
            }
            page.AddRow(row);
        }

        public void SavePagesToDisk()
        {
            for (int i = 0; i < _pages.Count; i++)
            {
                string pageFileName = $"{_name}_page_{i}.pg";
                try
                {
                    File.WriteAllBytes(pageFileName, _pages[i].Serialize());
                    Console.WriteLine($"Page {i} saved to disk.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving page {i} to disk: {ex.Message}");
                }
            }
        }

        public void LoadPagesFromDisk()
        {
            _pages.Clear();
            int pageIndex = 0;

            while (true)
            {
                string pageFileName = $"{_name}_page_{pageIndex}.pg";
                if (File.Exists(pageFileName))
                {
                    try
                    {
                        byte[] pageData = File.ReadAllBytes(pageFileName);
                        _pages.Add(Page.Deserialize(pageData));
                        Console.WriteLine($"Page {pageIndex} loaded from disk.");
                        pageIndex++;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading page {pageIndex} from disk: {ex.Message}");
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}
