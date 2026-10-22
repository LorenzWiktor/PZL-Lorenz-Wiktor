namespace PZL.TestApi.Domain
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TaskItemStatus Status { get; set; } = TaskItemStatus.Todo;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DueDate { get; set; }
        public int ProjectId { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}