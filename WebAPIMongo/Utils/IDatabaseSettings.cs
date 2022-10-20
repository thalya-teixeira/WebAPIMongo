namespace WebAPIMongo.Utils
{
    public interface IDatabaseSettings
    {
        string ClientCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

        public string AddressCollectionName { get; set; }

    }
}
