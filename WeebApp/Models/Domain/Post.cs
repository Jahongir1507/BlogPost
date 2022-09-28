using System.ComponentModel.DataAnnotations.Schema;

namespace WeebApp.Models.Domain
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ForeignKey("Creator")]
        public string? CreatorId { get; set; }
        public ApplicationUser? Creator { get; set; }

    }
}
