namespace API.Dtos.Users
{
    public class UpdateUserDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string account { get; set; }
        public string email { get; set; }
        public string contact { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}