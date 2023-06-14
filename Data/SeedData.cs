
namespace QFamilyForum.Data
{
    public class SeedData
    {
        public static void Initialize(QFamilyForumContext db)
        {
            // insert `todos` test data
            var todos = new TodoItem[] {
                new TodoItem(){
                    Id = 0,
                    Title = "Test_Title",
                    Description = "Test Describe",
                    EndTime = DateTime.Now,
                    IsDone = false
                }
            };
            db.TodoItem.AddRange(todos);
            db.SaveChanges();
        }
    }
}
