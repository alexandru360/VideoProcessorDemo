using System;
using System.IO;
using System.Runtime.Serialization;

namespace DTO
{
    public class ThumbnailDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FileContents { get; set; }
    }
}
