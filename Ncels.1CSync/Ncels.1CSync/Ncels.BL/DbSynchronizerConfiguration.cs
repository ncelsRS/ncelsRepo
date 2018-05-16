namespace Ncels.BL
{
    public class DbSynchronizerConfiguration
    {
        public string ConnectionString { get; set; }
        public string ScopeName { get; set; }
        public string ObjectPrefix { get; set; }
        public string ObjectSchema { get; set; }
    }

    public class TableMapping
    {
        public string TableNameDest { get; set; }
        public string GlobalName { get; set; }
        public string TableNameSrc { get; set; }
    }
}