namespace QFamilyForum.Data
{
    public class TodoService
    {
        private List<TodoItem> todos = new List<TodoItem>{};
        public TodoService() 
        {
            todos = Enumerable.Range(1, 5).Select(index => new TodoItem
            {
                Id = index,
                Title = $"Todo {index}",
                Description = $"This is a description for Todo {index}",
                EndTime = DateTime.Now.AddDays(index)
            }).ToList();
        }

        public Task<TodoItem[]> GetTodoAsync()
        {
            
            return Task.FromResult(todos.OrderBy(item => item.EndTime).ToArray());
        }
        public void AddTodo(string title)
        {
            Console.WriteLine($"Added Todo: {title}");
            todos.Add(new TodoItem{
                Id = todos.Count + 1,
                Title = title,
                Description = $"This is a description for Todo {todos.Count + 1}",
                EndTime = DateTime.Now.AddDays(todos.Count + 1)
            }); 
        }
    }
}
