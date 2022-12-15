namespace BlazorApp
{
    public class TodoItem
    {
        public string? Todo { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
