namespace NoteTakingServer.Models {
    public class Todo {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Detline { get; set; }
        public bool HasDetline { get; set; }
        public bool IsCompleted { get; set; }
    }
}