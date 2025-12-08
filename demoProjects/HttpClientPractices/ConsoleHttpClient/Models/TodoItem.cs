using System;

namespace ConsoleHttpClient.Models
{
    /// <summary>
    /// Represents a Todo item - matches server model
    /// </summary>
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
    }

    public class CreateTodoRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public class UpdateTodoRequest
    {
        public string Title { get; set; }
        public bool? IsCompleted { get; set; }
        public string Description { get; set; }
    }
}