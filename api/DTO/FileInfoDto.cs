using System.Runtime.Serialization;

namespace DTO
{
    public class FileInfoDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string FilePath { get; set; }
    }
}
