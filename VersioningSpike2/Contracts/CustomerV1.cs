namespace VersioningSpike2.Contracts
{
    [MimeType(MimeTypes.CustomerV1)]
    public class CustomerV1
    {
        public string FirstName { get; set; }
        public int TypeVersion => 1;
    }
}