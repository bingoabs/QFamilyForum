using QFamilyForum.Data;

namespace QFamilyForum.Interface
{
    public interface ITalker
    {
        public User GetMaster();
        public User GetGuest();
        public List<ChatRecord> GetRecords();
        public void AddMessage(string message);
        public string GetUserNameById(long id);
    }
}
