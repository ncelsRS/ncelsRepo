namespace SynchronizationServer
{
    public class DbSynchronizerConfiguration
    {
        public string ConnectionString { get; set; }
        public string ScopeName { get; set; }
        public string ObjectPrefix { get; set; }
        public string ObjectSchema { get; set; }
    }
}