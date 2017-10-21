namespace VersioningSpike2.Contracts
{
    [MimeType(MimeTypes.CustomerV2)]
    public class CustomerV2
    {
        public string Forename { get; set; }
        public int TypeVersion => 2;
    }
}