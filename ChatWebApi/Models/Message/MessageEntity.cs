using ChatWebApi.Models.Member;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatWebApi.Models.Message
{
    public class MessageEntity
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MessageId { get; set; }

        [Required]
        public Guid FromId { get; set; }

        [Required]
        public virtual MemberEntity Member { get; set; }

        [Required]
        public Guid ToId { get; set; }

        [Required]
        public DateTime SendDateTime { get; set; }

        [Required]
        [MaxLength(500)]
        public string Message { get; set; } = string.Empty;

       
    }
}
