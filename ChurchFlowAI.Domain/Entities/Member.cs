namespace ChurchFlowAI.Domain.Entities
{
    public class Member
    {
        public Guid Id { get; set; }

        public Guid ChurchId { get; set; }   // 🔥 MULTI-TENANT KEY

        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
    }
}