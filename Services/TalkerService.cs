using QFamilyForum.Data;
using QFamilyForum.Interface;

namespace QFamilyForum.Services
{
    public class TalkerService : ITalker
    {
        private User Master = new User { Id = 1, Name = "Master", ImagePath = "ava1.png" };
        private User Guest = new User { Id = 2, Name = "Guest", ImagePath = "ava2.png" };
        private List<ChatRecord> ChatRecords = new List<ChatRecord>();
        private long NextInsertedId = 0;
        public TalkerService() 
        {
            ChatRecords.Add(new ChatRecord { Id = NextInsertedId, Text = "Hello", InsertedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), SenderId = Master.Id, ReceiverId = Guest.Id });
            NextInsertedId = NextInsertedId + 1;
        }
        public void AddMessage(string message)
        {
            AddRecord(message, Master.Id, Guest.Id);
            string response = GetResponse(message);
            Thread.Sleep(1000);
            AddRecord(response, Guest.Id, Master.Id);
        }
        private string GetResponse(string message)
        {
            // todo
            return "Hello, " + message;
        }
        private void AddRecord(string message, long senderId, long receiverId)
        {
            ChatRecords.Add(new ChatRecord { Id = NextInsertedId, Text = message, InsertedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds(), SenderId = senderId, ReceiverId = receiverId });
            NextInsertedId = NextInsertedId + 1;
        }

        public User GetGuest()
        {
            return Guest;
        }

        public User GetMaster()
        {
            return Master;
        }

        public List<ChatRecord> GetRecords()
        {
            return ChatRecords;
        }

        public string GetUserNameById(long id)
        {
            return Master.Id == id ? Master.Name : Guest.Name;
        }
    }
}
