namespace WorkPlanner.Entities
{
    public class Task
    {
        public Guid ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public TaskStatus Status { get; set; }

        public string Duration { get; set; }
        public DateTime Date { get; set; }

 
        public enum TaskStatus
        {
            Planned = 0,
            WorkingAt = 1,
            Done = 2,
            RememberTo = 3
        }
    }
}