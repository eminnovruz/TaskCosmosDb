namespace TaskApiCosmos.Models
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public ushort Age { get; set; } = 0;
        public string? ProfilePhoto { get; set; }
    }
}
