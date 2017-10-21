using System;

namespace VersioningSpike2
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MimeTypeAttribute : Attribute
    {
        public MimeTypeAttribute(string mimeType) => MimeType = mimeType;

        public string MimeType { get; }
    }
}