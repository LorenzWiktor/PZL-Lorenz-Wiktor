namespace PZL.TestApi.DTOs
{
    public class CreateTaskRequest
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int ProjectId { get; set; }
    }
}

