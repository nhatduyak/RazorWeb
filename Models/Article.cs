using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tich_hop_EntityFramework.models
{

    // [Table("Posts")]
    public class Article
    {
        [Key]
        public int id { get; set; }
        [StringLength(255)]
        [Required]
        [Column(TypeName ="nvarchar")]
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
[DisplayName("Ngày tạo")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime Create { get; set; }

                [Column(TypeName ="ntext")]
[DisplayName("Nội dung")]
        public string  Content { get; set; }

    }
}