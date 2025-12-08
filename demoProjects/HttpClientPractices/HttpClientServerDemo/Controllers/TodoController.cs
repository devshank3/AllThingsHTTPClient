using Microsoft.AspNetCore.Mvc;
using HttpClientServerDemo.Models;

namespace HttpClientServerDemo.Controllers
{
    /// <summary>
    /// Controller demonstrating all HTTP verbs: GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS
    /// Used to test HttpClient capabilities
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private static List<TodoItem> _todos = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Learn HttpClient", IsCompleted = false, CreatedDate = DateTime.Now, Description = "Study HTTP methods" },
            new TodoItem { Id = 2, Title = "Test REST API", IsCompleted = false, CreatedDate = DateTime.Now, Description = "Test all verbs" }
        };
        private static int _nextId = 3;
        private readonly ILogger<TodoController> _logger;

        public TodoController(ILogger<TodoController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// GET: Retrieve all todos or a specific todo by id
        /// Query parameter: ?delay=5000 to simulate slow response
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get([FromQuery] int? delay)
        {
            _logger.LogInformation($"GET all todos requested. Delay: {delay}ms");

            // Simulate delay for timeout testing
            if (delay.HasValue && delay.Value > 0)
            {
                await Task.Delay(delay.Value);
            }

            return Ok(_todos);
        }

        /// <summary>
        /// GET: Retrieve a specific todo by ID
        /// </summary>
        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            _logger.LogInformation($"GET todo {id} requested");

            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound(new { message = $"Todo with id {id} not found" });
            }

            return Ok(todo);
        }

        /// <summary>
        /// POST: Create a new todo item
        /// Demonstrates creating resources
        /// </summary>
        [HttpPost]
        public ActionResult<TodoItem> Create([FromBody] CreateTodoRequest request)
        {
            _logger.LogInformation($"POST new todo: {request.Title}");

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                return BadRequest(new { message = "Title is required" });
            }

            var todo = new TodoItem
            {
                Id = _nextId++,
                Title = request.Title,
                Description = request.Description,
                IsCompleted = false,
                CreatedDate = DateTime.Now
            };

            _todos.Add(todo);

            // Return 201 Created with Location header
            return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
        }

        /// <summary>
        /// PUT: Complete replacement of a todo item
        /// Requires all fields
        /// </summary>
        [HttpPut("{id}")]
        public ActionResult<TodoItem> Replace(int id, [FromBody] TodoItem updatedTodo)
        {
            _logger.LogInformation($"PUT (replace) todo {id}");

            var index = _todos.FindIndex(t => t.Id == id);
            if (index == -1)
            {
                return NotFound(new { message = $"Todo with id {id} not found" });
            }

            updatedTodo.Id = id; // Ensure ID doesn't change
            updatedTodo.CreatedDate = _todos[index].CreatedDate; // Preserve creation date
            _todos[index] = updatedTodo;

            return Ok(updatedTodo);
        }

        /// <summary>
        /// PATCH: Partial update of a todo item
        /// Only updates provided fields
        /// </summary>
        [HttpPatch("{id}")]
        public ActionResult<TodoItem> Update(int id, [FromBody] UpdateTodoRequest request)
        {
            _logger.LogInformation($"PATCH (update) todo {id}");

            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound(new { message = $"Todo with id {id} not found" });
            }

            // Only update provided fields
            if (request.Title != null)
                todo.Title = request.Title;

            if (request.IsCompleted.HasValue)
                todo.IsCompleted = request.IsCompleted.Value;

            if (request.Description != null)
                todo.Description = request.Description;

            return Ok(todo);
        }

        /// <summary>
        /// DELETE: Remove a todo item
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation($"DELETE todo {id}");

            var todo = _todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return NotFound(new { message = $"Todo with id {id} not found" });
            }

            _todos.Remove(todo);
            return NoContent(); // 204 No Content
        }

        /// <summary>
        /// HEAD: Same as GET but returns only headers (no body)
        /// Useful for checking if resource exists without downloading content
        /// </summary>
        [HttpHead]
        public IActionResult Head()
        {
            _logger.LogInformation("HEAD request for todos");

            // Set custom headers
            Response.Headers.Add("X-Total-Count", _todos.Count.ToString());
            Response.Headers.Add("X-Last-Modified", DateTime.UtcNow.ToString("R"));

            return Ok();
        }

        /// <summary>
        /// OPTIONS: Returns allowed HTTP methods for this endpoint
        /// Used for CORS preflight requests
        /// </summary>
        [HttpOptions]
        public IActionResult Options()
        {
            _logger.LogInformation("OPTIONS request for todos");

            Response.Headers.Add("Allow", "GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS");
            return Ok();
        }
    }
}
