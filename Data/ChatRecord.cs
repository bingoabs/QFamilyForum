namespace QFamilyForum.Data
{
    public class ChatRecord
    {
        public long Id { get;set;}
        public string Text { get;set;}
        public long InsertedAt { get;set;}
        public long SenderId { get;set;}
        public long ReceiverId { get;set;}
    }
}
