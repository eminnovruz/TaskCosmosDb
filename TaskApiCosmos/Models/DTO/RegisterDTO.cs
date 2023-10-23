namespace TaskApiCosmos.Models.DTO
{
    public class RegisterDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ushort Age { get; set; }
        public IFormFile ProfilePicture { get; set; }
    }
}
