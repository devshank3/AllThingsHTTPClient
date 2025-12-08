namespace HttpClientServerDemo.Models
{
    /// <summary>
    /// Represents a Todo item used to demonstrate CRUD operations
    /// </summary>
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Description { get; set; }
    }
}