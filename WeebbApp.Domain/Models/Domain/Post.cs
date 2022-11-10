using System.ComponentModel.DataAnnotations.Schema;
using WeebApp.Enums;
using System.ComponentModel.DataAnnotations;
namespace WeebApp.Models.Domain
{
    public class Post
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("Creator")]
        public string CreatorId { get; set; }
        public ApplicationUser Creator { get; set; }

        [ForeignKey("Status")]
        public StatusEnum StatusId { get; set; }
        public Status Status { get; set; }

    }
}
