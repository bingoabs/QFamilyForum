namespace QFamilyForum.Data
{
    public class TodoItem
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get;set;}
        public DateTime EndTime { get;set;}
    }
}
