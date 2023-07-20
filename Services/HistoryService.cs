using LiteDB;
using QFamilyForum.Data;

namespace QFamilyForum.Services
{
    public class HistoryService
    {
        private readonly LiteDatabase db;
        public HistoryService()
        {
            db = new LiteDatabase(@"MyData.db");
        }
        public List<HistoryRecord> GetHistoryRecords() 
        {
            var col = db.GetCollection<HistoryRecord>("historyRecords");

            // Create your new customer instance
            var record = new HistoryRecord
            {
                Id = 1,
                Content = "Bingo Login in",
                InsertedAt = DateTime.UnixEpoch.Ticks
            };

            // Create unique index in Name field
            col.EnsureIndex(x => x.Id, true);

            // Insert new customer document (Id will be auto-incremented)
            col.Insert(record);

            // Update a document inside a collection
            record.Content = "Libby Login in";

            col.Update(record);

            // Use LINQ to query documents (with no index)
            var results = col.Find(x => x.Id == 1).ToList();
            return results;
        }
    }
}
