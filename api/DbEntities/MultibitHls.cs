using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("MultibitHls")]
    public class MultibitHls
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
        [Column(TypeName = "Blob")]
        public byte[] FileContents { get; set; }
        [StringLength(250)]
        public string GroupName { get; set; }
    }
}
