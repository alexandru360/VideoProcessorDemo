using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbEntities
{
    [Table("Hls264")]
    public class Hls264
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string FileName { get; set; }
        [Column(TypeName = "Blob")]
        public byte[] FileContents { get; set; }
    }
}
