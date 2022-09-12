namespace WeebApp.Models
{
    public class AddPostViewModel
    {
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
