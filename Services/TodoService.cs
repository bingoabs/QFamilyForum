using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using QFamilyForum.Data;

namespace QFamilyForum.Services
{
    public class TodoService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private List<TodoItem> todos = new List<TodoItem>();
        public TodoService(HttpClient httpClient, NavigationManager navigationManager)
        {
            // only call once in one page
            Console.WriteLine("Initilize TodoService");
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            // when migrate the code, and show error, need to consider the difference between environments
            var result = httpClient.GetFromJsonAsync<List<TodoItem>>(_navigationManager.BaseUri + "api/TodoItems").Result;
            if(result != null)
            {
                todos = result;
            }
        }

        public List<TodoItem> GetTodos()
        {
            return todos;
        }
        public void AddTodo(TodoItem item)
        {
            Console.WriteLine($"Added Todo: {item.Title}");
            todos.Add(item);
        }

        public void UpdateTodo(TodoItem item)
        {
            Console.WriteLine($"Updated Todo: {item.Title}");
            var index = todos.FindIndex(x => x.Id == item.Id);
            todos[index] = item;
        }
        ~TodoService()
        {
            Console.WriteLine($"Into TodoService dispose");
        }
    }
}
