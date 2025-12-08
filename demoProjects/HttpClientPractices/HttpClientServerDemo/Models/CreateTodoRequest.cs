namespace HttpClientServerDemo.Models
{
    /// <summary>
    /// Request model for creating a new Todo item
    /// </summary>
    public class CreateTodoRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}