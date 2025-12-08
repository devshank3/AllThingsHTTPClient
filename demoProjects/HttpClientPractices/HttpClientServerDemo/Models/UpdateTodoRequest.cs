namespace HttpClientServerDemo.Models
{
    /// <summary>
    /// Request model for updating an existing Todo item
    /// </summary>
    public class UpdateTodoRequest
    {
        public string? Title { get; set; }
        public bool? IsCompleted { get; set; }
        public string? Description { get; set; }
    }
}