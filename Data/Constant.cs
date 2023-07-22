using LiteDB;

namespace QFamilyForum.Data
{
    public class Constant
    {
        public static string DBPath = @"QFamilyForumLite.db";
        public static LiteDatabase db = new LiteDatabase(DBPath);
        public static ILiteCollection<APIInfo> APIInfoCollection = db.GetCollection<APIInfo>("APIInfo");
        public static ILiteCollection<HistoryRecord> HistoryCollection = db.GetCollection<HistoryRecord>("historyRecords");
    }
}
